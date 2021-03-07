using Disarray.Content.Forge.Buffs.Misc;
using Disarray.Content.Forge.Dusts.Misc;
using Disarray.Core.Data;
using Disarray.Core.Properties;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Buffs.Properties.Misc
{
    public class NapalmedNPC : NPCProperty
    {
        public override void PostLoadType()
        {
            if (BuffLoader.GetBuff(ModContent.BuffType<Napalmed>()) is DisarrayBuff buff)
            {
                buff.NPCProperties = this;
            }
        }

        public override void AI(NPC npc)
        {
            int Chance = (npc.height + npc.width) / 3;
            if (Main.rand.Next(Chance) == 0 || Main.GameUpdateCount % 10 == 0)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Napalm>(), npc.velocity.X / 2, npc.velocity.Y / 2);
            }

            for (int Indexer = 0; Indexer < Main.npc.Length - 1; Indexer++)
            {
                NPC indexedNPC = Main.npc[Indexer];
                if (npc.whoAmI != indexedNPC.whoAmI && npc.Distance(indexedNPC.Center) < (npc.width + indexedNPC.width) / 2 && Main.rand.NextFloat(1) < 0.025f)
                {
                    indexedNPC.AddBuff(ModContent.BuffType<Napalmed>(), npc.buffTime[npc.FindBuffIndex(ModContent.BuffType<Napalmed>())]);
                }
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            int DoTAdjusted = 40;
            int damageAdjusted = 3;
            if (npc.FullName.Contains("Slime") || npc.type == NPCID.Gastropod)
            {
                DoTAdjusted = 120;
                damageAdjusted = 9;
            }

            if (npc.aiStyle == 40 || npc.FullName.Contains("Spider"))
            {
                damageAdjusted = 6;
            }

            npc.lifeRegen -= DoTAdjusted;

            if (damage < damageAdjusted)
            {
                damage = damageAdjusted;
            }
        }
    }
}