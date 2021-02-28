using Terraria;
using Terraria.ModLoader;
using Disarray.Core.Properties;
using Disarray.Core.Globals;
using Terraria.ID;
using Disarray.Content.Forge.Items.Cloud;

namespace Disarray.Content.Forge.DropData
{
    public class Space : NPCProperty
    {
        public override void PostLoad(NPCProperty npcProperty)
        {
            DisarrayGlobalNPC.GlobalProperties.Add(npcProperty);
        }

        public override void NPCLoot(NPC npc, string internalName)
        {
            int CloudItemType = Main.maxRaining > 0.6f ? ModContent.ItemType<StormCloud>() : ModContent.ItemType<Items.Cloud.Cloud>();
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