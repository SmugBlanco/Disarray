using Disarray.Core.Autoload;
using Disarray.Core.Globals;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Properties
{
    [AutoloadedClass]
    public class ProjectileProperty : AutoloadedClass
    {
        public Mod Mod => Disarray.GetMod;

        public override bool Equals(object obj) => obj is ProjectileProperty property && GetHashCode().Equals(property.GetHashCode());

        public override int GetHashCode() => Type;

        public static void ImplementProperty(Projectile projectile, ProjectileProperty newProperty)
        {
            if (newProperty is null)
            {
                return;
            }

            DisarrayGlobalProjectile globalProjectile = projectile.GetGlobalProjectile<DisarrayGlobalProjectile>();
            ProjectileProperty oldProperty = globalProjectile.ActiveProperties.FirstOrDefault(prop => prop.Equals(newProperty));

            if (oldProperty != null)
            {
                oldProperty.Combine(newProperty);
            }
            else
            {
                globalProjectile.ManuallyRemovedProperties.Add(newProperty);
            }
        }

        public virtual void Combine(ProjectileProperty newProperty) { }

        public virtual void PostLoad(ProjectileProperty projectileProperty) { }

        public virtual void PostAI(Projectile projectile) { }

        public virtual void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit) { }

        public virtual void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit) { }

        public virtual void OnHitPvp(Projectile projectile, Player target, int damage, bool crit) { }

        public virtual void Kill(Projectile projectile, int timeLeft) { }
	}
}