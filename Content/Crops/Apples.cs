using Disarray.Core.Data;

namespace Disarray.Content.Crops
{
    public class Apples : Crop
    {
        public override void SetDefaults()
        {
            DisplayName = name;
            Description = "A popular perennial fruit thats widely consume across the world. Flavor usually ranges from sweet to tangy.";
            Origin = "Somewhere in asia lol";
            PlantingMonths = "Anytime the climate is mild and moist";
            HarvestMonths = "Varies by species to species";
            PricePerPound = "Averages between $1 and $1.50 for widely cultivated varities, prices may reach as high as $4 for less common ones";
        }
    }
}