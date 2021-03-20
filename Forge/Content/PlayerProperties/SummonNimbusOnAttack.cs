using Disarray.Core.Properties;
using Disarray.Forge.Content.Projectiles.Clouds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.PlayerProperties
{
	public class SummonNimbusOnAttack : PlayerProperty
    {
        public float InnateChance = 0.2f;

        public float AdditionalChance = 0;

        public float TotalChance => InnateChance + AdditionalChance;

        public override void Combine(PlayerProperty newProperty)
        {
            if (newProperty is SummonNimbusOnAttack property)
            {
                AdditionalChance += property.AdditionalChance;
            }
        }

        public override void OnHitNPC(Player player, Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat(1) < TotalChance && player.ownedProjectileCounts[ModContent.ProjectileType<StormNimbus>()] == 0)
            {
                Projectile.NewProjectile(target.Top + new Vector2(0, -25), Vector2.Zero, ModContent.ProjectileType<StormNimbus>(), damage / 2, knockback, player.whoAmI, 0, 0);
            }
        }

        public override void OnHitNPCWithProj(Player player, Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat(1) < TotalChance && player.ownedProjectileCounts[ModContent.ProjectileType<StormNimbus>()] == 0)
            {
                Projectile.NewProjectile(target.Top + new Vector2(0, -25), Vector2.Zero, ModContent.ProjectileType<StormNimbus>(), damage / 2, knockback, player.whoAmI, 0, 0);
            }
        }
    }
}