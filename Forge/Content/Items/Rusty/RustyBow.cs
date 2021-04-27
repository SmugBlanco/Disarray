using Disarray.Utility;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Rusty
{
	public class RustyBow : RustyItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, null, null);

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rusty Bow");
			Tooltip.SetDefault(GeneralDescription);
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "8 base damage, 10 max damage"
				+ "\n6 base critical strike chance"
				+ "\n1 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(1f, true) + " )"
				+ "\n30 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(30, true) + " )"
				+ "\n6.5 base shoot speed";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults()
		{
			item.width = 20;
			item.height = 40;
			item.maxStack = 999;

			item.useStyle = 0;

			item.shoot = ProjectileID.None;
			item.useAmmo = AmmoID.None;
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 22;
			item.height = 40;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item5;

			item.ranged = true;
			item.damage = 8 + (int)(2f * quality);
			item.knockBack = 1f;
			item.crit = 6;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 30;
			item.useAnimation = 30;

			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.shootSpeed = 6.5f;
			item.useAmmo = AmmoID.Arrow;
		}
	}
}