using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Disarray.Core.Properties;
using System.Linq;

namespace Disarray.Core.Globals
{
    public class DisarrayGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override bool CloneNewInstances => false;

        public IEnumerable<ProjectileProperty> ActiveProperties => ManuallyRemovedProperties.Concat(GlobalProperties).ToList();

        public ICollection<ProjectileProperty> ManuallyRemovedProperties = new HashSet<ProjectileProperty>();

        public static ICollection<ProjectileProperty> GlobalProperties = new HashSet<ProjectileProperty>();

        public static void Load()
        {
            GlobalProperties = new HashSet<ProjectileProperty>();
        }

        public static void Unload()
        {
            GlobalProperties.Clear();
        }

        public override void PostAI(Projectile projectile)
        {
            foreach (ProjectileProperty properties in ActiveProperties)
            {
                properties.PostAI(projectile);
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            foreach (ProjectileProperty properties in ActiveProperties)
            {
                properties.OnHitNPC(projectile, target, damage, knockback, crit);
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
        {
            foreach (ProjectileProperty properties in ActiveProperties)
            {
                properties.OnHitPlayer(projectile, target, damage, crit);
            }
        }

        public override void OnHitPvp(Projectile projectile, Player target, int damage, bool crit)
        {
            foreach (ProjectileProperty properties in ActiveProperties)
            {
                properties.OnHitPvp(projectile, target, damage, crit);
            }
        }

        public override void Kill(Projectile projectile, int timeLeft)
        {
            foreach (ProjectileProperty properties in ActiveProperties)
            {
                properties.Kill(projectile, timeLeft);
            }
        }
    }
}