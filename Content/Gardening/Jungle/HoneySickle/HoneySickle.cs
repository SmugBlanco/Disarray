using Disarray.Core.Gardening;

namespace Disarray.Content.Gardening.Jungle.HoneySickle
{
    public class HoneySickle : GardeningInformation
    {
        public override void SetDefaults()
        {
            DisplayName = "Honey Sickle";
            DifficultyRating = 0.3f;
            LightRequired = 0f;
            Thirstiness = 0.5f;
            LiquidType = 2;

            string ForagingTip = "Foraging:\nRare at first sight, but expierenced foragers know to look near liquid honey.";
            string GardeningTip = "Gardening:\nA fairy easy plant to garden producing an useful fruit.";
            Description = ForagingTip + "\n \n" + GardeningTip; 
        }
    }
}