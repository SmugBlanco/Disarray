using Disarray.Core.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Properties
{
    public abstract class ProjectileProperty
    {
        internal static bool Loading;

        public static bool IsLoading => Loading;

        public static IList<ProjectileProperty> LoadedProperties;

        public static IDictionary<string, ProjectileProperty> PropertyByName;

        private static int InternalIDCount;

        public ProjectileProperty()
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
        }

        public static void Load(Assembly assembly)
        {
            Loading = true;

            LoadedProperties = new List<ProjectileProperty>();

            PropertyByName = new Dictionary<string, ProjectileProperty>();

            InternalIDCount = -1;

            foreach (Type item in assembly.GetTypes())
            {
                if (!item.IsAbstract && item.IsSubclassOf(typeof(ProjectileProperty)) && item.GetConstructor(new Type[0]) != null)
                {
                    ProjectileProperty projectileProperty = Activator.CreateInstance(item) as ProjectileProperty;
                    projectileProperty.Type = ++InternalIDCount;
                    projectileProperty.Name = item.Name;
                    LoadedProperties.Add(projectileProperty);
                    PropertyByName.Add(projectileProperty.Name, projectileProperty);
                    projectileProperty.PostLoad(projectileProperty);
                }
            }

            Loading = false;
        }

        public static void Unload()
        {
            LoadedProperties?.Clear();

            PropertyByName?.Clear();

            InternalIDCount = 0;
        }

        public Mod Mod => Disarray.GetMod;

        public int Type { get; internal set; }

        public string Name { get; internal set; }

        public override bool Equals(object obj)
        {
            if (obj is ProjectileProperty property)
            {
                return GetHashCode().Equals(property.GetHashCode());
            }

            return false;
        }

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

        public static ProjectileProperty GetProperty(int ID)
        {
            if (ID < 0 || ID >= LoadedProperties.Count)
            {
                return null;
            }

            return LoadedProperties[ID];
        }

        public static ProjectileProperty GetProperty(string name)
        {
            if (PropertyByName.TryGetValue(name, out ProjectileProperty property))
            {
                return property;
            }

            return null;
        }

        public virtual void PostLoad(ProjectileProperty projectileProperty) { }

        public virtual void PostAI(Projectile projectile) { }

        public virtual void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit) { }

        public virtual void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit) { }

        public virtual void OnHitPvp(Projectile projectile, Player target, int damage, bool crit) { }

        public virtual void Kill(Projectile projectile, int timeLeft) { }
	}
}