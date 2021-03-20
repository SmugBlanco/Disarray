using Terraria.DataStructures;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria;
using Microsoft.Xna.Framework;
using System.Linq;
using Disarray.Gardening.Core;

namespace Disarray.Core.Globals
{
	public class DisarrayWorld : ModWorld
	{
		public static IDictionary<Point16, TileData> GardenEntitiesByPosition;

		public static IEnumerable<TileData> ActiveEntities => GardenEntitiesByPosition.Values.ToArray();

		public override void Initialize()
		{
			GardenEntitiesByPosition = new Dictionary<Point16, TileData>();
		}

		public override void PostUpdate()
		{
			TileData.ExecuteAI();
		}

		public override TagCompound Save()
		{
			TagCompound worldTags = new TagCompound();

			if (ActiveEntities != null && GardenEntitiesByPosition.Count > 0)
			{
				int counter = 0;
				worldTags.Add("Count", ((ICollection<TileData>)ActiveEntities).Count);
				foreach (TileData entity in ActiveEntities)
				{
					TagCompound currentTag = new TagCompound()
					{
						{ "Name", entity.Name },
						{ "Position", entity.Position.ToVector2() },
						{ "Data", entity.Save() },
					};

					worldTags.Add("Entity" + counter++, currentTag);
				}
			}

			return worldTags;
		}

		public override void Load(TagCompound tag)
		{
			int entityCount = tag.Get<int>("Count");
			
			for (int indexer = 0; indexer < entityCount; indexer++)
			{
				TagCompound entityInformation = tag.Get<TagCompound>("Entity" + indexer);
				string entityName = entityInformation.Get<string>("Name");
				Point16 entityPosition = entityInformation.Get<Vector2>("Position").ToPoint16();
				if (TileData.PlaceEntity(entityPosition, entityName))
				{
					if (GardenEntitiesByPosition.TryGetValue(entityPosition, out TileData placedEntity))
					{
						TagCompound entityData = entityInformation.Get<TagCompound>("Data");
						placedEntity.Load(entityData);
					}
				}
			}
		}
	}
}