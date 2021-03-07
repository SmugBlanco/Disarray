using Terraria;
using Terraria.ModLoader;
using Disarray.Core.Properties;
using Disarray.Core.Globals;
using Disarray.Content.Forge.Items.Granite;

namespace Disarray.Content.Forge.DropData
{
    public class Granite : NPCProperty
    {
        public override void PostLoadType()
        {
            DisarrayGlobalNPC.GlobalProperties.Add(this);
        }

        public override void NPCLoot(NPC npc, string internalName)
        {
            if (Main.rand.Next(25) == 0 && internalName.Contains("granite"))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<GraniteRelic>());
            }
        }
    }
}