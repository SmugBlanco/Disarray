using Disarray.Utility;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Rusty
{
	public class RustySword : RustyItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, null, null);

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rusty Sword");
			Tooltip.SetDefault(GeneralDescription);
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "18 base damage, 22 max damage"
				+ "\n6 base critical strike chance"
				+ "\n4 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(4f, true) + " )"
				+ "\n39 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(39, true) + " )";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults()
		{
			item.width = 42;
			item.height = 42;
			item.maxStack = 999;

			item.useStyle = 0;
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 42;
			item.height = 42;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item1;

			item.melee = true;
			item.damage = 18 + (int)(4f * quality);
			item.crit = 6;
			item.knockBack = 4;

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 39;
			item.useAnimation = 39;
		}
	}
}