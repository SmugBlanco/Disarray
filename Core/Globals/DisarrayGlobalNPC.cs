using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Disarray.Core.Data;
using System.Collections.ObjectModel;
using Disarray.Core.Properties;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Disarray.Core.Utilities;

namespace Disarray.Core.Globals
{
    public class DisarrayGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override bool CloneNewInstances => true;

        public IEnumerable<NPCProperty> ActiveProperties(NPC npc) => ActiveBuffs(npc).Concat(AutomaticallyRemovedProperties).Concat(ManuallyRemovedProperties).Concat(GlobalProperties).ToList();

        public IEnumerable<NPCProperty> ActiveBuffs(NPC npc)
        {
            ICollection<NPCProperty> activeBuffs = new Collection<NPCProperty>();
            for (int indexer = 0; indexer < npc.buffType.Length; indexer++)
            {
                if (ModContent.GetModBuff(npc.buffType[indexer]) is DisarrayBuff buff && buff.NPCProperties != null)
                {
                    activeBuffs.Add(buff.NPCProperties);
                }
            }
            return activeBuffs;
        }

        public ICollection<NPCProperty> AutomaticallyRemovedProperties = new HashSet<NPCProperty>();

        public ICollection<NPCProperty> ManuallyRemovedProperties = new HashSet<NPCProperty>();

        public static ICollection<NPCProperty> GlobalProperties = new HashSet<NPCProperty>();

        public static void Load()
		{
            GlobalProperties = new Collection<NPCProperty>();
        }

        public static void Unload()
		{
            GlobalProperties?.Clear();
        }

        public override void SetDefaults(NPC npc)
        {
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.SetDefaults(npc);
            }
        }

        public override void ScaleExpertStats(NPC npc, int numPlayers, float bossLifeScale)
        {
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.ScaleExpertStats(npc, numPlayers, bossLifeScale);
            }
        }

        public override void ResetEffects(NPC npc)
        {
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.ResetEffects(npc);
            }

            AutomaticallyRemovedProperties.Clear();
        }

        public override bool PreAI(NPC npc)
		{
            bool continueAI = base.PreAI(npc);
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                if (!properties.PreAI(npc))
				{
                    continueAI = false;
                }
            }
            return continueAI;
        }

        public override void AI(NPC npc)
		{
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.AI(npc);
            }
        }

        public override void HitEffect(NPC npc, int hitDirection, double damage)
		{
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.HitEffect(npc, hitDirection, damage);
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.UpdateLifeRegen(npc, ref damage);
            }
        }

        public override bool PreNPCLoot(NPC npc)
		{
            bool continueLoot = base.PreNPCLoot(npc);
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                if (!properties.PreNPCLoot(npc))
                {
                    continueLoot = false;
                }
            }
            return continueLoot;
        }

        public override void NPCLoot(NPC npc)
		{
            string internalName = NPCUtilities.GetInternalName(npc.type).ToLower();

            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.NPCLoot(npc, internalName);
            }
        }

        public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
		{
            bool continueHitPlayer = base.CanHitPlayer(npc, target, ref cooldownSlot);
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                if (!properties.CanHitPlayer(npc, target, ref cooldownSlot))
                {
                    continueHitPlayer = false;
                }
            }
            return continueHitPlayer;
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
		{
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.ModifyHitPlayer(npc, target, ref damage, ref crit);
            }
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
		{
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.OnHitPlayer(npc, target, damage, crit);
            }
        }

        public override bool? CanHitNPC(NPC npc, NPC target)
		{
            bool? continueHitNPC = base.CanHitNPC(npc, target);
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                bool? canBeHit = properties.CanHitNPC(npc, target);

                if (canBeHit.HasValue)
                {
                    if (!continueHitNPC.HasValue)
                    {
                        continueHitNPC = canBeHit.Value;
                    }

                    if (!canBeHit.Value)
                    {
                        continueHitNPC = false;
                    }
                }
            }
            return continueHitNPC;
        }

        public override void ModifyHitNPC(NPC npc, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.ModifyHitNPC(npc, target, ref damage, ref knockback, ref crit);
            }
        }

        public override void OnHitNPC(NPC npc, NPC target, int damage, float knockback, bool crit)
		{
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.OnHitNPC(npc, target, damage, knockback, crit);
            }
        }

        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
		{
            bool? continueHitNPC = base.CanBeHitByItem(npc, player, item);
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                bool? canBeHit = properties.CanBeHitByItem(npc, player, item);

                if (canBeHit.HasValue)
                {
                    if (!continueHitNPC.HasValue)
                    {
                        continueHitNPC = canBeHit.Value;
                    }

                    if (!canBeHit.Value)
                    {
                        continueHitNPC = false;
                    }
                }
            }
            return continueHitNPC;
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
		{
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.ModifyHitByItem(npc, player, item, ref damage, ref knockback, ref crit);
            }
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
		{
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.OnHitByItem(npc, player, item, damage, knockback, crit);
            }
        }

        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
		{
            bool? continueHitNPC = base.CanBeHitByProjectile(npc, projectile);
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                bool? canBeHit = properties.CanBeHitByProjectile(npc, projectile);
                if (canBeHit.HasValue)
                {
                    if (!continueHitNPC.HasValue)
                    {
                        continueHitNPC = canBeHit.Value;
                    }

                    if (!canBeHit.Value)
                    {
                        continueHitNPC = false;
                    }
                }
            }
            return continueHitNPC;
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.ModifyHitByProjectile(npc, projectile, ref damage, ref knockback, ref crit, ref hitDirection);
            }
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
		{
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.OnHitByProjectile(npc, projectile, damage, knockback, crit);
            }
        }

        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
            bool continueStrike = base.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                if (!properties.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit))
                {
                    continueStrike = false;
                }
            }
            return continueStrike;
        }

        public override Color? GetAlpha(NPC npc, Color drawColor)
		{
            Color? getAlpha = base.GetAlpha(npc, drawColor);
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                Color? getThisAlpha = properties.GetAlpha(npc, drawColor);

                if (getThisAlpha.HasValue)
                {
                    getAlpha = getThisAlpha.Value;
                }
            }
            return getAlpha;
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
		{
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                properties.DrawEffects(npc, ref drawColor);
            }
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
		{
            bool continueDraw = base.PreDraw(npc, spriteBatch, drawColor);
            foreach (NPCProperty properties in ActiveProperties(npc))
            {
                if (!properties.PreDraw(npc, spriteBatch, drawColor))
                {
                    continueDraw = false;
                }
            }
            return continueDraw;
        }
    }
}