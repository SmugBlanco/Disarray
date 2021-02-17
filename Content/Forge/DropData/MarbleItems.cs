using Disarray.Core.Data;
using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Forge.Items.Marble;
using Terraria.ID;

namespace Disarray.Content.Forge.DropData
{
    public class MarbleItems : NPCDropData
    {
        public override void NPCLoot(NPC npc, string internalName)
        {
            if (Main.rand.Next(25) == 0 && (npc.type == NPCID.GreekSkeleton || npc.type == NPCID.Medusa))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<MarbleRelic>());
            }
        }
    }
}