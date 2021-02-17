using Disarray.Core.Data;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Forge.Items.Overworld;

namespace Disarray.Content.Forge.DropData
{
    public class OverworldItems : NPCDropData
    {
        public override void NPCLoot(NPC npc, string internalName)
        {
            if (Main.rand.Next(5) == 0 && (npc.type == NPCID.Parrot || npc.type == NPCID.Raven || npc.type == NPCID.Vulture || npc.type == NPCID.Harpy || internalName.Contains("Bird")))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<BirdsEye>());
            }
        }
    }
}