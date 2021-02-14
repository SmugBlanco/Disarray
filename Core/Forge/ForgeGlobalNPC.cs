using Disarray.Content.Forge.Items.Ectoplasm;
using Disarray.Content.Forge.Items.Rusty;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Core.Forge
{
    public class ForgeGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            int RustyItemDropChance = Main.hardMode ? 50 : 20;
            if (npc.FullName.Contains("Zombie") && Main.rand.Next(RustyItemDropChance) == 0)
            {
                int[] RustyItems = new int[]
                {
                    ModContent.ItemType<RustyBow>(),
                    ModContent.ItemType<RustyCoil>(),
                    ModContent.ItemType<RustyPistol>(),
                    ModContent.ItemType<RustySword>(),
                    ModContent.ItemType<RustyTome>(),
                };

                Item.NewItem(npc.Hitbox, Utils.SelectRandom(Main.rand, RustyItems));
            }

            int CloudItemType = Main.maxRaining > 0.6f ? ModContent.ItemType<Content.Forge.Items.Cloud.StormCloud>() : ModContent.ItemType<Content.Forge.Items.Cloud.Cloud>();
            if (npc.type == NPCID.Harpy && Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.Hitbox, CloudItemType);
            }

            if (npc.type == NPCID.WyvernHead)
            {
                Item.NewItem(npc.Hitbox, CloudItemType);
            }

            if (npc.type == NPCID.DungeonSpirit && Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<MalignantSpirit>());
            }
        }
    }
}