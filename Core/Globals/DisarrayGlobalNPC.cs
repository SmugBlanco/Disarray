using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Disarray.Core.Data;
using System.Collections.ObjectModel;

namespace Disarray.Core.Globals
{
    public class DisarrayGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override bool CloneNewInstances => true;

        public IEnumerable<PropertiesBuffs> ActiveBuffs(NPC npc)
        {
            ICollection<PropertiesBuffs> activeBuffs = new Collection<PropertiesBuffs>();
            for (int indexer = 0; indexer < npc.buffType.Length; indexer++)
            {
                if (ModContent.GetModBuff(npc.buffType[indexer]) is DisarrayBuff buff)
                {
                    activeBuffs.Add(buff.Properties);
                }
            }
            return activeBuffs;
        }

        public override void AI(NPC npc)
        {
            foreach (PropertiesBuffs properties in ActiveBuffs(npc))
            {
                properties.Update(npc);
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            foreach (PropertiesBuffs properties in ActiveBuffs(npc))
            {
                properties.UpdateLifeRegen(npc, ref damage);
            }
        }

        public override void NPCLoot(NPC npc)
        {
            string internalName = string.Empty;
            if (NPCID.Search.TryGetName(npc.type, out string Name))
            {
                internalName = Name;
            }
            else
            {
                internalName = npc.modNPC?.Name;
            }

            foreach (NPCDropData dropData in NPCDropData.LoadedDropData)
            {
                dropData.NPCLoot(npc, internalName);
            }
        }
    }
}