using Disarray.Core.GlobalPlayers;
using Disarray.Utility;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Vitalium
{
	public class VitaliumBow : VitaliumItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Flora", 1f } };

		public override void SetStaticDefaults() => DisplayName.SetDefault("Vitalium Bow");

		public override string ItemStatistics
		{
			get
			{
				string statistic = "8 template damage, 13 base damage, 16 max damage"
				+ "\n1 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(1f, true) + " )"
				+ "\n22 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(22, true) + " )"
				+ "\n7.5 base shoot speed"
				+ "\nWhen forged, attacks gain a 33% chance to poison enemies, for 3 seconds, as long as the forge item's quality is equal to or above 50%";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults() => item.damage = 12;

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 34;
			item.height = 60;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item5;

			item.ranged = true;
			item.damage = 13 + (int)(3f * quality);
			item.knockBack = 1;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 22;
			item.useAnimation = 22;

			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.shootSpeed = 7.5f;
			item.useAmmo = AmmoID.Arrow;
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