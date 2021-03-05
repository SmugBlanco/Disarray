using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader.IO;

namespace Disarray.Core.Gardening
{
	public abstract class PlantNeeds
	{
		public static PlantNeeds CreateNewInstance(PlantNeeds needs)
		{
			PlantNeeds plantNeeds = Activator.CreateInstance(needs.GetType()) as PlantNeeds;
			plantNeeds.Name = needs.Name;
			plantNeeds.Type = needs.Type;
			return plantNeeds;
		}

		public static IList<PlantNeeds> LoadedPlantNeeds;

		public static int InternalIDCount;

		public static IDictionary<string, PlantNeeds> PlantNeedsByName;

		public static void Load()
		{
			LoadedPlantNeeds = new List<PlantNeeds>();

			InternalIDCount = 0;

			PlantNeedsByName = new Dictionary<string, PlantNeeds>();
		}

		public static void LoadType(Type item)
		{
			if (item.IsSubclassOf(typeof(PlantNeeds)))
			{
				PlantNeeds plantNeeds = Activator.CreateInstance(item) as PlantNeeds;
				plantNeeds.Type = InternalIDCount++;
				plantNeeds.Name = item.Name;
				LoadedPlantNeeds.Add(plantNeeds);
				PlantNeedsByName.Add(plantNeeds.Name, plantNeeds);
			}
		}

		public static void Unload()
		{
			LoadedPlantNeeds?.Clear();

			InternalIDCount = 0;

			PlantNeedsByName?.Clear();
		}

		public static PlantNeeds GetPlantNeeds(int ID)
		{
			if (ID < 0 || ID >= LoadedPlantNeeds.Count)
			{
				return null;
			}

			return LoadedPlantNeeds[ID];
		}

		public static PlantNeeds GetPlantNeeds(string name)
		{
			if (PlantNeedsByName.TryGetValue(name, out PlantNeeds property))
			{
				return property;
			}

			return null;
		}

		public int Type { get; internal set; }

		public string Name { get; internal set; }

		public override bool Equals(object obj) => obj is PlantNeeds plantNeeds && plantNeeds.GetHashCode() == GetHashCode();

		public override int GetHashCode() => Type;

		//--------------------------------------------------------------------------

		public virtual int Sturdiness { get; protected set; }

		public int GetTimer { get => Timer; set => Timer = Utils.Clamp(value, 0, int.MaxValue); }

		private int Timer;

		public virtual string DisplayIcon => GetType().FullName.Replace('.', '/');

		public abstract bool FulfilledNeeds(GardenEntity gardenEntity);

		public virtual void Update(GardenEntity gardenEntity) { }

		public abstract bool CanDisplayIcon(GardenEntity gardenEntity);

		public virtual void DisplayInformation(GardenEntity gardenEntity) { }

		public virtual TagCompound Save() => null;

		public virtual void Load(TagCompound tagCompound) { }
	}
}