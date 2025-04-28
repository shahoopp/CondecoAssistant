using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using CondecoAssistant.Helpers;
using CondecoAssistant.Models;
using System.Windows;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;


namespace CondecoAssistant.Automation;

public static class AutomationRunner
{
    public static async Task RunAsync()
    {
        var prefs = PreferencesStorage.Load();
        string username = prefs.Username;
        string password = prefs.Password;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Username or password is not set. Please set them in the application.");
            return;
        }

        //List<string> days = prefs.SelectedDays;
        //MessageBox.Show(string.Join("\n", days), "Booking days", MessageBoxButton.OK, MessageBoxImage.Information);

        //List<string> desks = prefs.SelectedDesksInPriority;
        //MessageBox.Show(string.Join("\n", desks), "Booking desks", MessageBoxButton.OK, MessageBoxImage.Information);

        // Initialize Playwright
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        //List<ILocator> dates = Locators.BookingDates(page);
        //MessageBox.Show(string.Join("\n", dates), "Booking dates", MessageBoxButton.OK, MessageBoxImage.Information);

        // Navigate to Condeco website
        await page.GotoAsync("https://hoopp.condecosoftware.com");

        // Sign in page - email
        await Locators.EmailField(page).WaitForAsync();
        await Locators.EmailField(page).ClickAsync();
        await Locators.EmailField(page).FillAsync(username);
        await Locators.SignInPageNextButton(page).ClickAsync();
        // Sign in page - password
        await Locators.PasswordField(page).WaitForAsync();
        await Locators.PasswordField(page).ClickAsync();
        await Locators.PasswordField(page).FillAsync(password);
        await Locators.SignInPage_SignInButton(page).ClickAsync();

        // Selecting booking dates
        var bookingDateLocators = Locators.BookingDates(page);
        var deskLocators = Locators.Desks(page);
        var remainingDeskLocators = Locators.RemainingDesks(page);
        // loop through each date and try to book a desk
        foreach (var dateLocator in bookingDateLocators)
        {
            try
            {
                // Always start the booking from the home page
                await Locators.HomePageButton(page).ClickAsync();
                // Home page left side navigation menu
                await Locators.PersonalSpacesButton(page).ClickAsync();
                await Locators.BookAPersonalSpaceButton(page).ClickAsync();
                // Book a personal space page - confirm selections
                await Locators.CountryDropdown(page).ClickAsync();
                await Locators.LocationDropdown(page).ClickAsync();
                await Locators.GroupDropdown(page).ClickAsync();
                await Locators.FloorDropdown(page).ClickAsync();
                await Locators.WorkspaceTypeDropdown(page).ClickAsync();
                // Select booking date from the calendar
                await dateLocator.ClickAsync();
                // Search for desks on selected date
                await Locators.SearchButton(page).ClickAsync();
                bool deskBooked = false;
                // loop through each desk and check for availability
                foreach (var deskLocator in deskLocators)
                {
                    try
                    {
                        // wait for the desk locator to be available
                        await deskLocator.WaitForAsync(new LocatorWaitForOptions() { Timeout = 3000 });
                        // click on the desk locator
                        await deskLocator.ClickAsync();
                        await Locators.BookDeskButton(page).ClickAsync();
                        await Locators.OKButton(page).ClickAsync();
                        deskBooked = true;
                        // break out of loop and move onto the next date for booking
                        break;
                    }
                    // if the desk in priority is not found, continue to the next desk in the list
                    catch (Exception)
                    {
                        Console.WriteLine($"Desks not available, trying next.");
                    }
                }
                if (!deskBooked)
                {
                    foreach (var remainingDesk in remainingDeskLocators)
                    {
                        try
                        {
                            // wait for the desk locator to be available
                            await remainingDesk.WaitForAsync(new LocatorWaitForOptions() { Timeout = 3000 });
                            // click on the desk locator
                            await remainingDesk.ClickAsync();
                            await Locators.BookDeskButton(page).ClickAsync();
                            await Locators.OKButton(page).ClickAsync();
                            deskBooked = true;
                            // break out of loop and move onto the next date for booking
                            break;
                        }
                        // if the desk in priority is not found, continue to the next desk in the list
                        catch (Exception)
                        {
                            Console.WriteLine($"Desks not available, trying next.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error selecting booking date: {ex.Message}");
            }
        }
        await browser.CloseAsync();
    }
}
