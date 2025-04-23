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

        List<string> days = prefs.SelectedDays;
        List<string> desks = prefs.SelectedDesksInPriority;

        //MessageBox.Show(string.Join("\n", desks), "Selected Desks", MessageBoxButton.OK, MessageBoxImage.Information);


        // Initialize Playwright
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

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

        foreach (var dateLocator in bookingDateLocators)
        {
            try
            {
                // Always reset to home page
                await Locators.HomePageButton(page).ClickAsync();
                // Home page left side navigation menu
                await Locators.PersonalSpacesButton(page).ClickAsync();
                await Locators.BookAPersonalSpaceButton(page).ClickAsync();
                // Book a personal space page
                await Locators.CountryDropdown(page).ClickAsync();
                await Locators.LocationDropdown(page).ClickAsync();
                await Locators.GroupDropdown(page).ClickAsync();
                await Locators.FloorDropdown(page).ClickAsync();
                await Locators.WorkspaceTypeDropdown(page).ClickAsync();
                // Select booking date
                await dateLocator.ClickAsync();
                await page.PauseAsync();
                // Search for desks
                await Locators.SearchButton(page).ClickAsync();
                bool deskBooked = false;
                foreach (var deskLocator in deskLocators)
                {
                    try
                    {
                        //Clipboard.SetText(string.Join("\n", deskLocator));
                        //MessageBox.Show(string.Join("\n", deskLocator), "Selected Desks", MessageBoxButton.OK, MessageBoxImage.Information);
                        // test to see if the desklocator hardcode works
                        //await Locators.TestLocator(page).ClickAsync();
                        await deskLocator.WaitForAsync(new LocatorWaitForOptions() {  Timeout = 3000 });
                        await deskLocator.ClickAsync();
                        await Locators.BookDeskButton(page).ClickAsync();
                        await Locators.OKButton(page).ClickAsync();
                        deskBooked = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Desks not available, trying next.");
                    }
                }
                if (!deskBooked)
                {
                    Console.WriteLine($"No desks available for {dateLocator}.");
                }
                await page.WaitForTimeoutAsync(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error selecting booking date: {ex.Message}");
            }
        }
        await browser.CloseAsync();
    }
}
