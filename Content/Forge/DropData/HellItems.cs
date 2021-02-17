using Disarray.Core.Data;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Forge.Items.Hell;

namespace Disarray.Content.Forge.DropData
{
    public class HellItems : NPCDropData
    {
        public override void NPCLoot(NPC npc, string internalName)
        {
            if (Main.rand.Next(5) == 0 && (npc.type == NPCID.Demon || npc.type == NPCID.VoodooDemon))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<DemonsEye>());
            }
        }
    }
}