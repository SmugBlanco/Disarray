using Disarray.Content.Forge.Items.Rusty;
using Disarray.Core.Data;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.DropData
{
	public class RustyItems : NPCDropData
	{
        public override void NPCLoot(NPC npc, string internalName)
        {
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
        }
    }
}