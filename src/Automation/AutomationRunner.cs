using Microsoft.Playwright;
using System;
using System.IO;
using System.Threading.Tasks;
using CondecoAssistant.Helpers;
using CondecoAssistant.Models;
using System.Text;
using System.Windows;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.IO.Pipes;


namespace CondecoAssistant.Automation;

public static class AutomationRunner
{
    public static async Task RunAsync()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var startTime = DateTime.Now;

        var prefs = PreferencesStorage.Load();
        string username = prefs.Username;
        string password = prefs.Password;
        string formsLink = prefs.FormsLink;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Username or password is not set.");
            return;
        }

        if (string.IsNullOrEmpty(formsLink))
        {
            Console.WriteLine("Forms link not found.");
            return;
        }

        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            AcceptDownloads = true
        });
        var page = await context.NewPageAsync();
        // Go to the MS form
        await page.GotoAsync(formsLink);
        // Enter email
        await Locators.FormsEmailField(page).ClickAsync();
        await Locators.FormsEmailField(page).FillAsync(username);
        await Locators.FormsNextButton(page).ClickAsync();
        // Enter password
        await Locators.FormsPasswordField(page).ClickAsync();
        await Locators.FormsPasswordField(page).FillAsync(password);
        await Locators.FormsSignInPage_SignInButton(page).ClickAsync();

        // Download the form responses
        await Locators.DropdownButton(page).ClickAsync();
        var downloadTask = page.WaitForDownloadAsync();
        await Locators.DownloadButton(page).ClickAsync();
        var download = await downloadTask;

        // Save the form responses locally in the project
        var filePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\form_responses.xlsx"));
        await download.SaveAsAsync(filePath);

        //var bookingsPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\form_responses2.xlsx"));
        var bookings = ExcelReader.ReadBookings(filePath);

        // Prepare log file
        var logFilePath = Path.ChangeExtension(filePath, ".log");
        var logSb = new StringBuilder();
        logSb.AppendLine($"=== Automation Run Started ===");
        logSb.AppendLine($"Start Time: {startTime:yyyy-MM-dd HH:mm:ss}");
        logSb.AppendLine();

        var bookingsByDay = BookingHelper.GroupByDay(bookings);

        await page.GotoAsync("https://hoopp.condecosoftware.com");

        await page.WaitForTimeoutAsync(120000); // Remain idle for 120 seconds and let the next day bookings open up (12:01am)

        var successfulBookings = new List<string>();
        var failedBookings = new List<string>();

        await Locators.HomePageButton(page).ClickAsync();
        await Locators.YourTeamButton(page).ClickAsync();
        await Locators.TeamDaysButton(page).ClickAsync();
        
        // Iterate through each day and make bookings
        foreach (var day in bookingsByDay.Where(d => d.Value.Any()))
        {
            try
            {
                await page.WaitForTimeoutAsync(2000); // Wait for the page to load
                await Locators.CreateTeamDayButton(page).ClickAsync();
                await Locators.CalendarButton(page).ClickAsync();

                var today = DateTime.Today;
                var bookingStart = today.AddDays(14 - (int)today.DayOfWeek + (int)DayOfWeek.Monday); // 2 Mondays from now
                var bookingWeek = Enumerable.Range(0, 7).Select(i => bookingStart.AddDays(i)).ToList();

                int currentMonth = today.Month;
                bool includesNextMonth = bookingWeek.Any(d => d.Month != currentMonth);

                // Try the most likely month first
                string primaryMonth = includesNextMonth ? "6" : "5";
                string fallbackMonth = includesNextMonth ? "5" : "6";

                // Try primary month
                await Locators.SelectMonthButton(page).SelectOptionAsync(new SelectOptionValue { Value = primaryMonth });

                var bookingDateLocator = Locators.BookingDate(page, day.Key);
                var bookingClickTask = bookingDateLocator.ClickAsync();
                var bookingTimeoutTask = Task.Delay(5000);

                var completedBookingTask = await Task.WhenAny(bookingClickTask, bookingTimeoutTask);

                if (completedBookingTask == bookingTimeoutTask)
                {
                    await Locators.CloseButton(page).ClickAsync();
                    await page.WaitForTimeoutAsync(2000); // Wait for the page to load
                    await Locators.CreateTeamDayButton(page).ClickAsync();
                    await Locators.CalendarButton(page).ClickAsync();
                    await Locators.SelectMonthButton(page).SelectOptionAsync(new SelectOptionValue { Value = fallbackMonth });

                    bookingClickTask = bookingDateLocator.ClickAsync();
                    bookingTimeoutTask = Task.Delay(5000);
                    completedBookingTask = await Task.WhenAny(bookingClickTask, bookingTimeoutTask);

                    if (completedBookingTask == bookingTimeoutTask)
                    {
                        logSb.AppendLine($"[FAIL] Could not select booking date {day.Key:yyyy-MM-dd} (timeout)");
                        continue;
                    }
                    else
                    {
                        await bookingClickTask;
                    }
                }
                else
                {
                    await bookingClickTask;
                }

                var names = day.Value.Select(b => b.Name).ToList();
                // Get the checkboxes locators for the names
                var personCheckboxLocators = Locators.PersonCheckBoxLocators(page, names);

                // Reset selection state
                await Locators.SelectAllCheckbox(page).ClickAsync();
                await Locators.SelectAllCheckbox(page).ClickAsync();

                // Select the checkboxes for the names
                foreach (var person in personCheckboxLocators)
                {
                    await person.ClickAsync();
                }

                await Locators.ContinueToBookingButton(page).ClickAsync();

                foreach (var booking in day.Value)
                {
                    var deskLocator = Locators.DeskButtonLocator(page, booking.Desk);

                    // Try to get the parent element with a timeout
                    var parentHandleTask = deskLocator.EvaluateHandleAsync("el => el.parentElement");
                    var timeoutTask = Task.Delay(5000);

                    var completedParentTask = await Task.WhenAny(parentHandleTask, timeoutTask);

                    if (completedParentTask == timeoutTask)
                    {
                        var failMsg = $"[FAIL] {day.Key:yyyy-MM-dd}: Could not get parent for desk {booking.Desk} ({booking.Name})";
                        failedBookings.Add(failMsg);
                        logSb.AppendLine(failMsg);
                        continue;
                    }

                    var parentHandle = await parentHandleTask;

                    // Check if the parent has the 'icon-selected' class
                    bool isSelected = await parentHandle.EvaluateAsync<bool>("el => el.classList.contains('icon-selected')");

                    if (!isSelected)
                    {
                        var clickTask = deskLocator.ClickAsync();
                        var clickTimeoutTask = Task.Delay(5000);

                        var completedClickTask = await Task.WhenAny(clickTask, clickTimeoutTask);

                        if (completedClickTask == clickTimeoutTask)
                        {
                            var failMsg = $"[FAIL] {day.Key:yyyy-MM-dd}: Desk {booking.Desk} not clickable for {booking.Name}";
                            failedBookings.Add(failMsg);
                            logSb.AppendLine(failMsg);
                        }
                        else
                        {
                            await clickTask;
                            var successMsg = $"[OK] {day.Key:yyyy-MM-dd}: Booked desk {booking.Desk} for {booking.Name}";
                            successfulBookings.Add(successMsg);
                            logSb.AppendLine(successMsg);
                        }
                    }
                    else
                    {
                        var alreadyMsg = $"[OK] {day.Key:yyyy-MM-dd}: Desk {booking.Desk} already selected for {booking.Name}";
                        successfulBookings.Add(alreadyMsg);
                        logSb.AppendLine(alreadyMsg);
                    }
                }

                await Locators.BookAndSendInvitesButton(page).ClickAsync();
                // After BookAndSendInvitesButton is clicked, check for "I have enough space" button and click if present
                try
                {
                    var enoughSpaceButton = Locators.IHaveEnoughSpaceButton(page);
                    if (await enoughSpaceButton.IsVisibleAsync(new() { Timeout = 2000 }))
                    {
                        await enoughSpaceButton.ClickAsync();
                    }
                }
                catch
                {
                    // Ignore if not found or not clickable, continue as normal
                }

                try
                {
                    var NoSpacesSelectedCloseButton = Locators.CloseTextButton(page);
                    if (await NoSpacesSelectedCloseButton.IsVisibleAsync(new() { Timeout = 2000 }))
                    {
                        await NoSpacesSelectedCloseButton.ClickAsync();
                        await Locators.CloseButton(page).ClickAsync(); // Close the modal
                        continue;
                    }
                }
                catch
                {
                    // Ignore if not found or not clickable, continue as normal
                }

                await Locators.DoneButton(page).ClickAsync();
            }
            catch (Exception ex)
            {
                var failMsg = $"[FAIL] {day.Key:yyyy-MM-dd}: Exception - {ex.Message}";
                failedBookings.Add(failMsg);
                logSb.AppendLine(failMsg);
                continue; // Skip this date if anything fails
            }
        }

        await browser.CloseAsync();

        stopwatch.Stop();
        var endTime = DateTime.Now;
        var duration = stopwatch.Elapsed;

        logSb.AppendLine();
        logSb.AppendLine($"=== Automation Run Ended ===");
        logSb.AppendLine($"End Time: {endTime:yyyy-MM-dd HH:mm:ss}");
        logSb.AppendLine($"Duration: {duration.TotalSeconds:F2} seconds");
        logSb.AppendLine();
        logSb.AppendLine("Summary:");
        logSb.AppendLine($"Successful bookings: {successfulBookings.Count}");
        logSb.AppendLine($"Unsuccessful bookings: {failedBookings.Count}");

        // Write log to file
        try
        {
            File.WriteAllText(logFilePath, logSb.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not write log file: {ex.Message}");
        }
    }
}