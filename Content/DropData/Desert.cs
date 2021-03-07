using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Forge.Items.Desert;
using Disarray.Core.Properties;
using Disarray.Core.Globals;

namespace Disarray.Content.Forge.DropData
{
    public class Desert : NPCProperty
    {
        public override void PostLoadType()
        {
            DisarrayGlobalNPC.GlobalProperties.Add(this);
        }

        public override void NPCLoot(NPC npc, string internalName)
        {
            if (Main.rand.Next(6) == 0 && internalName.Contains("sandshark"))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<SandsharksMaw>());
            }
        }
    }
}