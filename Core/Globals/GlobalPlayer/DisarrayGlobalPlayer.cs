using Disarray.Extensions;
using Disarray.Core.Properties;
using Disarray.Gardening.Core;
using Disarray.Gardening.Core.GE;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Globals
{
    public partial class DisarrayGlobalPlayer : ModPlayer
    {
        public IEnumerable<PlayerProperty> ActiveProperties => ActiveBuffs.Concat(AutomaticallyRemovedProperties).Concat(ManuallyRemovedProperties).Concat(GlobalProperties).ToHashSet();

        public IEnumerable<PlayerProperty> ActiveBuffs
        {
            get
            {
                ICollection<PlayerProperty> activeBuffs = new Collection<PlayerProperty>();
                for (int indexer = 0; indexer < player.buffType.Length; indexer++)
                {
                    if (ModContent.GetModBuff(player.buffType[indexer]) is BuffProperties buff && buff.PlayerProperties != null)
                    {
                        activeBuffs.Add(buff.PlayerProperties);
                    }
                }
                return activeBuffs;
            }
        }

        public ICollection<PlayerProperty> AutomaticallyRemovedProperties = new HashSet<PlayerProperty>();

        public ICollection<PlayerProperty> ManuallyRemovedProperties = new HashSet<PlayerProperty>();

        public static ICollection<PlayerProperty> GlobalProperties = new HashSet<PlayerProperty>();

        public static void Load() => GlobalProperties = new Collection<PlayerProperty>();

        public static void Unload() => GlobalProperties?.Clear();

		public override void ModifyDrawLayers(List<PlayerLayer> layers)
		{
            try
			{
                foreach (GardenEntity entity in DisarrayWorld.ActiveEntities)
				{
                    foreach (PlantNeeds needs in entity.Needs)
                    {
                        needs.DrawExtra(Main.spriteBatch);
                    }
				}
			}
            catch
			{

			}
		}

		public override void ResetEffects() => AutomaticallyRemovedProperties.Clear();	

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.ModifyHitByNPC(player, npc, ref damage, ref crit);
            }
        }

		public override void OnHitByNPC(NPC npc, int damage, bool crit)
		{
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.OnHitByNPC(player, npc, damage, crit);
            }
        }

		public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.ModifyHitByProjectile(player, proj, ref damage, ref crit);
            }
        }

		public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
		{
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.OnHitByProjectile(player, proj, damage, crit);
            }
        }

		public override void PostUpdateMiscEffects()
        {
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.PostUpdateMiscEffects(player);
            }
        }

        public override void PostUpdateRunSpeeds()
        {
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.PostUpdateRunSpeeds(player);
            }
        }

        public override void PostUpdateBuffs()
        {
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.Update(player);
            }
        }

        public override void UpdateBadLifeRegen()
        {
            foreach (PlayerProperty properties in ActiveProperties)
            {
                properties.UpdateBadLifeRegen(player);
            }
        }
    }
}