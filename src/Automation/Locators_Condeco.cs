using Microsoft.Playwright;
using CondecoAssistant.Helpers;
using CondecoAssistant.Models;

public static class Locators_Condeco
{
    // Sign in page
    public static ILocator EmailField(IPage page) =>
        page.GetByRole(AriaRole.Textbox, new() { Name = "someone@example.com" });
    public static ILocator SignInPageNextButton(IPage page) =>
        page.GetByRole(AriaRole.Button, new() { Name = "Next" });
    public static ILocator PasswordField(IPage page) =>
        page.GetByRole(AriaRole.Textbox, new() { Name = "Enter the password for slone@" });
    public static ILocator SignInPage_SignInButton(IPage page) =>
        page.GetByRole(AriaRole.Button, new() { Name = "Sign in" });

    // Left Navigation Menu
    public static ILocator PersonalSpacesButton(IPage page) =>
        page.FrameLocator("iframe[name=\"leftNavigation\"]")
            .GetByRole(AriaRole.Button, new() { Name = "Personal Spaces" });

    public static ILocator BookAPersonalSpaceButton(IPage page) =>
        page.FrameLocator("iframe[name=\"leftNavigation\"]")
            .GetByRole(AriaRole.Link, new() { Name = "Book a personal space" });

    public static ILocator CountryDropdown(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByLabel("Country");

    public static ILocator LocationDropdown(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByLabel("Location");

    public static ILocator GroupDropdown(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByLabel("Group");

    public static ILocator FloorDropdown(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByLabel("Floor");

    public static ILocator WorkspaceTypeDropdown(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByText("Workspace type Desk");

    public static ILocator BookingDateOne(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByText("21", new() { Exact = true });

    public static ILocator BookingDateTwo(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByText("21", new() { Exact = true });

    public static ILocator BookingDateThree(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByText("21", new() { Exact = true });

    public static ILocator SearchButton(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByRole(AriaRole.Button, new() { Name = "Search" });




    // Home page

    public static ILocator OneYorkLocationOption(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByText("One York StreetLocation");


    public static ILocator ITGroupOption(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByText("ITGroup");

    public static ILocator SixteenthFloorOption(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByText("17Floor");

    public static ILocator WorkspaceType(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByLabel("Workspace type");
    public static ILocator DeskWorkspaceOption(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByText("Desk Workspace type");

    public static ILocator CalendarButton(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByRole(AriaRole.Button, new() { Name = "Calendar" });
    public static ILocator BookingDate(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByRole(AriaRole.Button, new() { Name = "21/04/" });
}