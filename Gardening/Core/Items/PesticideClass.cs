using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using DustID = Disarray.ID.DustID;
using Disarray.Gardening.Core.GE;
using Disarray.Gardening.Core.UI.Pesticide;
using Disarray.Gardening.Core.Needs.PestTypes;

namespace Disarray.Gardening.Core.Items
{
	public abstract class PesticideClass : GardeningUsableItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault(ItemName);
			Tooltip.SetDefault("Allows you to kill pests"
			+ "\nMax capacity: " + (MaxQuantity * 20f) + " milliliters");
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) => tooltips.Add(new TooltipLine(mod, "GardeningPesticideContent", "Contents: " + (GetQuantity * 20f) + " milliliters. ( " + (100f / MaxQuantity * GetQuantity) + "/100% )"));

		public override void UseDust(Player player)
		{
			int offsetX = (int)(player.direction > 0 ? ItemDimensions.X * 2f : -ItemDimensions.X);
			int offsetY = (int)(ItemDimensions.Y / 5f);
			for (int indexer = -1; indexer < 2; indexer++)
			{
				Dust.NewDustPerfect(player.itemLocation + new Vector2(offsetX + 22 * player.direction, offsetY + Main.rand.Next(-10, 10)), DustID.Cloud, Vector2.Zero, 0, Color.White, 1f).noGravity = true;
				Dust.NewDustPerfect(player.itemLocation + new Vector2(offsetX + 22 * player.direction, offsetY + Main.rand.Next(-10, 10)), DustID.Cloud, Vector2.Zero, 0, Color.White, 1f).noGravity = true;
				Dust.NewDustPerfect(player.itemLocation + new Vector2(offsetX, offsetY), DustID.Cloud, new Vector2(3f * player.direction, 0.75f * indexer), 0, default, 0.75f).noGravity = true;
			}
		}

		public override void OnUse(GardenEntity gardenEntity, Player player)
		{
			for (int indexer = 0; indexer < 6; indexer++)
			{
				Dust.NewDustPerfect(Main.MouseWorld, DustID.Cloud, new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2)), Scale: 1.25f).noGravity = true;
			}

			PlantNeeds need = gardenEntity.Needs.FirstOrDefault(needs => needs is Needs.Pests);

			if (need is Needs.Pests pests)
			{
				if (pests.CurrentPests.Count > 0)
				{
					pests.GetTimer = 0;

					foreach (PestEntity pestEntity in pests.CurrentPests.ToArray())
					{
						if (pestEntity.CanKill(player))
						{
							pestEntity.OnKill();
						}

						pests.CurrentPests.Remove(pestEntity);
					}
				}
			}
		}

		public sealed override void HoldItem(Player player)
		{
			if (ModContent.GetInstance<Disarray>().GardeningInterface.CurrentState == null)
			{
				ModContent.GetInstance<Disarray>().GardeningInterface?.SetState(new PesticideDisplay());
				GetTimeSinceLastInteraction = 0;
			}

			int offsetY = (int)(ItemDimensions.Y / 7.5f);
			player.itemLocation += new Vector2(0, offsetY);
		}

		public sealed override bool CanUseItem(Player player)
		{
			if (GetQuantity <= 0)
			{
				item.UseSound = null;
			}
			else
			{
				item.UseSound = SoundID.Item64;
			}

			return true;
		}

		public sealed override void UseStyle(Player player)
		{
			int offsetY = (int)(ItemDimensions.Y / 3.75f);
			player.itemLocation += new Vector2(0, offsetY);
		}
	}
}