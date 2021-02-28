using Disarray.Core.Properties;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.PlayerProperties
{
    public class DeadMansSpark : PlayerProperty
    {
        public int SparkCount;

        public override void Combine(PlayerProperty newProperty)
        {
            if (newProperty is DeadMansSpark property)
            {
                SparkCount += property.SparkCount;
            }
        }

        public override void OnHitNPC(Player player, Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (target.life - damage <= 0)
            {
                for (int Indexer = 0; Indexer < SparkCount; Indexer++)
                {
                    Vector2 SparkVelocity = new Vector2(0, -3).RotatedByRandom(MathHelper.ToRadians(45));
                    Projectile.NewProjectile(target.position, SparkVelocity, ModContent.ProjectileType<Projectiles.Graveyard.DeadMansSpark>(), damage, knockback, player.whoAmI);
                }
            }
        }

        public override void OnHitNPCWithProj(Player player, Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (target.life - damage <= 0)
            {
                for (int Indexer = 0; Indexer < SparkCount; Indexer++)
                {
                    Vector2 SparkVelocity = new Vector2(0, -3).RotatedByRandom(MathHelper.ToRadians(45));
                    Projectile.NewProjectile(target.position, SparkVelocity, ModContent.ProjectileType<Projectiles.Graveyard.DeadMansSpark>(), damage, knockback, player.whoAmI);
                }
            }
        }
    }
}