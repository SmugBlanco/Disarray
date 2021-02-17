using Disarray.Core.Data;
using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Forge.Items.Granite;

namespace Disarray.Content.Forge.DropData
{
    public class GraniteItems : NPCDropData
    {
        public override void NPCLoot(NPC npc, string internalName)
        {
            if (Main.rand.Next(25) == 0 && internalName.Contains("Granite"))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<GraniteRelic>());
            }
        }
    }
}