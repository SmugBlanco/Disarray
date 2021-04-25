using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Blacksmith
{
	public class BowMold : BlacksmithItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, null, null);

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override string ItemStatistics
		{
			get
			{
				string statistic = "8 base damage, 11 max damage"
				+ "\n4 base critical strike chance"
				+ "\n1 base knockback ( extremely weak )"
				+ "\n30 base use time and animation ( average )"
				+ "\n7 base shoot speed";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void SetStaticDefaults() => DisplayName.SetDefault("Bow Mold");

		public override void NonProductDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.maxStack = 999;

			item.useStyle = 0;

			item.useAmmo = AmmoID.None;
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 22;
			item.height = 42;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item5;

			item.ranged = true;
			item.damage = 8 + (int)(3f * quality);
			item.knockBack = 1;
			item.crit = 4;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 30;
			item.useAnimation = 30;

			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.shootSpeed = 7;
			item.useAmmo = AmmoID.Arrow;
		}
	}
}