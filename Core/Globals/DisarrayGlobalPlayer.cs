using Disarray.Core.Data;
using Disarray.Core.Properties;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Globals
{
    public class DisarrayGlobalPlayer : ModPlayer
    {
        public IEnumerable<PlayerProperty> ActiveProperties => ActiveBuffs.Concat(AutomaticallyRemovedProperties).Concat(ManuallyRemovedProperties).Concat(GlobalProperties).ToList();

        public IEnumerable<PlayerProperty> ActiveBuffs
        {
            get
            {
                ICollection<PlayerProperty> activeBuffs = new Collection<PlayerProperty>();
                for (int indexer = 0; indexer < player.buffType.Length; indexer++)
                {
                    if (ModContent.GetModBuff(player.buffType[indexer]) is DisarrayBuff buff && buff.PlayerProperties != null)
                    {
                        activeBuffs.Add(buff.PlayerProperties);
                    }
                }
                return activeBuffs;
            }
        }

        public ICollection<PlayerProperty> AutomaticallyRemovedProperties = new HashSet<PlayerProperty>();

        public ICollection<PlayerProperty> ManuallyRemovedProperties = new HashSet<PlayerProperty>();

        public static ICollection<PlayerProperty> GlobalProperties = new HashSet<PlayerProperty>();

        public static void Load()
        {
            GlobalProperties = new Collection<PlayerProperty>();
        }

        public static void Unload()
        {
            GlobalProperties?.Clear();
        }

        public override void ResetEffects() => AutomaticallyRemovedProperties.Clear();

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.ModifyHitNPC(player, item, target, ref damage, ref knockback, ref crit);
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.ModifyHitNPCWithProj(player, proj, target, ref damage, ref knockback, ref crit);
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.OnHitNPC(player, item, target, damage, knockback, crit);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.OnHitNPCWithProj(player, proj, target, damage, knockback, crit);
            }
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.ModifyHitByNPC(player, npc, ref damage, ref crit);
            }
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.ModifyHitByProjectile(player, proj, ref damage, ref crit);
            }
        }

        public override void PostUpdateMiscEffects()
        {
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.PostUpdateMiscEffects(player);
            }
        }

        public override void PostUpdateBuffs()
        {
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.Update(player);
            }
        }

        public override void UpdateBadLifeRegen()
        {
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.UpdateBadLifeRegen(player);
            }
        }
    }
}