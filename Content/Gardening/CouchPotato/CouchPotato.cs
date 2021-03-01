using Disarray.Core.Gardening;

namespace Disarray.Content.Gardening.CouchPotato
{
    public class CouchPotato : GardeningInformation
    {
        public override void SetDefaults()
        {
            DisplayName = "Couch Potatos";
            DifficultyRating = 0.333f;
            LightRequired = 0.75f;
            Thirstiness = 0.2f;

            string ForagingTip = "Foraging:\nThese lazy plants can be found setting up home on beaches.";
            string GardeningTip = "Gardening:\nThe Couch Potato is a fairy easy and bountiful plant to garden, though it's slow rate of growth may discourage those who want to make a quick buck.";
            Description = ForagingTip + "\n \n" + GardeningTip;
        }
    }
}