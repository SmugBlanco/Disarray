using Disarray.Content.Forge.Projectiles.Marble;
using Disarray.Core.Data;
using Disarray.Core.Globals;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.PlayerProperties
{
    public class MarbleEnergyRelease : PropertiesPlayer
    {
        public static void ImplementChance(Player player, int Strength, float Chance)
        {
            DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
            PropertiesPlayer property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is MarbleEnergyRelease);
            if (property is MarbleEnergyRelease marbleEnergyReleaseProperty)
            {
                marbleEnergyReleaseProperty.EffectStrength += Strength;
                marbleEnergyReleaseProperty.OrbChance += Chance;
            }
            else
            {
                player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties.Add(new MarbleEnergyRelease(Strength, Chance));
            }
        }

        public float DefaultEffectStrength = 1;

        public float EffectStrength = 0;

        public float TotalEffectStrength => DefaultEffectStrength + EffectStrength;

        public float DefaultOrbReleaseChance = 0.25f;

        public float OrbChance = 0f;

        public float TotalOrbChance => DefaultOrbReleaseChance + OrbChance;

        public MarbleEnergyRelease(float EffectStrength, float OrbChance)
        {
            this.EffectStrength += EffectStrength;
            this.OrbChance += OrbChance;
        }

        public override void PostUpdateMiscEffects(Player player)
        {
            Tile tile = Framing.GetTileSafely(player.Bottom);
            if (tile.type == TileID.Marble)
            {
                player.allDamage += 0.01f * TotalEffectStrength;
                player.endurance += 0.01f * TotalEffectStrength;
                player.manaRegen += (int)(1 * TotalEffectStrength);
            }
        }

        public override void OnHitNPC(Player player, Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat(1) < TotalOrbChance && player.ownedProjectileCounts[ModContent.ProjectileType<MarbleEnergyOrb>()] < TotalEffectStrength)
            {
                Projectile.NewProjectile(target.Top, new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3)), ModContent.ProjectileType<MarbleEnergyOrb>(), damage, knockback, player.whoAmI, 0, 0);
            }
        }

        public override void OnHitNPCWithProj(Player player, Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat(1) < TotalOrbChance && player.ownedProjectileCounts[ModContent.ProjectileType<MarbleEnergyOrb>()] < TotalEffectStrength && proj.type != ModContent.ProjectileType<MarbleEnergyOrb>())
            {
                Projectile.NewProjectile(target.Top, new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3)), ModContent.ProjectileType<MarbleEnergyOrb>(), damage, knockback, player.whoAmI, 0, 0);
            }
        }
    }
}