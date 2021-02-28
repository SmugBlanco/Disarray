using Terraria;
using Terraria.ModLoader;
using Disarray.Core.Properties;
using Disarray.Core.Globals;
using Terraria.ID;
using Disarray.Content.Forge.Items.Ocean;

namespace Disarray.Content.Forge.DropData
{
    public class Ocean : NPCProperty
    {
        public override void PostLoad(NPCProperty npcProperty)
        {
            DisarrayGlobalNPC.GlobalProperties.Add(npcProperty);
        }

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