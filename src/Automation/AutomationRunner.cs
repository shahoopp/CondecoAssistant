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
        var startTime = stopwatch.Elapsed;

        var prefs = PreferencesStorage.Load();
        string username = prefs.Username;
        string password = prefs.Password;
        string formsLink = prefs.FormsLink;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Username or password is not set. Please set them in the application.");
            return;
        }


        if (string.IsNullOrEmpty(formsLink))
        {
            Console.WriteLine("Forms link not found. Please save it from the Home Page.");
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

        var bookingsPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\form_responses2.xlsx"));
        var bookings = ExcelReader.ReadBookings(bookingsPath);

        var bookingsByDay = BookingHelper.GroupByDay(bookings);

        await page.WaitForTimeoutAsync(120000); // Remain idle for 100 seconds and let the next day bookings open up (12:01am)

        // View the bookings by day in a message box
        /*var sb = new StringBuilder();
        foreach (var day in bookingsByDay)
        {
            sb.AppendLine($"📅 {day.Key}:");

            foreach (var booking in day.Value)
            {
                sb.AppendLine($"- {booking.Name} → {booking.Desk}");
            }

            sb.AppendLine();
        }

        MessageBox.Show(sb.ToString(), "Bookings by Day", MessageBoxButton.OK, MessageBoxImage.Information);*/

        // Iterate through each day and make bookings
        foreach (var day in bookingsByDay.Where(d => d.Value.Any()))
        {
            try
            {
                await page.GotoAsync("https://hoopp.condecosoftware.com");
                await Locators.HomePageButton(page).ClickAsync();
                await Locators.YourTeamButton(page).ClickAsync();
                await Locators.TeamDaysButton(page).ClickAsync();
                await page.WaitForTimeoutAsync(2000); // Wait for the page to load
                await Locators.CreateTeamDayButton(page).ClickAsync();
                await Locators.CalendarButton(page).ClickAsync();
                //await page.PauseAsync();


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
                    await page.GotoAsync("https://hoopp.condecosoftware.com");
                    await Locators.HomePageButton(page).ClickAsync();
                    await Locators.YourTeamButton(page).ClickAsync();
                    await Locators.TeamDaysButton(page).ClickAsync();
                    await page.WaitForTimeoutAsync(2000); // Wait for the page to load
                    await Locators.CreateTeamDayButton(page).ClickAsync();
                    await Locators.CalendarButton(page).ClickAsync();
                    await Locators.SelectMonthButton(page).SelectOptionAsync(new SelectOptionValue { Value = fallbackMonth });

                    bookingClickTask = bookingDateLocator.ClickAsync();
                    bookingTimeoutTask = Task.Delay(5000);
                    completedBookingTask = await Task.WhenAny(bookingClickTask, bookingTimeoutTask);

                    if (completedBookingTask == bookingTimeoutTask)
                    {
                        // Still failed, skip or log
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
                    // MessageBox.Show($"Booking desk {booking.Desk} for {booking.Name} on {day.Key}", "Booking Desk", MessageBoxButton.OK, MessageBoxImage.Information);

                    var deskLocator = Locators.DeskButtonLocator(page, booking.Desk);

                    // Try to get the parent element with a timeout
                    var parentHandleTask = deskLocator.EvaluateHandleAsync("el => el.parentElement");
                    var timeoutTask = Task.Delay(5000);

                    var completedParentTask = await Task.WhenAny(parentHandleTask, timeoutTask);

                    if (completedParentTask == timeoutTask)
                    {
                        Console.WriteLine($"⏳ Timeout: Could not get parent for desk {booking.Desk} on {day.Key}");
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
                            Console.WriteLine($"⏳ Timeout: Desk {booking.Desk} not clickable for {booking.Name} on {day.Key}");
                        }
                        else
                        {
                            await clickTask;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"✅ Desk {booking.Desk} already selected for {booking.Name} on {day.Key}");
                    }
                }

                // await page.PauseAsync();
                // await Locators.BookAndSendInvitesButton(page).ClickAsync();
                // await Locators.DoneButton(page).ClickAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error on {day.Key}: {ex.Message}");
                continue; // Skip this date if anything fails
            }
        }

        await browser.CloseAsync();

        stopwatch.Stop();
        var endTime = stopwatch.Elapsed;
        MessageBox.Show(
            $"Automation completed in {endTime.TotalSeconds:F2} seconds.\n\nStart Time: {DateTime.Now.Subtract(endTime - startTime):yyyy-MM-dd HH:mm:ss}\nEnd Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
            "Time Taken",
            MessageBoxButton.OK,
            MessageBoxImage.Information
        );
    }
}