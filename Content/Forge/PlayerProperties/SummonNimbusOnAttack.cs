using Disarray.Content.Forge.Projectiles.Cloud;
using Disarray.Core.Data;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.PlayerProperties
{
    public class SummonNimbusOnAttack : PropertiesPlayer
    {
        public float InnateChance = 0.2f;

        public float AdditionalChance = 0;

        public float TotalChance => InnateChance + AdditionalChance;

        public SummonNimbusOnAttack(float Chance)
        {
            AdditionalChance += Chance;
        }

        public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (Main.rand.NextFloat(1) < TotalChance && player.ownedProjectileCounts[ModContent.ProjectileType<StormNimbus>()] == 0)
            {
                Vector2 NPCTopMiddle = new Vector2(target.position.X, target.position.Y) + new Vector2(target.width / 2, 0);
                Projectile.NewProjectile(NPCTopMiddle + new Vector2(0, -25), Vector2.Zero, ModContent.ProjectileType<StormNimbus>(), damage / 2, knockback, player.whoAmI, 0, 0);
            }
        }

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (Main.rand.NextFloat(1) < TotalChance && player.ownedProjectileCounts[ModContent.ProjectileType<StormNimbus>()] == 0)
            {
                Vector2 NPCTopMiddle = new Vector2(target.position.X, target.position.Y) + new Vector2(target.width / 2, 0);
                Projectile.NewProjectile(NPCTopMiddle + new Vector2(0, -25), Vector2.Zero, ModContent.ProjectileType<StormNimbus>(), damage / 2, knockback, player.whoAmI, 0, 0);
            }
        }
    }
}