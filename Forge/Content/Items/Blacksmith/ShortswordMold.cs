using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Blacksmith
{
	public class ShortswordMold : BlacksmithItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shortsword Mold");
			Tooltip.SetDefault("Serves as a basic template for creating a custom staff");
		}

		public override string ItemStatistics
		{
			get
			{
				string Damage = "Damage: " + item.damage;
				string CritChance = "Crit Chance: " + item.crit + "%";
				string Knockback = "Knockback: " + item.knockBack;
				string UseTime = "Use Time: " + item.useTime;
				string UseAnimation = "Use Animation: " + item.useAnimation;
				return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation;
			}
		}

		public override void NonProductDefaults()
        {
			item.width = 36;
			item.height = 36;
			item.maxStack = 999;

			item.useStyle = 0;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 50;
			item.height = 50;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;

			item.melee = true;
			item.damage = 8;
			item.knockBack = 2;
			item.crit = 4;

			item.useStyle = ItemUseStyleID.Stabbing;
			item.useTime = 15;
			item.useAnimation = 15;
		}
	}
}