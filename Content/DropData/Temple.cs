using Terraria;
using Terraria.ModLoader;
using Disarray.Core.Properties;
using Disarray.Core.Globals;
using Terraria.ID;
using Disarray.Content.Forge.Items.Temple;

namespace Disarray.Content.Forge.DropData
{
    public class Temple : NPCProperty
    {
        public override void PostLoad(NPCProperty npcProperty)
        {
            DisarrayGlobalNPC.GlobalProperties.Add(npcProperty);
        }

        public override void NPCLoot(NPC npc, string internalName)
        {
            if (npc.type == NPCID.FlyingSnake && Main.rand.Next(3) == 0)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<SnakesEye>());
            }
        }
    }
}