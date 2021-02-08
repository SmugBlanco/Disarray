using Disarray.Core.Data;

namespace Disarray.Content.Crops
{
    public class Oranges : Crop
    {
        public override void SetDefaults()
        {
            DisplayName = name;
            Description = "A popular perennial citrus fruit thats widely consume across the world. Flavor usually ranges from sweet to tangy.";
            Origin = "Somewhere in asia lol";
            PlantingMonths = "Early to mid-spring";
            HarvestMonths = "Traditionally winter";
            PricePerPound = "Averages between $1 and $1.50 for widely cultivated varities, prices may reach as high as $2.50 for less common ones";
        }
    }
}