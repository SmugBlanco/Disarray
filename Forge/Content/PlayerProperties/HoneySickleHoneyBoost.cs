using Disarray.Core.Properties;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.PlayerProperties
{
    public class HoneySickleHoneyBoost : PlayerProperty
    {
        public int Count;

        public float InnateChance = 0.2f;

        public float AdditionalChance;

        public float TotalChance => InnateChance + AdditionalChance;

        public override void Combine(PlayerProperty newProperty)
        {
            if (newProperty is HoneySickleHoneyBoost property)
            {
                Count += property.Count;
                AdditionalChance += property.AdditionalChance;
            }
        }

        public override void PostUpdateMiscEffects(Player player)
        {
            player.lifeRegen += Count / 3;
        }

        public override void OnHitNPC(Player player, Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat(1) < TotalChance)
            {
                player.AddBuff(BuffID.Honey, 300);
            }
        }

        public override void OnHitNPCWithProj(Player player, Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat(1) < TotalChance)
            {
                player.AddBuff(BuffID.Honey, 300);
            }
        }
    }
}