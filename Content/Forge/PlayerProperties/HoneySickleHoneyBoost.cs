using Disarray.Core.Data;
using Disarray.Core.Globals;
using System.Linq;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.PlayerProperties
{
    public class HoneySickleHoneyBoost : PropertiesPlayer
    {
        public static void ImplementThis(Player player, int Count, float Chance)
        {
            DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
            PropertiesPlayer property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is HoneySickleHoneyBoost);
            if (property is HoneySickleHoneyBoost honeySickleHoneyBoostProperty)
            {
                honeySickleHoneyBoostProperty.Count += Count;
                honeySickleHoneyBoostProperty.AdditionalChance += Chance;
            }
            else
            {
                player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties.Add(new HoneySickleHoneyBoost(Count, Chance));
            }
        }

        public int Count;

        public float InnateChance = 0.2f;

        public float AdditionalChance;

        public float TotalChance => InnateChance + AdditionalChance;

        public HoneySickleHoneyBoost(int Count, float Chance)
        {
            this.Count += Count;
            AdditionalChance += Chance;
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