using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Disarray.Core.Gardening.Tiles;
using Terraria.DataStructures;

namespace Disarray.Content.Gardening.Items
{
	public class GardeningHelper : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gardening Helper");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.Item1;
			item.useTime = 60;
			item.useAnimation = 60;
		}

        public override bool UseItem(Player player)
        {
			if (player.itemAnimation == player.itemAnimationMax - 1)
			{
				Point16 point = new Point16((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16));
				Tile tile = Framing.GetTileSafely(point);
				if (ModContent.GetModTile(tile.type) is FloraBase flora)
				{
					Main.NewText("Displaying information for: " + flora.Name);
					Main.NewText("Basic Liquid Information: Minimum Liquid Radius: " + flora.MinimumLiquidRadius + " | Liquid Type: " + flora.RequiredLiquidType);
					Main.NewText("Fufills liquid requirements?: " + flora.LiquidCheck(new Rectangle(point.X, point.Y, flora.Width / 18, flora.Height / 18), flora.MinimumLiquidRadius, flora.RequiredLiquidType));
					Main.NewText("Minimum Light Level: " + flora.MinimumLightLevel);
					Vector3 subLight = Lighting.GetSubLight(Main.MouseWorld);
					float averagedLighting = (subLight.X + subLight.Y + subLight.Z) / 3 * 1.2f;
					if (averagedLighting > 1)
					{
						averagedLighting = 1;
					}
					Main.NewText("Current Light Level: " + averagedLighting);
					Main.NewText("Has Met Light Needs: " + flora.LightCheckWText(point.X, point.Y));
					Main.NewText("Has Met Programmed Basics Needs: " + flora.BasicNecessities(point.X, point.Y));
					Main.NewText("Can grow/survive here?: " + flora.HasMetBasicNecessities(point.X, point.Y));
				}
			}
            return base.UseItem(player);
        }
	}
}