using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Disarray.Core.Data;

namespace Disarray.Core.Globals
{
    public class DisarrayGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override bool CloneNewInstances => true;

        public ICollection<PropertiesBuffs> ActiveBuffs(NPC npc)
        {
            IList<PropertiesBuffs> activeBuffs = new List<PropertiesBuffs>();
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
    }
}