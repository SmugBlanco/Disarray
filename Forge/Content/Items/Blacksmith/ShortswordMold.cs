using Disarray.Utility;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Blacksmith
{
	public class ShortswordMold : BlacksmithItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, null, null);

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults() => DisplayName.SetDefault("Shortsword Mold");

		public override string ItemStatistics
		{
			get
			{
				string statistic = "10 base damage, 13 max damage"
				+ "\n2 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(2f, true) + " )"
				+ "\n15 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(15, true) + " )";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.maxStack = 999;

			item.useStyle = 0;
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 34;
			item.height = 34;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;

			item.melee = true;
			item.damage = 10 + (int)(3f * quality);
			item.knockBack = 2;

			item.useStyle = ItemUseStyleID.Stabbing;
			item.useTime = 15;
			item.useAnimation = 15;
		}
	}
}