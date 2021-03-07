using Disarray.Core.Globals;
using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Properties
{
    [AutoloadedClass]
    public class PlayerProperty : AutoloadedClass
    {
        public Mod Mod => Disarray.GetMod;

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