using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Forge
{
	public partial class ForgePlayer : ModPlayer
	{
        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            if (player.HeldItem?.modItem is ForgeItem forgeItem)
            {
                forgeItem.HoldItem_Functional(player);
            }
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            damage = (int)(damage * Damage) + DamageFlat;
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(damage * Damage) + DamageFlat;
        }
    }
}