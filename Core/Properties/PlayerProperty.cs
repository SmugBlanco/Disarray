using Disarray.Core.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Properties
{
    public abstract class PlayerProperty
    {
        public static IList<PlayerProperty> LoadedProperties;

        public static IDictionary<string, PlayerProperty> PropertyByName;

        private static int InternalIDCount;

        public static void Load(Assembly assembly)
		{
            LoadedProperties = new List<PlayerProperty>();

            PropertyByName = new Dictionary<string, PlayerProperty>();

            InternalIDCount = -1;

            foreach (Type item in assembly.GetTypes())
			{
                if (!item.IsAbstract && item.IsSubclassOf(typeof(PlayerProperty)) && item.GetConstructor(new Type[0]) != null)
				{
                    PlayerProperty propertyPlayer = Activator.CreateInstance(item) as PlayerProperty;
                    propertyPlayer.Type = ++InternalIDCount;
                    string name = item.Name;
                    propertyPlayer.Name = item.Name;
                    LoadedProperties.Add(propertyPlayer);
                    PropertyByName.Add(propertyPlayer.Name, propertyPlayer);
                    propertyPlayer.PostLoad(propertyPlayer);
                }
			}
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
            if (obj is PlayerProperty property)
            {
                return GetHashCode().Equals(property.GetHashCode());
            }

            return false;
        }

        public override int GetHashCode() => Type;

        public static void ImplementProperty(Player player, PlayerProperty newProperty, bool manualRemoval = true)
        {
            DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
            PlayerProperty property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is PlayerProperty);

            if (newProperty != null && property is PlayerProperty oldProperty)
            {
                oldProperty.Combine(newProperty);
            }
            else
            {
                PlayerProperty addedProperty = newProperty is null ? Activator.CreateInstance(newProperty.GetType()) as PlayerProperty : newProperty;
                if (manualRemoval)
                {
                    GlobalPlayer.ManuallyRemovedProperties.Add(addedProperty);
                }
                else
                {
                    GlobalPlayer.AutomaticallyRemovedProperties.Add(addedProperty);
                }
            }
        }

        public virtual void Combine(PlayerProperty newProperty) { }

        public static PlayerProperty GetProperty(int ID)
		{
            if (ID < 0 || ID >= LoadedProperties.Count)
			{
                return null;
			}

            return LoadedProperties[ID];
		}

        public static PlayerProperty GetProperty(string name)
        {
            if (PropertyByName.TryGetValue(name, out PlayerProperty property))
			{
                return property;
            }

            return null;
        }

        public virtual void PostLoad(PlayerProperty playerProperty) { }

        public virtual void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) { }

        public virtual void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit) { }

        public virtual void OnHitNPC(Player player, Item item, NPC target, int damage, float knockback, bool crit) { }

        public virtual void OnHitNPCWithProj(Player player, Projectile proj, NPC target, int damage, float knockback, bool crit) { }

        public virtual void ModifyHitByNPC(Player player, NPC npc, ref int damage, ref bool crit) { }

        public virtual void ModifyHitByProjectile(Player player, Projectile proj, ref int damage, ref bool crit) { }

        public virtual void OnHitByNPC(Player player, NPC npc, int damage, bool crit) { }

        public virtual void OnHitByProjectile(Player player, Projectile proj, int damage, bool crit) { }

        public virtual void PostUpdateMiscEffects(Player player) { }

        public virtual void PostUpdateRunSpeeds(Player player) { }

        public virtual void Update(Player player) { }

        public virtual void UpdateBadLifeRegen(Player player) { }
	}
}