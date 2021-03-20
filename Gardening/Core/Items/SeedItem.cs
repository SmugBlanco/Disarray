using Terraria.ModLoader;

namespace Disarray.Gardening.Core.Items
{
	public abstract class SeedItem : ModItem
	{
        public abstract GardeningInformation GeneralInformation { get; protected set; }
    }
}