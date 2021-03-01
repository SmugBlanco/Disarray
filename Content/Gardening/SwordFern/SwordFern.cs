using Disarray.Core.Gardening;

namespace Disarray.Content.Gardening.SwordFern
{
    public class SwordFern : GardeningInformation
    {
        public override void SetDefaults()
        {
            DisplayName = "Sword Fern";
            DifficultyRating = 0.4f;
            LightRequired = 0.75f;
            Thirstiness = 0.5f;

            string ForagingTip = "Foraging:\nSword Ferns can be commonly found growing in forests.";
            string GardeningTip = "Gardening:\nA fairly standard plant to garden; it's blades, which sprout at maturity, can be collected for many uses.";
            Description = ForagingTip + "\n \n" + GardeningTip;
        }
    }
}