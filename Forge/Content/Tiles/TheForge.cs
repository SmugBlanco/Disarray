using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Disarray.Forge.Core.UI;

namespace Disarray.Forge.Content.Tiles
{
	public class TheForge : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			animationFrameHeight = 36;
			Main.tileFrameImportant[Type] = true;
			Main.tileLighted[Type] = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("The Forge");
			AddMapEntry(new Color(200, 200, 200), name);
			disableSmartCursor = true;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = Main.DiscoR / 130f;
			g = Main.DiscoG / 200f;
			b = Main.DiscoB / 255f;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("TheForge"));

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 8)
			{
				frameCounter = 0;
				frame++;
				if (frame > 3)
				{
					frame = 0;
				}
			}
		}

		public override bool NewRightClick(int i, int j)
		{
			Main.PlaySound(SoundID.MenuOpen);
			Tile tile = Framing.GetTileSafely(i, j);
			Vector2 SpawnTile = new Vector2(i, j).ToWorldCoordinates() - new Vector2(tile.frameX, tile.frameY) + new Vector2(8, 8);
			Main.playerInventory = true;
			ModContent.GetInstance<Disarray>().ForgeUserInterface.SetState(new ForgeUI(SpawnTile));
			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.showItemIcon2 = ModContent.ItemType<Items.TheForge>();
			player.showItemIcon = true;
		}
	}
}