using Disarray.Core.Data;

namespace Disarray.Content.Crops
{
    public class Beets : Crop
    {
        public override void SetDefaults()
        {
            DisplayName = name;
            Description = "An annual vegetable who's roots and leaves can be eaten";
            Origin = "???";
            PlantingMonths = "Spring or fall";
            HarvestMonths = "Around two months after planting, some variants may mature as quickly as 45 days";
            PricePerPound = "Averages between $2 and $3 from standard retailers";
        }
    }
}