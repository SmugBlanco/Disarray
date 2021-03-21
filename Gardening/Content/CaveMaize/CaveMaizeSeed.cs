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

		public const string InfoTexture = "Disarray/Gardening/Content/CaveMaize/CaveMaize";

		public const string Information = "A species native to the undergrounds, it bears a striking resemblence to the maize that we eat. Also, the plant seems to have evolutionary discarded it's need for light.";

		public override GardeningInformation GeneralInformation { get; protected set; } = new GardeningInformation(InfoTexture, "Cave Maize", Information, 0.4f, 0, (0, 0.75f));

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