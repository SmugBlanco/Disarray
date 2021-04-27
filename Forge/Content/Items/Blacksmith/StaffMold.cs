using Disarray.Utility;
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
				string statistic = "8 base damage, 10 max damage"
				+ "\n3 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(3f, true) + " )"
				+ "\n14 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(14, true) + " )"
				+ "\n8 base shoot speed"
				+ "\n3 mana consumption on use"
				+ "\nConjures piercing stars";
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
			item.damage = 8 + (int)(2f * quality);
			item.knockBack = 3;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 14;
			item.useAnimation = 14;

			item.shoot = ProjectileID.HallowStar;
			item.shootSpeed = 8;
			item.mana = 3;
		}
	}
}