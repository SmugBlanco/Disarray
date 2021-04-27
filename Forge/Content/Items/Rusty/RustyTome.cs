using Disarray.Utility;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Rusty
{
	public class RustyTome : RustyItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, null, null);

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rusty Tome");
			Tooltip.SetDefault(GeneralDescription);
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "14 base damage, 17 max damage"
				+ "\n6 base critical strike chance"
				+ "\n2 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(2f, true) + " )"
				+ "\n32 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(32, true) + " )"
				+ "\n10 base shoot speed"
				+ "\n5 mana consumption on use";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults()
		{
			item.maxStack = 999;

			item.useStyle = 0;

			item.shoot = ProjectileID.None;
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 28;
			item.height = 30;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item69;

			item.magic = true;
			item.damage = 14 + (int)(3f * quality);
			item.crit = 6;
			item.knockBack = 2;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 32;
			item.useAnimation = 32;

			item.shoot = ModContent.ProjectileType<RustyTomeProjectile>();
			item.shootSpeed = 10;
			item.mana = 5;
		}
	}
}