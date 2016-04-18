namespace HallScheduler.Server.Infrastructure.Helpers
{
    public static class ExtensionMethods
    {
        public static string ToTime(this int value)
        {
            var hoursPart = (value / 60).ToString().PadLeft(2, '0');
            var minutesPart = (value % 60).ToString().PadRight(2, '0');

            return $"{hoursPart}:{minutesPart}";
        }
    }
}