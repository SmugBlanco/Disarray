using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Blacksmith
{
	public class SwordMold : BlacksmithItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, null, "Disarray/Forge/Content/Items/Blacksmith/SwordMold_Weapon");

		public override string ItemStatistics
		{
			get
			{
				return "Does shit innit";
			}
		}

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float>
		{
			{ "Metal", 1f }
		};

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sword Mold");
			Tooltip.SetDefault("Serves as a basic template for creating a custom sword." + "\nWorks well with metal based materials.");
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
			item.damage = 12 + (int)Math.Round(5f * quality);
			item.crit = 4;
			item.knockBack = 5;

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 28;
			item.useAnimation = 28;
		}
	}
}