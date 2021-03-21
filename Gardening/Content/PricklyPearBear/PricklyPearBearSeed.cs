using Terraria.ModLoader;
using Terraria.ID;
using Disarray.Gardening.Core.Items;
using Disarray.Gardening.Core;

namespace Disarray.Gardening.Content.PricklyPearBear
{
	public class PricklyPearBearSeed : SeedItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Prickly Pear Bear Seed");
		}

		public const string InfoTexture = "Disarray/Gardening/Content/PricklyPearBear/PricklyPearBear";

		public const string Information = "An unique cacti species with resilence to droughts.";

		public override GardeningInformation GeneralInformation { get; protected set; } = new GardeningInformation(InfoTexture, "Prickly Pear Bear", Information, 0.4f, 0.5f, (0, 0.1f));

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
			item.createTile = ModContent.TileType<PricklyPearBearPlant>();

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item1;
			item.useTime = 15;
			item.useAnimation = 15;

			item.autoReuse = true;
			item.consumable = true;
		}
	}
}