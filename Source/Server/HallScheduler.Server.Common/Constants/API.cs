namespace HallScheduler.Server.Common.Constants
{
    public class API
    {
        // Common
        public const string RequestCannotBeEmpty = "Request cannot be empty or have NULL properties.";
        public const int Single = 1;
        public const int Conflict = -3;
        public const bool Success = true;
        public const bool Failure = false;

        // Routes (Common)
        public const string All = "All";
        public const string Update = "Update";
        public const string Create = "Create";
        public const string Delete = "Delete";

        // Routes (Hall Controller)
        public const string Halls = "api/Halls";
        public const string Schedule = "Schedule";
        public const string FullSchedule = "FullSchedule";

        // Routes (Events Controller)
        public const string Events = "api/Events";
        public const int Overlap = -2;
        public const int InvalidModel = -21;

        // Routes (Identity controller)
        public const string Identity = "api/Identity";

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

        public static string Message(string message)
        {
            return $"{message}";
        }
    }
}