using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Forest
{
	public class WoodlandStaff : WoodlandItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Woodland Staff");
			Item.staff[item.type] = true;
		}

		public override string ItemStatistics()
		{
			string Damage = "Damage: " + (item.damage + DamageFlat);
			string CritChance = "Crit Chance: " + item.crit + "%";
			string Knockback = "Knockback: " + item.knockBack;
			string UseTime = "Use Time: " + item.useTime;
			string UseAnimation = "Use Animation: " + item.useAnimation;
			string ReuseDelay = "Reuse Delay: " + item.reuseDelay;
			string ShootSpeed = "Shoot Speed: " + item.shootSpeed;
			string Mana = "Uses " + item.mana + " mana per cast";
			string Conjures = "Rapidly conjures seeds to pellet enemies.";
			string Effect = "Increases life regeneration by 1 when held.";
			return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation + "\n" + ReuseDelay + "\n" + ShootSpeed + "\n" + Mana + "\n" + Conjures + "\n" + Effect;
		}

		public override void NonProductDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.maxStack = 999;

			item.useStyle = 0;

			item.shoot = ProjectileID.None;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 50;
			item.height = 50;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item7;

			item.magic = true;
			item.damage = 4;
			item.crit = 4;
			item.knockBack = 1;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 4;
			item.useAnimation = 12;
			item.reuseDelay = 20;

			item.shoot = ProjectileID.Seed;
			item.shootSpeed = 12;
			item.mana = 5;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 12);
			recipe.AddIngredient(ItemID.Mushroom, 4);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}