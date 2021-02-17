using Disarray.Core.Data;
using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Forge.Items.Desert;

namespace Disarray.Content.Forge.DropData
{
    public class DesertItem : NPCDropData
    {
        public override void NPCLoot(NPC npc, string internalName)
        {
            if (Main.rand.Next(6) == 0 && (internalName.Contains("Sandshark") || internalName.Contains("SandShark")))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<SandsharksMaw>());
            }
        }
    }
}