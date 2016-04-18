namespace HallScheduler.Data.Migrations.Seed.Seeders
{
    using Common.Enumerations;
    using Contexts;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class HallsSeeder : ISeeder
    {
        public int Priority { get; set; } = 3;

        public Random Generator { get; set; } = new Random();

        public void Seed(HallSchedulerDbContext context)
        {
            if (!context.Halls.Any())
            {
                var hallTypes = Enum.GetValues(typeof(HallType))
                    .Cast<HallType>()
                    .ToList();
                var blockTypes = Enum.GetValues(typeof(BlockType))
                    .Cast<BlockType>()
                    .ToList();
                var stageTypes = Enum.GetValues(typeof(StageType))
                    .Cast<StageType>()
                    .ToList();
                var roomTypes = Enum.GetValues(typeof(RoomType))
                    .Cast<RoomType>()
                    .ToList();

                for (int blockType = 0; blockType < blockTypes.Count - 2; blockType++)
                {
                    for (int stageType = 0; stageType < stageTypes.Count - 1; stageType++)
                    {
                        for (int roomType = 0; roomType < roomTypes.Count; roomType++)
                        {
                            var hallType = this.Generator.Next(0, hallTypes.Count);

                            var hall = new Hall()
                            {
                                Type = hallTypes[hallType],
                                Block = blockTypes[blockType],
                                Stage = stageTypes[stageType],
                                Room = roomTypes[roomType]
                            };

                            context.Halls.AddOrUpdate(hall);
                        }

                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
