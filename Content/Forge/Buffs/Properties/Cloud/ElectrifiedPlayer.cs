using Disarray.Content.Forge.Dusts.Cloud;
using Disarray.Core.Data;
using Disarray.Core.Properties;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Buffs.Properties.Cloud
{
	public class ElectrifiedPlayer : PlayerProperty
	{
		public override void PostLoad(PlayerProperty playerProperty)
		{
			if (BuffLoader.GetBuff(ModContent.BuffType<Buffs.Cloud.Electrified>()) is DisarrayBuff buff)
			{
                buff.PlayerProperties = playerProperty;
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