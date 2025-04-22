# CondecoAssistant
A simple automation tool that securely logs into Condeco, schedules desk bookings, and ensures you get your preferred desk at the earliest available time. Ideal for teams facing booking challenges. Made for Pension Core, by Pension Core :)

## Building the WPF app

For some reason, WPF App (.Net Core) does not appear in the list of project templates in Visual Studio 2022. To create a WPF app, you need to use the command line.

```dotnet new wpf -n CondecoAssistant -f net8.0 --no-restore```
```cd C:\Projects\CondecoAssistant```
```dotnet new sln -n CondecoAssistant```
```dotnet sln add CondecoAssistant.csproj```