namespace HallScheduler.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;
    using Common.Enumerations;

    public class Hall
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EnumDataType(
            typeof(HallType), 
            ErrorMessage = ValidationConstants.HallTypeErrorMessage)]
        public HallType Type { get; set; }

        [Required]
        [EnumDataType(
            typeof(BlockType), 
            ErrorMessage = ValidationConstants.HallBlockTypeErrorMessage)]
        public BlockType Block { get; set; }

        [Required]
        [EnumDataType(
            typeof(StageType), 
            ErrorMessage = ValidationConstants.HallStageTypeErrorMessage)]
        public StageType Stage { get; set; }

        [Required]
        [EnumDataType(
            typeof(RoomType),
            ErrorMessage = ValidationConstants.HallRoomValueErrorMessage)]
        public RoomType Room { get; set; }

        public virtual ICollection<Event> Schedule { get; set; }
    }
}
