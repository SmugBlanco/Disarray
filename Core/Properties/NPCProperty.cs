using Disarray.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Properties
{
    public abstract class NPCProperty
    {
        public static IList<NPCProperty> LoadedProperties;

        public static IDictionary<string, NPCProperty> PropertyByName;

        private static int InternalIDCount;

        public static void Load(Assembly assembly)
        {
            LoadedProperties = new List<NPCProperty>();

            PropertyByName = new Dictionary<string, NPCProperty>();

            InternalIDCount = -1;

            foreach (Type item in assembly.GetTypes())
            {
                if (!item.IsAbstract && item.IsSubclassOf(typeof(NPCProperty)) && item.GetConstructor(new Type[0]) != null)
                {
                    NPCProperty npcProperty = Activator.CreateInstance(item) as NPCProperty;
                    npcProperty.Type = ++InternalIDCount;
                    string name = item.Name;
                    npcProperty.Name = item.Name;
                    LoadedProperties.Add(npcProperty);
                    PropertyByName.Add(npcProperty.Name, npcProperty);
                    npcProperty.PostLoad(npcProperty);
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
            if (obj is NPCProperty property)
            {
                return GetHashCode().Equals(property.GetHashCode());
            }

            return false;
        }

        public override int GetHashCode() => Type;

        public static void ImplementProperty(NPC npc, NPCProperty newProperty, bool manualRemoval = true)
        {
            DisarrayGlobalNPC globalNPC = npc.GetGlobalNPC<DisarrayGlobalNPC>();
            NPCProperty property = globalNPC.ActiveProperties(npc).FirstOrDefault(prop => prop is NPCProperty);

            if (newProperty != null && property is NPCProperty oldProperty)
            {
                oldProperty.Combine(newProperty);
            }
            else
            {
                if (manualRemoval)
                {
                    globalNPC.ManuallyRemovedProperties.Add(Activator.CreateInstance(newProperty.GetType()) as NPCProperty);
                }
                else
                {
                    globalNPC.AutomaticallyRemovedProperties.Add(Activator.CreateInstance(newProperty.GetType()) as NPCProperty);
                }
            }
        }

        public virtual void Combine(NPCProperty newProperty) { }

        public static NPCProperty GetProperty(int ID)
        {
            if (ID < 0 || ID >= LoadedProperties.Count)
            {
                return null;
            }

            return LoadedProperties[ID];
        }

        public static NPCProperty GetProperty(string name)
        {
            if (PropertyByName.TryGetValue(name, out NPCProperty property))
            {
                return property;
            }

            return null;
        }

        public virtual void PostLoad(NPCProperty npcProperty) { }

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