using Disarray.Core.Data;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Forge.Items.Ocean;

namespace Disarray.Content.Forge.DropData
{
    public class OceanItems : NPCDropData
    {
        public override void NPCLoot(NPC npc, string internalName)
        {
            if (npc.type == NPCID.Shark)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<SharkTooth>());
            }

            if (npc.type == NPCID.DukeFishron)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<FishronsTusk>(), 2);
            }
        }
    }
}