using Disarray.Core.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Globals
{
    public class DisarrayGlobalPlayer : ModPlayer
    {
        public ICollection<PropertiesBuffs> ActiveBuffs
        {
            get
            {
                IList<PropertiesBuffs> activeBuffs = new List<PropertiesBuffs>();
                for (int indexer = 0; indexer < player.buffType.Length; indexer++)
                {
                    if (ModContent.GetModBuff(player.buffType[indexer]) is DisarrayBuff buff)
                    {
                        activeBuffs.Add(buff.Properties);
                    }
                }
                return activeBuffs;
            }
        }

        public ICollection<PropertiesPlayer> ActiveProperties = new Collection<PropertiesPlayer>();

        public override void ResetEffects()
        {
            ActiveProperties.Clear();
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            foreach (PropertiesPlayer properties in ActiveProperties)
            {
                properties.ModifyHitNPC(player, item, target, ref damage, ref knockback, ref crit);
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            foreach (PropertiesPlayer properties in ActiveProperties)
            {
                properties.ModifyHitNPCWithProj(player, proj, target, ref damage, ref knockback, ref crit);
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            foreach (PropertiesPlayer properties in ActiveProperties)
            {
                properties.OnHitNPC(player, item, target, damage, knockback, crit);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            foreach (PropertiesPlayer properties in ActiveProperties)
            {
                properties.OnHitNPCWithProj(player, proj, target, damage, knockback, crit);
            }
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            foreach (PropertiesPlayer properties in ActiveProperties)
            {
                properties.ModifyHitByNPC(player, npc, ref damage, ref crit);
            }
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            foreach (PropertiesPlayer properties in ActiveProperties)
            {
                properties.ModifyHitByProjectile(player, proj, ref damage, ref crit);
            }
        }

        public override void PostUpdateMiscEffects()
        {
            foreach (PropertiesPlayer properties in ActiveProperties)
            {
                properties.PostUpdateMiscEffects(player);
            }
        }

        public override void PostUpdateBuffs()
        {
            foreach (PropertiesBuffs properties in ActiveBuffs)
            {
                properties.Update(player);
            }
        }

        public override void UpdateBadLifeRegen()
        {
            foreach (PropertiesBuffs properties in ActiveBuffs)
            {
                properties.UpdateBadLifeRegen(player);
            }
        }
    }
}