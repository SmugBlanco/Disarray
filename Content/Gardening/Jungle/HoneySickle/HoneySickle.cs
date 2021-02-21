using Disarray.Core.Data;

namespace Disarray.Content.Gardening.Jungle.HoneySickle
{
    public class HoneySickle : GardeningInformation
    {
        public override void SetDefaults()
        {
            DisplayName = Name;
            DifficultyRating = 0.425f;
            LightRequired = 0f;
            Thirstiness = 0.75f;
            string ForagingTip = "A plant that may seem rare at first, but so not much if the forager knows where to look. Check grassy areas with a nearby source of honey.";
            string GardeningTip = "Honey Sickles are suprisingly easy plants to garden, they only require one condition to prosper: a nearby source of honey.";
            Description = ForagingTip + "\n \n" + GardeningTip; 
        }
    }
}