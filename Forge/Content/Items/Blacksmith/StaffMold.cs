using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Blacksmith
{
	public class StaffMold : BlacksmithItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, null, null);

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Staff Mold");
			Item.staff[item.type] = true;
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "12 base damage, 15 max damage"
				+ "\n4 base critical strike chance"
				+ "\n3 base knockback ( very weak )"
				+ "\n32 base use time and animation ( slow )"
				+ "\n8 base shoot speed"
				+ "\n2 mana consumption on use";
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
			item.width = 40;
			item.height = 40;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item7;

			item.magic = true;
			item.damage = 12 + (int)(3f * quality);
			item.crit = 4;
			item.knockBack = 3;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 32;
			item.useAnimation = 32;

			item.shoot = ProjectileID.HallowStar;
			item.shootSpeed = 8;
			item.mana = 2;
		}
	}
}