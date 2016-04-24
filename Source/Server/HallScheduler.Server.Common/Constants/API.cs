namespace HallScheduler.Server.Common.Constants
{
    public class API
    {
        // Common
        public const string RequestCannotBeEmpty = "Request cannot be empty or have NULL properties.";
        public const int Single = 1;
        public const bool Success = true;
        public const bool Failure = false;

        // Routes (Common)
        public const string All = "All";

        // Routes (Hall controller)
        public const string Halls = "api/Halls";
        public const string Schedule = "Schedule";
        public const string FullSchedule = "FullSchedule";

        public static string ReturnedItems(int count)
        {
            if (count == 1)
            {
                return $"Returned {count} item";
            }
            else
            {
                return $"Returned {count} items";
            }
        }
    }
}