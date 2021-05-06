using Disarray.Core.GlobalPlayers;
using Disarray.Utility;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Vitalium
{
	public class VitaliumStaff : VitaliumItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Flora", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vitalium Staff");
			Item.staff[item.type] = true;
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "13 template damage, 19 base damage, 24 max damage"
				+ "\n3 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(3f, true) + " )"
				+ "\n24 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(24, true) + " )"
				+ "\n8 base shoot speed"
				+ "\n5 mana consumption on use"
				+ "\nConjures bolts of floral energy."
				+ "\nWhen forged, attacks gain a 33% chance to poison enemies, for 3 seconds, as long as the forge item's quality is equal to or above 50%";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults() => item.damage = 13;

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 40;
			item.height = 40;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item7;

			item.magic = true;
			item.damage = 19 + (int)(5f * quality);
			item.knockBack = 3;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 24;
			item.useAnimation = 24;

			item.shoot = ModContent.ProjectileType<VitaliumStaffProjectile>();
			item.shootSpeed = 8;
			item.mana = 5;
		}

		public override void HoldItem(Player player)
		{
			if (ImplementedItem != null && ImplementedItem.Quality >= 0.5f)
			{
				player.GetModPlayer<PoisonPlayer>().PoisonousAttackChance += 0.33f;

				player.GetModPlayer<PoisonPlayer>().PoisonousAttackDuration += 180;
			}
		}
	}
}