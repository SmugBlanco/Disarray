using Terraria.ModLoader;
using Terraria.ID;
using Disarray.Gardening.Core.Items;
using Disarray.Gardening.Core;

namespace Disarray.Gardening.Content.SwordFern
{
	public class SwordFernSeed : SeedItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sword Fern Seed");
		}

		public override GardeningInformation GeneralInformation { get; protected set; } = new GardeningInformation("Disarray/Gardening/Content/SwordFern/SwordFern", "Sword Fern", "Testing", 5f, 7.5f, (0, 1f));

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
			item.createTile = ModContent.TileType<SwordFernPlant>();

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item1;
			item.useTime = 15;
			item.useAnimation = 15;

			item.autoReuse = true;
			item.consumable = true;
		}
	}
}