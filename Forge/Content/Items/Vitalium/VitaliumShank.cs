using Disarray.Core.GlobalPlayers;
using Disarray.Utility;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Vitalium
{
	public class VitaliumShank : VitaliumItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Flora", 1f } };

		public override void SetStaticDefaults() => DisplayName.SetDefault("Vitalium Shank");

		public override string ItemStatistics
		{
			get
			{
				string statistic = "8 template damage, 13 base damage, 16 max damage"
				+ "\n2 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(2f, true) + " )"
				+ "\n14 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(14, true) + " )"
				+ "\nWhen forged, attacks gain a 33% chance to poison enemies, for 3 seconds, as long as the forge item's quality is equal to or above 50%";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults() => item.damage = 8;

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;

			item.melee = true;
			item.damage = 13 + (int)(3f * quality);
			item.knockBack = 2;

			item.useStyle = ItemUseStyleID.Stabbing;
			item.useTime = 14;
			item.useAnimation = 14;
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