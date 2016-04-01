namespace HallScheduler.Data.Common.Constants
{
    public class ValidationConstants
    {
        // User
        public const int FirstNameMaxLength = 50;
        public const int FirstNameMinLength = 2;
        public const string FirstNameMaxLengthErrorMessage = "First name max length cannot exceed 50 characters.";
        public const string FirstNameMinLengthErrorMessage = "First name min length cannot be less than 2 characters.";

        public const int LastNameMaxLength = 50;
        public const int LastNameMinLength = 2;
        public const string LastNameMaxLengthErrorMessage = "Last name length cannot exceed 50 characters.";
        public const string LastNameMinLengthErrorMessage = "Last name length cannot be less than 2 characters.";

        public const int UsernameMaxLength = 50;
        public const int UsernameMinLength = 3;
        public const string UsernameMaxLengthErrorMessage = "Username length cannot exceed 50 characters.";
        public const string UsernameMinLengthErrorMessage = "Username length cannot be less than 3 characters.";

        public const int EmailMinLength = 3;
        public const string EmailMinLengthErrorMessage = "Email length cannot be less than 3 characters.";

        public const string AcademicRankTypeErrorMessage = "AcademicRank must be an enum of type \"AcademicRank\"";
        public const string FacultyTypeErrorMessage = "Faculty must be an enum of type \"FacultyType\"";

        // Event
        public const string EventStartingHourErrorMessage = "EventStartingHour must be in the following format - \"HH:MM\"";
        public const string EventDayOfWeekErrorMessage = "EventDayOfWeek must be an enum of type \"DayOfWeek\"";
        public const string EventDurationErrorMessage = "EventDurationInMinutes must be a number between 0 and 1440 inclusive";
        public const string EventStartsAtErrorMessage = "EventStartsAt must be a number between 0 and 1440 inclusive";

        public const string EventTopicMinLengthErrorMessage = "EventTopic length cannot be less than 2 characters.";
        public const string EventTopicMaxLengthErrorMessage = "EventTopic length cannot exceed 240 characters.";
        public const int EventTopicMinLength = 2;
        public const int EventTopicMaxLength = 240;

        public const int EventStartsAtMinValue = 0;
        public const int EventStartsAtMaxValue = 1440;

        public const int EventDurationMinLength = 0;
        public const int EventDurationMaxLength = 1440;

        // Hall
        public const int HallRoomMinValue = 0;
        public const int HallRoomMaxValue = 300;

        public const string HallRoomValueErrorMessage = "HallRoom must be a number between 0 and 300 inclusive";
        public const string HallTypeErrorMessage = "HallType must be an enum of type \"HallType\"";
        public const string HallBlockTypeErrorMessage = "HallBlockType must be an enum of type \"BlockType\"";
        public const string HallStageTypeErrorMessage = "HallStageType must be an enum of type \"StageType\"";
    }
}