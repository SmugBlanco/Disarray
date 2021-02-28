using Disarray.Content.Forge.Projectiles.Granite;
using Disarray.Core.Properties;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.PlayerProperties
{
    public class GraniteEnergyRelease : PlayerProperty
    {
        public float DefaultEffectStrength = 1;

        public float EffectStrength = 0;

        public float TotalEffectStrength => DefaultEffectStrength + EffectStrength;

        public float DefaultOrbReleaseChance = 0.25f;

        public float OrbChance = 0f;

        public float TotalOrbChance => DefaultOrbReleaseChance + OrbChance;

        public override void Combine(PlayerProperty newProperty)
        {
            if (newProperty is GraniteEnergyRelease property)
            {
                EffectStrength += property.EffectStrength;
                OrbChance += property.OrbChance;
            }
        }

        public override void PostUpdateMiscEffects(Player player)
        {
            if (Framing.GetTileSafely(player.Bottom).type == TileID.Granite)
            {
                player.allDamage += 0.01f * TotalEffectStrength;
                player.statDefense += (int)(2 * TotalEffectStrength);
                player.lifeRegen += (int)(1 * TotalEffectStrength);
            }
        }

        public override void OnHitNPC(Player player, Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat(1) < TotalOrbChance && player.ownedProjectileCounts[ModContent.ProjectileType<GraniteEnergyOrb>()] < TotalEffectStrength)
            {
                Projectile.NewProjectile(target.Top, new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3)), ModContent.ProjectileType<GraniteEnergyOrb>(), damage, knockback, player.whoAmI, 0, 0);
            }
        }

        public override void OnHitNPCWithProj(Player player, Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat(1) < TotalOrbChance && player.ownedProjectileCounts[ModContent.ProjectileType<GraniteEnergyOrb>()] < TotalEffectStrength && proj.type != ModContent.ProjectileType<GraniteEnergyOrb>())
            {
                Projectile.NewProjectile(target.Top, new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3)), ModContent.ProjectileType<GraniteEnergyOrb>(), damage, knockback, player.whoAmI, 0, 0);
            }
        }
    }
}