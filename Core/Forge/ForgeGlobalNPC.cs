using Disarray.Content.Forge.Items.Bats;
using Disarray.Content.Forge.Items.Birds;
using Disarray.Content.Forge.Items.Bones;
using Disarray.Content.Forge.Items.Demons;
using Disarray.Content.Forge.Items.DukeFishron;
using Disarray.Content.Forge.Items.Ectoplasm;
using Disarray.Content.Forge.Items.Rusty;
using Disarray.Content.Forge.Items.Sharks;
using Disarray.Content.Forge.Items.Snakes;
using Disarray.Content.Forge.Items.Snow;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Core.Forge
{
    public class ForgeGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            string NPCIDName = string.Empty;
            if (NPCID.Search.TryGetName(npc.type, out string Name))
            {
                NPCIDName = Name;
            }

            int RustyItemDropChance = Main.hardMode ? 50 : 20;
            if (Main.rand.Next(RustyItemDropChance) == 0 && npc.FullName.Contains("Zombie"))
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

            if (Main.rand.Next(3) == 0 && npc.FullName.Contains("Bat"))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<BatsEye>());
            }

            if (npc.type == NPCID.SnowFlinx)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<SnowFlinxsFur>());
            }

            if (Main.rand.Next(5) == 0 && (npc.type == NPCID.Parrot || npc.type == NPCID.Raven || npc.type == NPCID.Vulture || npc.type == NPCID.Harpy || Name.Contains("Bird")))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<BirdsEye>());
            }

            if (npc.type == NPCID.Shark)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<SharkTooth>());
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

            int FemurDropChance = NPC.downedBoss3 ? 33 : 100;
            if (Main.rand.Next(FemurDropChance) == 0 && (npc.FullName.Contains("Skeleton") || npc.FullName.Contains("Bone")))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<SkeletonsFemur>());
            }

            if (Main.rand.Next(5) == 0 && (npc.type == NPCID.Demon || npc.type == NPCID.VoodooDemon))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<DemonsEye>());
            }

            if (Main.rand.Next(6) == 0 && (NPCIDName.Contains("Sandshark") || NPCIDName.Contains("SandShark")))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<SandsharksMaw>());
            }

            if (npc.type == NPCID.DungeonSpirit && Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<MalignantSpirit>());
            }

            if (npc.type == NPCID.FlyingSnake && Main.rand.Next(3) == 0)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<SnakesEye>());
            }

            if (npc.type == NPCID.DukeFishron)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<FishronsTusk>(), 2);
            }
        }
    }
}