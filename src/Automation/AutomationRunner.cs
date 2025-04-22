using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using CondecoAssistant.Helpers;
using CondecoAssistant.Models;


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

        // Initialize Playwright
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();
        Console.WriteLine("Opened Chrome browser.");

        // Navigate to Condeco website
        await page.GotoAsync("https://hoopp.condecosoftware.com");
        Console.WriteLine("Navigated to Condeco login page.");

        await Locators_Condeco.EmailField(page).WaitForAsync();
        await Locators_Condeco.EmailField(page).ClickAsync();
        await Locators_Condeco.EmailField(page).FillAsync(username);
        Console.WriteLine("Entered email address.");
        await Locators_Condeco.SignInPageNextButton(page).ClickAsync();
        Console.WriteLine("Clicked on 'Next' button.");

        await Locators_Condeco.PasswordField(page).WaitForAsync();
        await Locators_Condeco.PasswordField(page).ClickAsync();
        await Locators_Condeco.PasswordField(page).FillAsync(password);
        Console.WriteLine("Entered password.");

        await Locators_Condeco.SignInPage_SignInButton(page).ClickAsync();
        Console.WriteLine("Clicked on 'Sign in' button.");

        await Locators_Condeco.PersonalSpacesButton(page).ClickAsync();
        Console.WriteLine("Clicked on 'Personal Spaces' button.");

        await Locators_Condeco.BookAPersonalSpaceButton(page).ClickAsync();
        Console.WriteLine("Clicked on 'Book a Personal Space' button.");

        await Locators_Condeco.CountryDropdown(page).ClickAsync();
        Console.WriteLine("Clicked on 'Country' dropdown.");

        await Locators_Condeco.LocationDropdown(page).ClickAsync();
        Console.WriteLine("Clicked on 'Location' dropdown.");

        await Locators_Condeco.GroupDropdown(page).ClickAsync();
        Console.WriteLine("Clicked on 'Group' dropdown.");

        await Locators_Condeco.FloorDropdown(page).ClickAsync();
        Console.WriteLine("Clicked on 'Floor' dropdown.");

        await Locators_Condeco.WorkspaceTypeDropdown(page).ClickAsync();
        Console.WriteLine("Clicked on 'Workspace Type' dropdown.");
        await page.PauseAsync();

        try
        {
            if (Locators_Condeco.BookingDates(page)[0] != null)
            {
                await Locators_Condeco.BookingDates(page)[0].ClickAsync();
            }
            if(Locators_Condeco.BookingDates(page)[1] != null)
            {
                await Locators_Condeco.BookingDates(page)[1].ClickAsync();
            }
            if (Locators_Condeco.BookingDates(page)[2] != null)
            {
                await Locators_Condeco.BookingDates(page)[2].ClickAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error selecting booking dates: {ex.Message}");
        }

        await page.PauseAsync();

        //await Locators.BookingDateTwo(page).ClickAsync();
        //Console.WriteLine("Selected 'Booking Date Two'.");

        //await Locators.BookingDateThree(page).ClickAsync();
        //Console.WriteLine("Selected 'Booking Date Three'.");

        await Locators_Condeco.SearchButton(page).ClickAsync();
        Console.WriteLine("Clicked on 'Search' button.");
        await page.PauseAsync();

        await Locators_Desks.W103(page).ClickAsync();
        Console.WriteLine("Clicked on 'W103' desk.");
        await page.PauseAsync();

        await Locators_Desks.BookDeskButton(page).ClickAsync();
        Console.WriteLine("Booked the desk.");
        await page.PauseAsync();

        await Locators_Desks.OKButton(page).ClickAsync();
        Console.WriteLine("Clicked on 'OK' button.");
        await page.PauseAsync();

        await browser.CloseAsync();
    }
}
