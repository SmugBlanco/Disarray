using Disarray.Forge.Core;
using Disarray.Utility;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Vitalium
{
	public class VitaliumClub : VitaliumItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Flora", 1f } };

		public override void SetStaticDefaults() => DisplayName.SetDefault("Vitalium Club");

		public override string ItemStatistics
		{
			get
			{
				string statistic = "25 template damage, 37 base damage, 46 max damage"
				+ "\n8 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(8f, true) + " )"
				+ "\n42 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(42, true) + " )"
				+ "\nWhen forged, attacks gain a 33% chance to poison enemies, for 3 seconds, as long as the forge item's quality is equal to or above 50%";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults() => item.damage = 25;
		
		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 62;
			item.height = 64;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;

			item.melee = true;
			item.damage = 37 + (int)(9f * quality);
			item.knockBack = 8;

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 42;
			item.useAnimation = 42;
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