using Terraria;
using Terraria.ModLoader;
using Disarray.Core.Properties;
using Disarray.Core.Globals;
using Terraria.ID;
using Disarray.Content.Forge.Items.Snow;

namespace Disarray.Content.Forge.DropData
{
    public class Snow : NPCProperty
    {
        public override void PostLoad(NPCProperty npcProperty)
        {
            DisarrayGlobalNPC.GlobalProperties.Add(npcProperty);
        }

        public override void NPCLoot(NPC npc, string internalName)
        {
            if (npc.type == NPCID.SnowFlinx)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<SnowFlinxsFur>());
            }
        }
    }
}