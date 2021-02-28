using Disarray.Content.Forge.Dusts.Cloud;
using Disarray.Core.Data;
using Disarray.Core.Properties;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Buffs.Properties.Cloud
{
    public class ElectrifiedNPC : NPCProperty
    {
        public override void PostLoad(NPCProperty npcProperty)
        {
            if (BuffLoader.GetBuff(ModContent.BuffType<Buffs.Cloud.Electrified>()) is DisarrayBuff buff)
            {
                buff.NPCProperties = npcProperty;
            }
        }

        public override void AI(NPC npc)
        {
            int Chance = (npc.height + npc.width) / 2;
            if (Main.rand.Next(Chance) == 0 || Main.GameUpdateCount % 15 == 0)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Electricity>(), npc.velocity.X / 2, npc.velocity.Y / 2);
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 15;

            if (damage < 3)
            {
                damage = 3;
            }
        }
    }
}