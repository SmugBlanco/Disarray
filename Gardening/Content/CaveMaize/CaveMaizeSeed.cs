using Terraria.ModLoader;
using Terraria.ID;
using Disarray.Gardening.Core.Items;
using Disarray.Gardening.Core;

namespace Disarray.Gardening.Content.CaveMaize
{
	public class CaveMaizeSeed : SeedItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cave Maize Seed");
		}

		public override GardeningInformation GeneralInformation { get; protected set; } = new GardeningInformation("Disarray/Gardening/Content/CaveMaize/CaveMaize", "Cave Maize", "Testing", 1f, 0, (0, 5));

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 24;
			item.maxStack = 999;
			item.createTile = ModContent.TileType<CaveMaizePlant>();

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item1;
			item.useTime = 15;
			item.useAnimation = 15;

			item.autoReuse = true;
			item.consumable = true;
		}
	}
}