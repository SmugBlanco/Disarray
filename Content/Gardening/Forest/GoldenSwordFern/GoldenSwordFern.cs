using Disarray.Core.Gardening;

namespace Disarray.Content.Gardening.Forest.GoldenSwordFern
{
    public class GoldenSwordFern : GardeningInformation
    {
        public override void SetDefaults()
        {
            DisplayName = "Golden Sword Fern";
            DifficultyRating = 0.55f;
            LightRequired = 0.75f;
            Thirstiness = 0.5f;

            string ForagingTip = "Foraging:\nGolden Sword Ferns can be rarely found growing in forests, given that the Wall of Flesh has been defeated.";
            string GardeningTip = "Gardening:\nA fairly standard plant to garden; it's blades, which sprout at maturity, can be collected for many uses.";
            Description = ForagingTip + "\n \n" + GardeningTip;
        }
    }
}