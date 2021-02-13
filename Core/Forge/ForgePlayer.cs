using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Forge
{
	public partial class ForgePlayer : ModPlayer
	{
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            damage = (int)((float)damage * Damage) + DamageFlat;
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)((float)damage * Damage) + DamageFlat;
        }
    }
}