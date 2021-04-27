using Disarray.Utility;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Rusty
{
	public class RustyPistol : RustyItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, null, null);

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rusty Pistol");
			Tooltip.SetDefault(GeneralDescription);
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "14 base damage, 17 max damage"
				+ "\n6 base critical strike chance"
				+ "\n1 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(1f, true) + " )"
				+ "\n50 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(50, true) + " )"
				+ "\n12 base shoot speed";
				return statistic + "\n" + StatTooltip;
			}
		}


		public override void NonProductDefaults()
		{
			item.width = 30;
			item.height = 22;
			item.maxStack = 999;

			item.useStyle = 0;

			item.shoot = ProjectileID.None;
			item.useAmmo = AmmoID.None;
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 36;
			item.height = 22;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item11;

			item.ranged = true;
			item.damage = 14 + (int)(3f * quality);
			item.crit = 6;
			item.knockBack = 1f;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 50;
			item.useAnimation = 50;

			item.shoot = ProjectileID.Bullet;
			item.shootSpeed = 12;
			item.useAmmo = AmmoID.Bullet;
		}
	}
}