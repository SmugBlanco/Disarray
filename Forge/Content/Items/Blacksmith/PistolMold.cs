using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Blacksmith
{
	public class PistolMold : BlacksmithItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, null, null);

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults() => DisplayName.SetDefault("Pistol Mold");

		public override string ItemStatistics
		{
			get
			{
				string statistic = "12 base damage, 15 max damage"
				+ "\n4 base critical strike chance"
				+ "\n1 base knockback ( extremely weak )"
				+ "\n45 base use time and animation ( very slow )"
				+ "\n12 base shoot speed";
				return statistic + "\n" + StatTooltip;
			}
		}

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
			item.width = 36;
			item.height = 26;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item11;

			item.ranged = true;
			item.damage = 12 + (int)(3f * quality);
			item.crit = 4;
			item.knockBack = 1f;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 45;
			item.useAnimation = 45;

			item.shoot = ProjectileID.Bullet;
			item.shootSpeed = 12;
			item.useAmmo = AmmoID.Bullet;
		}
	}
}