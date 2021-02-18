using Disarray.Content.Forge.Dusts.Misc;
using Disarray.Content.Forge.Projectiles.Properties;
using Disarray.Core.Forge.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Misc
{
	public class NapalmBarrel : Materials
	{
		public IEnumerable<ForgeBase> SameItems => (from bases in ImplementedItem?.AllBases where bases.item.type == item.type select bases);

		public float ChanceIncrement = 0.025f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Napalm Barrel");
			Tooltip.SetDefault("'Kill it with fire!'");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
			item.rare = ItemRarityID.LightRed;
			item.maxStack = 999;
		}

        public override void ModifyFiredProjectiles(Projectile projectile)
        {
			Napalmed.ImplementThis(projectile, ChanceIncrement);
        }

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
			int Chance = (hitbox.Width + hitbox.Height) / 3;
			if (Main.rand.Next(Chance) == 0 || Main.GameUpdateCount % 10 == 0)
			{
				Dust.NewDust(hitbox.Location.ToVector2(), hitbox.Width, hitbox.Height, ModContent.DustType<Napalm>(), player.velocity.X / 2, player.velocity.Y / 2);
			}
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
			if (Main.rand.NextFloat(1) < 0.2f + (ChanceIncrement * SameItems.Count()))
			{
				target.AddBuff(ModContent.BuffType<Buffs.Misc.Napalmed>(), 900);
			}
		}

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
			if (Main.rand.NextFloat(1) < 0.2f + (ChanceIncrement * SameItems.Count()))
			{
				target.AddBuff(ModContent.BuffType<Buffs.Misc.Napalmed>(), 900);
			}
		}

        public override string ItemDescription() => "A dangerous weapon that gain infamy for it's usage in the Vietnam War; perhaps you can utilised in 'The Forge'";

		public override string ItemStatistics()
		{
			string AttackInfo = "Attacks are set ablazed with napalm, having a 20% to carry over to enemies when struck ( 15 seconds ).";
			string BoostedEnemies = "Gelatinous enemies incur a 200% damage and speed boost to the damage over time." + "\nMost arachnids incur only a 100% damage boost.";
			string SpreadEffect = "Enemies covered in blazing napalm have a 2.5% chance to spread it to other nearby ( within their hitbox ) enemies every tick ( 1/60 of a second ).";
			string Stackability = "Subsequent materials increases initial spread chance by 2.5%";
			return AttackInfo + "\n" + BoostedEnemies + "\n" + SpreadEffect + "\n" + SpreadEffect + "\n" + Stackability;
		}

		public override string ObtainingDetails() => "Created at a mythril anvil by combinding bars of iron or lead with gel and living fire.";

		public override string MiscDetails() => "'Napalm' is a portmanteau of it's two original gelling ingredients: Naphthenic acid and Palmitic acid.";

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 15);
			recipe.AddIngredient(ItemID.Gel, 333);
			recipe.AddIngredient(ItemID.LivingFireBlock, 25);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}