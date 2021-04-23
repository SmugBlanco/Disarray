//using Disarray.Forge.Content.Items.Desert;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Reagents.Desert
{
	public class Artifact : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Artifact");
			Tooltip.SetDefault("'Created near the dawn of civilization'"
			+ "\nRight click to uncover the secrets within...");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 34;
			item.maxStack = 999;
			item.rare = ItemRarityID.Orange;
			item.value = 10000;

			item.useTime = 0;
			item.useAnimation = 60;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.consumable = true;
		}

		public override bool AltFunctionUse(Player player) => true;

		public override bool UseItem(Player player)
		{
			if (player.itemAnimation == player.itemAnimationMax - 1)
			{
				Main.PlaySound(SoundID.Item74, player.Center);
			}

			if (player.itemAnimation % 3 == 0)
			{
				int maxCount = 8;
				for (int indexer = 0; indexer < maxCount; indexer++)
				{
					float rotation = MathHelper.ToRadians(360 / maxCount * indexer) + MathHelper.ToRadians(360 / player.itemAnimationMax * player.itemAnimation);
					Vector2 offsetToHand = new Vector2(4 * player.direction, 2);
					Vector2 offset = new Vector2((player.itemAnimationMax - player.itemAnimation) / 5).RotatedBy(rotation);
					Dust dust = Dust.NewDustDirect(player.Center + offsetToHand + offset, 0, 0, ID.DustID.Amber);
					float speed = 3;
					dust.velocity = new Vector2(speed).RotatedBy(rotation);
					dust.noGravity = true;
				}
			}

			if (player.itemAnimation == 1)
			{
				//player.QuickSpawnItem(Utils.SelectRandom(Main.rand, ModContent.ItemType<Ankh>(), ModContent.ItemType<Wadjet>(), ModContent.ItemType<Djed>(), ModContent.ItemType<Tyet>(), ModContent.ItemType<Lotus>(), ModContent.ItemType<Shen>(), ModContent.ItemType<Seba>(), ModContent.ItemType<Amenta>(), ModContent.ItemType<Iteru>()));
				return true;
			}
			return false;
		}
	}
}