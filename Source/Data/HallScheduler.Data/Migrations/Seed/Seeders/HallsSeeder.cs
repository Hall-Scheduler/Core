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

                for (int i = 1; i <= 160; i++)
                {
                    var hall = new Hall()
                    {
                        Type = hallTypes[this.Generator.Next(0, hallTypes.Count)],
                        Block = blockTypes[this.Generator.Next(0, blockTypes.Count)],
                        Stage = stageTypes[this.Generator.Next(0, stageTypes.Count)],
                        Room = i
                    };

                    context.Halls.AddOrUpdate(hall);
                }

                context.SaveChanges();
            }
        }
    }
}
