namespace CondecoAssistant.Helpers
{
    public static class BookingHelper
    {
        public static Dictionary<string, List<DeskBooking>> GroupByDay(List<DeskBooking> bookings)
        {
            var grouped = new Dictionary<string, List<DeskBooking>>
            {
                { "Monday", new List<DeskBooking>() },
                { "Tuesday", new List<DeskBooking>() },
                { "Wednesday", new List<DeskBooking>() },
                { "Thursday", new List<DeskBooking>() },
                { "Friday", new List<DeskBooking>() }
            };

            foreach (var booking in bookings)
            {
                foreach (var day in booking.Days)
                {
                    if (grouped.ContainsKey(day))
                    {
                        grouped[day].Add(booking);
                    }
                }
            }

            return grouped;
        }
    }
}
