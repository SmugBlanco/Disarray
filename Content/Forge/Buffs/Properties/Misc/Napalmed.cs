using Disarray.Content.Forge.Dusts.Misc;
using Disarray.Core.Data;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Buffs.Properties.Misc
{
    public class Napalmed : PropertiesBuffs
    {
        public override void Update(NPC npc)
        {
            int Chance = (npc.height + npc.width) / 3;
            if (Main.rand.Next(Chance) == 0 || Main.GameUpdateCount % 10 == 0)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Napalm>(), npc.velocity.X / 2, npc.velocity.Y / 2);
            }

            for (int Indexer = 0; Indexer < Main.npc.Length - 1; Indexer++)
            {
                NPC indexedNPC = Main.npc[Indexer];
                if (npc.whoAmI != indexedNPC.whoAmI && npc.Distance(indexedNPC.Center) < (npc.width + indexedNPC.width) / 2 && Main.rand.NextFloat(1) < 0.025f)
                {
                    indexedNPC.AddBuff(ModBuff.Type, npc.buffTime[npc.FindBuffIndex(ModBuff.Type)]);
                }
            }
        }

        public override void Update(Player player)
        {
            int Chance = (player.height + player.width) / 3;
            if (Main.rand.Next(Chance) == 0 || Main.GameUpdateCount % 10 == 0)
            {
                Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<Napalm>(), player.velocity.X / 2, player.velocity.Y / 2);
            }

            for (int Indexer = 0; Indexer < Main.player.Length - 1; Indexer++)
            {
                Player indexedPlayer = Main.player[Indexer];
                if (player.whoAmI != indexedPlayer.whoAmI && player.Distance(indexedPlayer.Center) < (player.width + indexedPlayer.width) / 2 && Main.rand.NextFloat(1) < 0.025f)
                {
                    indexedPlayer.AddBuff(ModBuff.Type, player.buffTime[player.FindBuffIndex(ModBuff.Type)]);
                }
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            int DoTAdjusted = 40;
            int damageAdjusted = 3;
            if (npc.FullName.Contains("Slime") || npc.type == NPCID.Gastropod)
            {
                DoTAdjusted = 120;
                damageAdjusted = 9;
            }

            if (npc.aiStyle == 40 || npc.FullName.Contains("Spider"))
            {
                damageAdjusted = 6;
            }

            npc.lifeRegen -= DoTAdjusted;

            if (damage < damageAdjusted)
            {
                damage = damageAdjusted;
            }
        }

        public override void UpdateBadLifeRegen(Player player)
        {
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }

            player.lifeRegen -= 25;
        }
    }
}