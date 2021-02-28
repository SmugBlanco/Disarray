using Terraria;
using Terraria.ModLoader;
using Disarray.Core.Properties;
using Disarray.Core.Globals;
using Terraria.ID;
using Disarray.Content.Forge.Items.Overworld;

namespace Disarray.Content.Forge.DropData
{
    public class Overworld : NPCProperty
    {
        public override void PostLoad(NPCProperty npcProperty)
        {
            DisarrayGlobalNPC.GlobalProperties.Add(npcProperty);
        }

        public override void NPCLoot(NPC npc, string internalName)
        {
            if (Main.rand.Next(5) == 0 && (npc.type == NPCID.Parrot || npc.type == NPCID.Raven || npc.type == NPCID.Vulture || npc.type == NPCID.Harpy || internalName.Contains("bird")))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<BirdsEye>());
            }
        }
    }
}