using Disarray.Core.Data;

namespace Disarray.Content.Crops
{
    public class Asparagus : Crop
    {
        public override void SetDefaults()
        {
            DisplayName = name;
            Description = "A perennial vegetable with a distinct flavor and according to some, effect on urine.";
            Origin = "Somewhere in western coastal europe";
            PlantingMonths = "Early spring or fall";
            HarvestMonths = "Minimum of two years after planting; anytime in spring but before July. Cut off spears when they are 6 - 10 inches above the soil line.";
            PricePerPound = "Averages between $2 and $4 from standard retailers";
        }
    }
}