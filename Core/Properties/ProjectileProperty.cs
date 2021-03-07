using Disarray.Core.Globals;
using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Properties
{
    [AutoloadedClass]
    public class ProjectileProperty : AutoloadedClass
    {
        /*public ProjectileProperty()
        {
            if (IsLoading)
            {
                return;
            }

            if (PropertyByName.ContainsKey(GetType().Name))
            {
                ProjectileProperty propertyToMimic = PropertyByName[GetType().Name];
                Type = propertyToMimic.Type;
                Name = propertyToMimic.Name;
            }
        }*/ // A nice alternative to CreateNewInstance

        public static ProjectileProperty CreateNewInstance(ProjectileProperty sourceProperty)
		{
            ProjectileProperty newProperty = Activator.CreateInstance(sourceProperty.GetType()) as ProjectileProperty;
            newProperty.Type = sourceProperty.Type;
            newProperty.Name = sourceProperty.Name;
            return newProperty;
        }

        public Mod Mod => Disarray.GetMod;

        public override bool Equals(object obj) => obj is ProjectileProperty property && GetHashCode().Equals(property.GetHashCode());

        public override int GetHashCode() => Type;

        public static void ImplementProperty(Projectile projectile, ProjectileProperty newProperty)
        {
            DisarrayGlobalProjectile GlobalProjectile = projectile.GetGlobalProjectile<DisarrayGlobalProjectile>();
            ProjectileProperty property = GlobalProjectile.ActiveProperties.FirstOrDefault(prop => prop is ProjectileProperty);

            if (newProperty != null && property is ProjectileProperty oldProperty)
            {
                oldProperty.Combine(newProperty);
            }
            else
            {
                GlobalProjectile.ManuallyRemovedProperties.Add(Activator.CreateInstance(newProperty.GetType()) as ProjectileProperty);
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