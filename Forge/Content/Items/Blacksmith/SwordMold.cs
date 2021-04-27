using Disarray.Utility;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Blacksmith
{
	public class SwordMold : BlacksmithItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, null, null);

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults() => DisplayName.SetDefault("Sword Mold");

		public override string ItemStatistics
		{
			get
			{
				string statistic = "17 base damage, 21 max damage"
				+ "\n5 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(5f, true) + " )"
				+ "\n28 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(28, true) + " )";
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
			item.width = 50;
			item.height = 50;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;

			item.melee = true;
			item.damage = 17 + (int)(4f * quality);
			item.knockBack = 5;

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 28;
			item.useAnimation = 28;
		}
	}
}