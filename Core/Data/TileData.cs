using Disarray.Core.Globals;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace Disarray.Core.Data
{
	public class TileData
	{
		public const int EntityCap = 500;

		private static int InternalIDCount;

		public static IList<TileData> LoadedEntities;

		public static IDictionary<string, TileData> LoadedEntitiesByName;

		public static IDictionary<Point16, TileData> EntityByPosition;

		public int Type { get; internal set; }

		public string Name { get; internal set; }

		public static void Load()
		{
			InternalIDCount = -1;
			LoadedEntities = new List<TileData>();
			LoadedEntitiesByName = new Dictionary<string, TileData>();
		}

		public static void LoadType(Type item)
		{
			if (item.IsSubclassOf(typeof(TileData)))
			{
				TileData entity = Activator.CreateInstance(item) as TileData;
				entity.Type = ++InternalIDCount;
				entity.Name = item.Name;
				LoadedEntities.Add(entity);
				LoadedEntitiesByName.Add(entity.Name, entity);
			}
		}

		public static void Unload()
		{
			InternalIDCount = 0;
			LoadedEntities?.Clear();
			LoadedEntitiesByName?.Clear();
			EntityByPosition?.Clear();
		}

		public static TileData GetTileDara(int Type) => (Type < 0 || Type >= LoadedEntities.Count) ? null : LoadedEntities[Type];

		public static TileData GetTileData(string Name) => LoadedEntitiesByName.TryGetValue(Name, out TileData entity) ? entity : null;

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

			TileData newEntity = CreateNewEntity(GetTileData(entityName));
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