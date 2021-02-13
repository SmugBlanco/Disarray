using Disarray.Content.Forge.Dusts.Cloud;
using Disarray.Core.Data;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Buffs.Properties.Cloud
{
	public class Electrified : PropertiesBuffs
	{
        public override void Update(NPC npc)
        {
            int Chance = (npc.height + npc.width) / 2;
            if (Main.rand.Next(Chance) == 0 || Main.GameUpdateCount % 15 == 0)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Electricity>(), npc.velocity.X / 2, npc.velocity.Y / 2);
            }
        }

        public override void Update(Player player)
        {
            int Chance = (player.height + player.width) / 2;
            if (Main.rand.Next(Chance) == 0 || Main.GameUpdateCount % 15 == 0)
            {
                Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<Electricity>(), player.velocity.X / 2, player.velocity.Y / 2);
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 15;

            if (damage < 3)
            {
                damage = 3;
            }
        }

        public override void UpdateBadLifeRegen(Player player)
        {
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }

            player.lifeRegen -= 15;
        }
    }
}