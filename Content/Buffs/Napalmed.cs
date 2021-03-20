using Disarray.Core.Properties;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Dusts;

namespace Disarray.Content.Buffs
{
	public class Napalmed : BuffProperties
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Napalmed");
			Description.SetDefault("'Some folks are born, made to wave the flag. They're red, white, and blue.'");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = true;
		}
	}

    public class NapalmedNPC : NPCProperty
    {
        public override void PostLoadType()
        {
            if (BuffLoader.GetBuff(ModContent.BuffType<Napalmed>()) is BuffProperties buff)
            {
                buff.NPCProperties = this;
            }
        }

        public override void AI(NPC npc)
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
                    indexedNPC.AddBuff(ModContent.BuffType<Napalmed>(), npc.buffTime[npc.FindBuffIndex(ModContent.BuffType<Napalmed>())]);
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
    }

    public class NapalmedPlayer : PlayerProperty
    {
        public override void PostLoadType()
        {
            if (BuffLoader.GetBuff(ModContent.BuffType<Napalmed>()) is BuffProperties buff)
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