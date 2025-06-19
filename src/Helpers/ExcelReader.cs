
using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;

namespace CondecoAssistant.Helpers;

public static class ExcelReader
{
    public static List<DeskBooking> ReadBookings(string filePath)
    {
        var bookings = new List<DeskBooking>();

        using var workbook = new XLWorkbook(filePath);
        var worksheet = workbook.Worksheet(1); // Assuming data is in the first sheet
        var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip header row

        foreach (var row in rows)
        {
            var name = row.Cell(5).GetString();
            var email = row.Cell(4).GetString();
            var desk = row.Cell(9).GetString();
            var daysRaw = row.Cell(8).GetString();
            var days = daysRaw.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            bookings.Add(new DeskBooking
            {
                Name = name,
                Email = email,
                Desk = desk,
                Days = new List<string>(days)
            });
        }

        return bookings;
    }
}
