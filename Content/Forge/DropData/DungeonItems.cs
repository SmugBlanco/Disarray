using Disarray.Core.Data;
using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Forge.Items.Dungeon;
using Terraria.ID;

namespace Disarray.Content.Forge.DropData
{
    public class DungeonItems : NPCDropData
    {
        public override void NPCLoot(NPC npc, string internalName)
        {
            int FemurDropChance = NPC.downedBoss3 ? 33 : 100;
            if (Main.rand.Next(FemurDropChance) == 0 && (npc.FullName.Contains("Skeleton") || npc.FullName.Contains("Bone")))
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