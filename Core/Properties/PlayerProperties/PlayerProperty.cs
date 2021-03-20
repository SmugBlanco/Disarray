using Disarray.Core.Autoload;
using Disarray.Core.Globals;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Properties
{
    [AutoloadedClass]
    public partial class PlayerProperty : AutoloadedClass
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
            if (newProperty is null)
			{
                return;
			}
            
            DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
            PlayerProperty oldProperty = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop.Equals(newProperty));

            if (oldProperty != null)
            {
                oldProperty.Combine(newProperty);
            }
            else
            {
                if (manualRemoval)
                {
                    GlobalPlayer.ManuallyRemovedProperties.Add(newProperty);
                }
                else
                {
                    GlobalPlayer.AutomaticallyRemovedProperties.Add(newProperty);
                }
            }
        }

        public virtual void Combine(PlayerProperty newProperty) { }

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