using Disarray.Content.Forge.Items.Rusty;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Forge
{
    public class ForgeGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            int RustyItemDropChance = Main.hardMode ? 100 : 20;
            if (npc.GivenName.Contains("Zombie") && Main.rand.Next(RustyItemDropChance) == 0)
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
        }
    }
}