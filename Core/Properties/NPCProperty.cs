using Disarray.Core.Autoload;
using Disarray.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Properties
{
    [AutoloadedClass]
    public class NPCProperty : AutoloadedClass
    {
        public Mod Mod => Disarray.GetMod;

        public override bool Equals(object obj)
        {
            if (obj is NPCProperty property)
            {
                return GetHashCode().Equals(property.GetHashCode());
            }

            return false;
        }

		public override int GetHashCode() => Type;

        public static void ImplementProperty(NPC npc, NPCProperty newProperty, bool manualRemoval = true)
        {
            if (newProperty is null)
            {
                return;
            }

            DisarrayGlobalNPC globalNPC = npc.GetGlobalNPC<DisarrayGlobalNPC>();
            NPCProperty oldProperty = globalNPC.ActiveProperties(npc).FirstOrDefault(prop => prop.Equals(newProperty));

            if (oldProperty != null)
            {
                oldProperty.Combine(newProperty);
            }
            else
            {
                if (manualRemoval)
                {
                    globalNPC.ManuallyRemovedProperties.Add(newProperty);
                }
                else
                {
                    globalNPC.AutomaticallyRemovedProperties.Add(newProperty);
                }
            }
        }

        public virtual void Combine(NPCProperty newProperty) { }

        public virtual void SetDefaults(NPC npc) { }

        public virtual void ScaleExpertStats(NPC npc, int numPlayers, float bossLifeScale) { }

		public virtual void ResetEffects(NPC npc) { }

		public virtual bool PreAI(NPC npc) => true;

		public virtual void AI(NPC npc) { }

		public virtual void HitEffect(NPC npc, int hitDirection, double damage) { }

		public virtual void UpdateLifeRegen(NPC npc, ref int damage) { }

		public virtual bool PreNPCLoot(NPC npc) => true;

		public virtual void NPCLoot(NPC npc, string internalName) { }

		public virtual bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot) => true;

		public virtual void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit) { }

		public virtual void OnHitPlayer(NPC npc, Player target, int damage, bool crit) { }

		public virtual bool? CanHitNPC(NPC npc, NPC target) => null;

		public virtual void ModifyHitNPC(NPC npc, NPC target, ref int damage, ref float knockback, ref bool crit) { }

		public virtual void OnHitNPC(NPC npc, NPC target, int damage, float knockback, bool crit) { }

		public virtual bool? CanBeHitByItem(NPC npc, Player player, Item item) => null;

		public virtual void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit) { }

		public virtual void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit) { }

		public virtual bool? CanBeHitByProjectile(NPC npc, Projectile projectile) => null;

		public virtual void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) { }

		public virtual void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit) { }

		public virtual bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit) => true;

		public virtual Color? GetAlpha(NPC npc, Color drawColor) => null;

		public virtual void DrawEffects(NPC npc, ref Color drawColor) { }

		public virtual bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor) => true;
    }
}