﻿namespace HallScheduler.Server.Common.Constants
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
        public const string Update = "Update";

        // Routes (Hall Controller)
        public const string Halls = "api/Halls";
        public const string Schedule = "Schedule";
        public const string FullSchedule = "FullSchedule";

        // Routes (Events Controller)
        public const string Events = "api/Events";

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
    }
}