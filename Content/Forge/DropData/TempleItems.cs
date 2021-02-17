using Disarray.Core.Data;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Disarray.Content.Forge.Items.Temple;

namespace Disarray.Content.Forge.DropData
{
    public class TempleItems : NPCDropData
    {
        public override void NPCLoot(NPC npc, string internalName)
        {
            if (npc.type == NPCID.FlyingSnake && Main.rand.Next(3) == 0)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<SnakesEye>());
            }
        }
    }
}