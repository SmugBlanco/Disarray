using Disarray.Content.Forge.Items.Cave;
using Disarray.Core.Data;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.DropData
{
    public class CaveItems : NPCDropData
    {
        public override void NPCLoot(NPC npc, string internalName)
        {
            if (Main.rand.Next(3) == 0 && npc.FullName.Contains("Bat"))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<BatsEye>());
            }
        }
    }
}