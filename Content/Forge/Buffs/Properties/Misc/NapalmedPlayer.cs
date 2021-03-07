using Disarray.Content.Forge.Buffs.Misc;
using Disarray.Content.Forge.Dusts.Misc;
using Disarray.Core.Data;
using Disarray.Core.Properties;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Buffs.Properties.Misc
{
    public class NapalmedPlayer : PlayerProperty
    {
        public override void PostLoadType()
        {
            if (BuffLoader.GetBuff(ModContent.BuffType<Napalmed>()) is DisarrayBuff buff)
            {
                buff.PlayerProperties = this;
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
                    indexedPlayer.AddBuff(ModContent.BuffType<Napalmed>(), player.buffTime[player.FindBuffIndex(ModContent.BuffType<Napalmed>())]);
                }
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