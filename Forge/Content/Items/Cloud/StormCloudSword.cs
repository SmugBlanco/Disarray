using Disarray.Content.Buffs;
using Disarray.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Cloud
{
	public class StormCloudSword : StormCloudItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override string ItemStatistics
		{
			get
			{
				string Damage = "Damage: " + item.damage;
				string CritChance = "Crit Chance: " + item.crit + "%";
				string Knockback = "Knockback: " + item.knockBack;
				string UseTime = "Use Time: " + item.useTime;
				string UseAnimation = "Use Animation: " + item.useAnimation;
				string Effect = "Strikes are electrified and has a 25% of inflicting that onto enemies for 5 seconds." + "\nThis effect is guaranteed if the weapon or enemy is at least partially submerged in water.";
				return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation + "\n" + Effect;
			}
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Storm Cloud Sword");
		}

		public override void NonProductDefaults()
		{
			item.width = 50;
			item.height = 44;
			item.maxStack = 999;

			item.useStyle = 0;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 44;
			item.height = 44;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;

			item.melee = true;
			item.damage = 13;
			item.crit = 4;
			item.knockBack = 2.5f;

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 26;
			item.useAnimation = 26;
		}

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
			int Chance = (hitbox.Width + hitbox.Height) / 2;
            if (Main.rand.Next(Chance) == 0 || Main.GameUpdateCount % 15 == 0)
            {
				Dust.NewDust(hitbox.Location.ToVector2(), hitbox.Width, hitbox.Height, ModContent.DustType<Electricity>(), player.velocity.X / 2, player.velocity.Y / 2);
			}
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
			float ChanceToInflict = 0.25f;
			if (target.wet || player.wet)
			{
				ChanceToInflict = 1f;
			}

			if (Main.rand.NextFloat(1) < ChanceToInflict)
			{
				target.AddBuff(ModContent.BuffType<Electrified>(), 300);
			}
		}

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
			float ChanceToInflict = 0.25f;
			if (target.wet || player.wet)
			{
				ChanceToInflict = 1f;
			}

			if (Main.rand.NextFloat(1) < ChanceToInflict)
			{
				target.AddBuff(ModContent.BuffType<Electrified>(), 300);
			}
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cloud, 15);
			recipe.AddIngredient(ItemID.RainCloud, 3);
			recipe.AddIngredient(ItemID.FallenStar, 5);
			recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CloudSword>());
			recipe.AddIngredient(ItemID.RainCloud, 3);
			recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}