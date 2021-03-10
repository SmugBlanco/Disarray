using Disarray.Content.Gardening.Needs;
using Disarray.Core.Data;
using Disarray.Core.Gardening;
using Disarray.Core.Globals;
using Disarray.Core.Properties;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;

namespace Disarray.Content.Effects
{
	public class LoadPestillence : PlayerProperty // Deffo need to optimize this some day
	{
		public override void PostLoadType()
		{
			DisarrayGlobalPlayer.GlobalProperties.Add(this);
		}

		public ICollection<PlayerProperty> ActiveProperties(Player player) => (ICollection<PlayerProperty>)player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties;

		public LoadPestillenceData PestillenceData => GetClass<PlayerProperty>().GetData<LoadPestillenceData>();

		public override void PostUpdateMiscEffects(Player player)
		{
			float CheckDistanceFromNearestPest()
			{
				float minimumDistance = 160000;

				IEnumerable<TileData> gardenEntities = from GE in DisarrayWorld.ActiveEntities where GE is GardenEntity entity && entity.Needs.Contains(GetClass<PlantNeeds>().GetData<Pests>()) && (entity.Needs.First(pest => pest.Equals(GetClass<PlantNeeds>().GetData<Pests>())) as Pests).CurrentPests.Count > 0 select GE;

				foreach (GardenEntity gardenEntity in gardenEntities)
				{
					float distanceSQ = player.DistanceSQ(gardenEntity.Position.ToWorldCoordinates());
					if (distanceSQ < minimumDistance)
					{
						minimumDistance = distanceSQ;
					}
				}
				return minimumDistance;
			}

			float CurrentDistance = CheckDistanceFromNearestPest();

			if (CurrentDistance < 160000)
			{
				if (!ActiveProperties(player).Contains(PestillenceData))
				{
					ImplementProperty(player, CreateNewInstance<LoadPestillenceData>(), true);
				}

				LoadPestillenceData activeData = ActiveProperties(player).FirstOrDefault(pestData => pestData.Equals(PestillenceData)) as LoadPestillenceData;

				if (activeData != null)
				{
					activeData.Active = true;
				}

				if (Filters.Scene["Pestillence"].IsActive())
				{
					Filters.Scene["Pestillence"].GetShader().UseIntensity(CurrentDistance / 160000);
				}
			}
			else
			{
				LoadPestillenceData activeData = ActiveProperties(player).FirstOrDefault(pestData => pestData.Equals(PestillenceData)) as LoadPestillenceData;

				if (activeData != null)
				{
					activeData.Active = false;
				}
			}
		}
	}

	public class LoadPestillenceData : PlayerProperty
	{
		public bool Active;
		public const float MaxShade = 0.66f;
		public int TimeNearPestillence;

		public override void PostUpdateMiscEffects(Player player)
		{
			if (Main.netMode != NetmodeID.Server && Filters.Scene["Pestillence"].IsActive())
			{
				float progress = Utils.Clamp((TimeNearPestillence / 360f) * MaxShade, 0, MaxShade);
				Filters.Scene["Pestillence"].GetShader().UseProgress(progress).UseTargetPosition(player.Center);
			}

			if (Active)
			{
				TimeNearPestillence++;

				if (Main.netMode != NetmodeID.Server && !Filters.Scene["Pestillence"].IsActive())
				{
					Filters.Scene.Activate("Pestillence").GetShader().UseTargetPosition(player.Center);
				}

				return;
			}
			
			if (TimeNearPestillence > 360)
			{
				TimeNearPestillence = 360;
			}

			TimeNearPestillence -= 3;

			if (TimeNearPestillence <= 0 && Filters.Scene["Pestillence"].IsActive())
			{
				Filters.Scene["Pestillence"].Deactivate();
				player.GetModPlayer<DisarrayGlobalPlayer>().ManuallyRemovedProperties.Remove(this);
			}
		}
	}
}