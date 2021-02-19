using Disarray.Content.Forge.Projectiles.Cloud;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Cloud
{
	public class CloudSatchel : CloudItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cloud Satchel");
		}

		public override string ItemStatistics()
		{
			string Damage = "Damage: " + (item.damage + DamageFlat);
			string CritChance = "Crit Chance: " + item.crit + "%";
			string Knockback = "Knockback: " + item.knockBack;
			string UseTime = "Use Time: " + item.useTime;
			string UseAnimation = "Use Animation: " + item.useAnimation;
			string ShootSpeed = "Shoot Speed: " + item.shootSpeed;
			string Mana = "Uses " + item.mana + " mana per cast";
			string Conjure = "Expells a piece of the pocketed cloud";
			return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation + "\n" + ShootSpeed + "\n" + Mana + "\n" + Conjure;
		}

		public override string ObtainingDetails() => "Crafted at a pool of water from solidified clouds, bound together by a few pieces of leather.";

		public override void NonProductDefaults()
		{
			item.width = 28;
			item.height = 38;
			item.maxStack = 999;

			item.useStyle = 0;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 30;
			item.height = 38;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item66;

			item.magic = true;
			item.damage = 14;
			item.crit = 4;
			item.knockBack = 1;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 32;
			item.useAnimation = 32;
			item.noUseGraphic = true;

			item.shoot = ModContent.ProjectileType<ExpelledCloudPiece>();
			item.shootSpeed = 8;
			item.mana = 8;
		}

        public override Projectile ShootButBetter(Player player, Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 0, -1);
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cloud, 12);
			recipe.AddIngredient(ItemID.Leather, 3);
			recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}