using Disarray.Content.Forge.Items.Cave;
using Disarray.Core.Globals;
using Disarray.Core.Properties;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.DropData
{
    public class Cavern : NPCProperty
    {
        public override void PostLoad(NPCProperty npcProperty)
        {
            DisarrayGlobalNPC.GlobalProperties.Add(npcProperty);
        }

        public override void NPCLoot(NPC npc, string internalName)
        {
            if (Main.rand.Next(3) == 0 && internalName.Contains("bat"))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<BatsEye>());
            }
        }
    }
}