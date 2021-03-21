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

		public const string InfoTexture = "Disarray/Gardening/Content/SwordFern/SwordFern";

		public const string Information = "Found commonly in forests, it's evolved to defend itself against pests with weaponry.";

		public override GardeningInformation GeneralInformation { get; protected set; } = new GardeningInformation(InfoTexture, "Sword Fern", Information, 0.5f, 0.5f, (0, 0.5f));

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