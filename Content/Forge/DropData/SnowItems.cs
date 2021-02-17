using Disarray.Core.Data;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Forge.Items.Snow;

namespace Disarray.Content.Forge.DropData
{
    public class SnowItems : NPCDropData
    {
        public override void NPCLoot(NPC npc, string internalName)
        {
            if (npc.type == NPCID.SnowFlinx)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<SnowFlinxsFur>());
            }
        }
    }
}