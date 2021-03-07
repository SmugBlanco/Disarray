using Disarray.Content.Forge.Items.Rusty;
using Disarray.Core.Globals;
using Disarray.Core.Properties;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.DropData
{
    public class RustyItems : NPCProperty
    {
        public override void PostLoadType()
        {
            DisarrayGlobalNPC.GlobalProperties.Add(this);
        }

        public override void NPCLoot(NPC npc, string internalName)
        {
            if (Main.rand.Next(Main.hardMode ? 50 : 20) == 0 && internalName.Contains("zombie"))
            {
                int droppedItem = Utils.SelectRandom(Main.rand, ModContent.ItemType<RustyBow>(), ModContent.ItemType<RustyCoil>(), ModContent.ItemType<RustyPistol>(), ModContent.ItemType<RustySword>(), ModContent.ItemType<RustyTome>());
                Item.NewItem(npc.Hitbox, droppedItem);
            }
        }
    }
}