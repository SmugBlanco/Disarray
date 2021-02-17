using Disarray.Core.Data;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Forge.Items.Cloud;
using Cloud = Disarray.Content.Forge.Items.Cloud.Cloud;

namespace Disarray.Content.Forge.DropData
{
    public class SkyItems : NPCDropData
    {
        public override void NPCLoot(NPC npc, string internalName)
        {
            int CloudItemType = Main.maxRaining > 0.6f ? ModContent.ItemType<StormCloud>() : ModContent.ItemType<Cloud>();
            if (npc.type == NPCID.Harpy && Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.Hitbox, CloudItemType);
            }

            if (npc.type == NPCID.WyvernHead)
            {
                Item.NewItem(npc.Hitbox, CloudItemType);
            }
        }
    }
}