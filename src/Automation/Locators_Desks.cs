using Microsoft.Playwright;
using CondecoAssistant.Helpers;
using CondecoAssistant.Models;

public static class Locators_Desks
{
    // Book desk button
    public static ILocator BookDeskButton(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "Book" });

    // OK button
    public static ILocator OKButton(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "OK" });

    // 100 - 107
    public static ILocator W100(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W100 }}Desk!!" });

    public static ILocator W101(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W101 }}Desk!!" });

    public static ILocator W102(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W102 }}Desk!!" });

    public static ILocator W103(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W103 }}Desk!!" });

    public static ILocator W104(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W104 }}Desk!!" });

    public static ILocator W105(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W105 }}Desk!!" });

    public static ILocator W106(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W106 }}Desk!!" });

    public static ILocator W107(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W107 }}Desk!!" });

    // 108 - 115
    public static ILocator W109(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W109 }}Desk!!" });

    public static ILocator W110(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W110 }}Desk!!" });

    public static ILocator W111(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W111 }}Desk!!" });

    public static ILocator W112(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W112 }}Desk!!" });

    public static ILocator W113(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W113 }}Desk!!" });

    public static ILocator W114(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W114 }}Desk!!" });

    public static ILocator W115(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W115 }}Desk!!" });

    // 116 - 119

    public static ILocator W116(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W116 }}Desk!!" });

    public static ILocator W117(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W117 }}Desk!!" });

    public static ILocator W118(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W118 }}Desk!!" });

    public static ILocator W119(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W119 }}Desk!!" });

    // 200 - 203

    public static ILocator W200(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W200 }}Desk!!" });

    public static ILocator W201(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W201 }}Desk!!" });

    public static ILocator W202(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W202 }}Desk!!" });

    public static ILocator W203(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W203 }}Desk!!" });

    // 204 - 210
    public static ILocator W204(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W204 }}Desk!!" });

    public static ILocator W205(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W205 }}Desk!!" });

    public static ILocator W206(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W206 }}Desk!!" });

    public static ILocator W207(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W207 }}Desk!!" });

    public static ILocator W208(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W208 }}Desk!!" });

    public static ILocator W209(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W209 }}Desk!!" });

    public static ILocator W210(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W210 }}Desk!!" });
}
