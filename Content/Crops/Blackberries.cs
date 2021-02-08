using Disarray.Core.Data;

namespace Disarray.Content.Crops
{
    public class Blackberries : Crop
    {
        public override void SetDefaults()
        {
            DisplayName = name;
            Description = "A perennials berry with similar appearance to raspberries";
            Origin = "???";
            PlantingMonths = "Spring or a mild fall, cooler climates may kill some varieties";
            HarvestMonths = "Every few days in late-spring to early-summer";
            PricePerPound = "Averages between $3 and $4 from standard retailers";
        }
    }
}