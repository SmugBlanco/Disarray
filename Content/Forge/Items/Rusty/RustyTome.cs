using Disarray.Content.Forge.Projectiles.Rusty;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Rusty
{
	public class RustyTome : RustyItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rusty Tome");
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
			string Conjure = "Conjures a glob of mud on usage";
			return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation + "\n" + ShootSpeed + "\n" + Mana + "\n" + Conjure;
		}

		public override void NonProductDefaults()
		{
			item.maxStack = 999;

			item.useStyle = 0;

			item.shoot = ProjectileID.None;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 28;
			item.height = 30;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item69;

			item.magic = true;
			item.damage = 8;
			item.crit = 6;
			item.knockBack = 2;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 24;
			item.useAnimation = 24;

			item.shoot = ModContent.ProjectileType<MudGlob>();
			item.shootSpeed = 10;
			item.mana = 5;
		}
	}
}