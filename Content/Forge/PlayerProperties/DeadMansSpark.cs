using Disarray.Core.Data;
using Disarray.Core.Globals;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.PlayerProperties
{
    public class DeadMansSpark : PropertiesPlayer
    {
        public static void ImplementThis(Player player, int Count)
        {
            DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
            PropertiesPlayer property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is DeadMansSpark);
            if (property is DeadMansSpark sparkProperty)
            {
                sparkProperty.SparkCount += Count;
            }
            else
            {
                player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties.Add(new DeadMansSpark(Count));
            }
        }

        public int SparkCount;

        public DeadMansSpark(int Count)
        {
            SparkCount += Count;
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