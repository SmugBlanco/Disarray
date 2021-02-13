using Disarray.Core.Data;
using System.Collections.Generic;
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