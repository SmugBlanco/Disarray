using Terraria;
using Terraria.ModLoader;
using Disarray.Core.Properties;
using Disarray.Core.Globals;
using Disarray.Content.Forge.Items.Marble;
using Terraria.ID;

namespace Disarray.Content.Forge.DropData
{
    public class Marble : NPCProperty
    {
        public override void PostLoad(NPCProperty npcProperty)
        {
            DisarrayGlobalNPC.GlobalProperties.Add(npcProperty);
        }

        public override void NPCLoot(NPC npc, string internalName)
        {
            if (Main.rand.Next(25) == 0 && (npc.type == NPCID.GreekSkeleton || npc.type == NPCID.Medusa))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<MarbleRelic>());
            }
        }
    }
}