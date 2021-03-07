using Disarray.Core.Gardening;
using Disarray.Core.Globals;
using System;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace Disarray.Core.Data
{
	[AutoloadedClass]
	public class TileData : AutoloadedClass
	{
		public const int EntityCap = 500;

		public static TileData CreateNewEntity(TileData entityType)
		{
			TileData entity = Activator.CreateInstance(entityType.GetType()) as TileData;
			entity.Name = entityType.Name;
			entity.Type = entityType.Type;
			return entity;
		}

		public static bool PlaceEntity(Point16 position, string entityName)
		{
			if (DisarrayWorld.GardenEntitiesByPosition.Count >= EntityCap || DisarrayWorld.GardenEntitiesByPosition.ContainsKey(position))
			{
				return false;
			}

			TileData newEntity = CreateNewEntity(GetClass<TileData>().GetData<TileData>(entityName));
			newEntity.Position = position;
			DisarrayWorld.GardenEntitiesByPosition.Add(position, newEntity);
			newEntity.OnPlace();
			return true;
		}

		public static void KillEntity(Point16 position)
		{
			DisarrayWorld.GardenEntitiesByPosition[position].OnDestory();

			if (DisarrayWorld.GardenEntitiesByPosition.Remove(position))
			{
			}
		}

		public static void ExecuteAI()
		{
			GardenEntity.BobbingTimer++;

			foreach (TileData entity in DisarrayWorld.ActiveEntities)
			{
				if (entity.CanSurvive())
				{
					entity.AI();
				}
				else
				{
					KillEntity(entity.Position);
				}
			}
		}

		public Point16 Position { get; protected set; }

		public virtual bool CanSurvive() => true;

		public virtual void AI() { }

		public virtual void OnPlace() { }

		public virtual void OnDestory() { }

		public virtual TagCompound Save() => null;

		public virtual void Load(TagCompound tagCompound) { }
	}
}