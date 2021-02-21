using Disarray.Core.Data;

namespace Disarray.Content.Gardening.Beach.CouchPotato
{
    public class CouchPotato : GardeningInformation
    {
        public override void SetDefaults()
        {
            DisplayName = Name;
            DifficultyRating = 0.1f;
            LightRequired = 0.5f;
            Thirstiness = 0.3f;

            LikesAndDislikes.Add("Televisions", 0.75f);
            LikesAndDislikes.Add("Snacks", 0.5f);
            LikesAndDislikes.Add("Pillows", 0.25f);
            LikesAndDislikes.Add("Air", 0f);
            LikesAndDislikes.Add("Disturbances", -0.25f);
            LikesAndDislikes.Add("You", -0.5f);
            LikesAndDislikes.Add("Other Couch Potatos", -0.75f);

            string ForagingTip = "Lazy, found chillaxing at the beach.";
            string GardeningTip = "Long to grow but bountiful.";
            Description = ForagingTip + "\n \n" + GardeningTip;
        }
    }
}