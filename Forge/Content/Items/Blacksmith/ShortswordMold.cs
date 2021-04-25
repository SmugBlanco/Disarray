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
				string statistic = "8 base damage, 11 max damage"
				+ "\n4 base critical strike chance"
				+ "\n2 base knockback ( very weak )"
				+ "\n15 base use time and animation ( very fast )";
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
			item.damage = 8 + (int)(3f * quality);
			item.knockBack = 2;
			item.crit = 4;

			item.useStyle = ItemUseStyleID.Stabbing;
			item.useTime = 15;
			item.useAnimation = 15;
		}
	}
}