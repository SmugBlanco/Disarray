using Terraria;
using Terraria.ModLoader;
using Disarray.Core.Properties;
using Disarray.Core.Globals;
using Disarray.Content.Forge.Items.Dungeon;
using Terraria.ID;

namespace Disarray.Content.Forge.DropData
{
    public class Dungeon : NPCProperty
    {
        public override void PostLoad(NPCProperty npcProperty)
        {
            DisarrayGlobalNPC.GlobalProperties.Add(npcProperty);
        }

        public override void NPCLoot(NPC npc, string internalName)
        {
            if (Main.rand.Next(NPC.downedBoss3 ? 33 : 100) == 0 && (internalName.Contains("skeleton") || internalName.Contains("bone")))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<SkeletonsFemur>());
            }

            if (npc.type == NPCID.DungeonSpirit && Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<MalignantSpirit>());
            }
        }
    }
}