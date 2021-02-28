using Terraria;
using Terraria.ModLoader;
using Disarray.Core.Properties;
using Disarray.Core.Globals;
using Terraria.ID;
using Disarray.Content.Forge.Items.Hell;

namespace Disarray.Content.Forge.DropData
{
    public class Underworld : NPCProperty
    {
        public override void PostLoad(NPCProperty npcProperty)
        {
            DisarrayGlobalNPC.GlobalProperties.Add(npcProperty);
        }

        public override void NPCLoot(NPC npc, string internalName)
        {
            if (Main.rand.Next(5) == 0 && (npc.type == NPCID.Demon || npc.type == NPCID.VoodooDemon))
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<DemonsEye>());
            }
        }
    }
}