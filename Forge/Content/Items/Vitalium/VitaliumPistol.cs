using Disarray.Core.GlobalPlayers;
using Disarray.Utility;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Vitalium
{
	public class VitaliumPistol : VitaliumItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Flora", 1f } };

		public override void SetStaticDefaults() => DisplayName.SetDefault("Vitalium Pistol");

		public override string ItemStatistics
		{
			get
			{
				string statistic = "12 template damage, 18 base damage, 22 max damage"
				+ "\n1 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(1f, true) + " )"
				+ "\n29 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(29, true) + " )"
				+ "\n12 base shoot speed"
				+ "\nWhen forged, attacks gain a 33% chance to poison enemies, for 3 seconds, as long as the forge item's quality is equal to or above 50%";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults() => item.damage = 12;

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 38;
			item.height = 22;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item11;

			item.ranged = true;
			item.damage = 18 + (int)(4f * quality);
			item.knockBack = 1f;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 29;
			item.useAnimation = 29;

			item.shoot = ProjectileID.Bullet;
			item.shootSpeed = 12;
			item.useAmmo = AmmoID.Bullet;
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