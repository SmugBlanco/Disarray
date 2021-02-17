using Disarray.Content.Forge.Projectiles.Granite;
using Disarray.Core.Data;
using Disarray.Core.Globals;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.PlayerProperties
{
    public class GraniteEnergyRelease : PropertiesPlayer
    {
        public static void ImplementChance(Player player, int Strength, float Chance)
        {
            DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
            PropertiesPlayer property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is GraniteEnergyRelease);
            if (property is GraniteEnergyRelease graniteEnergyReleaseProperty)
            {
                graniteEnergyReleaseProperty.EffectStrength += Strength;
                graniteEnergyReleaseProperty.OrbChance += Chance;
            }
            else
            {
                player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties.Add(new GraniteEnergyRelease(Strength, Chance));
            }
        }

        public float DefaultEffectStrength = 1;

        public float EffectStrength = 0;

        public float TotalEffectStrength => DefaultEffectStrength + EffectStrength;

        public float DefaultOrbReleaseChance = 0.25f;

        public float OrbChance = 0f;

        public float TotalOrbChance => DefaultOrbReleaseChance + OrbChance;

        public GraniteEnergyRelease(float EffectStrength, float OrbChance)
        {
            this.EffectStrength += EffectStrength;
            this.OrbChance += OrbChance;
        }

        public override void PostUpdateMiscEffects(Player player)
        {
            Tile tile = Framing.GetTileSafely(player.Bottom);
            if (tile.type == TileID.Granite)
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