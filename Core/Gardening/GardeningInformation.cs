using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Disarray.Core.Gardening
{
	public class GardeningInformation
	{
		public GardeningInformation() { }

		public static IList<GardeningInformation> LoadedPlant = new List<GardeningInformation>();

		public static IDictionary<string, GardeningInformation> PlantIDs = new Dictionary<string, GardeningInformation>();

		public static IDictionary<int, Texture2D> PlantImageData = new Dictionary<int, Texture2D>();

		public int type;

		public string Name => GetType().Name;

		public virtual string Texture => (GetType().Namespace + "." + Name).Replace('.', '/');

		public string DisplayName;

		public float DifficultyRating;

		public float LightRequired;

		public float Thirstiness;

		public int LiquidType;

		public IDictionary<string, float> LikesAndDislikes = new Dictionary<string, float>();

		public string ConvertInfluencersToString()
		{
			string NewString = string.Empty;

			foreach (string Key in LikesAndDislikes.Keys)
			{
				if (LikesAndDislikes.TryGetValue(Key, out float Influence))
				{
					string InfluenceText = Influence * 100 + "%";
					NewString += Key + ": " + InfluenceText + "\n \n";
				}
			}

			return NewString;
		}

		public string Description;

		public virtual void SetDefaults() { }

		public static GardeningInformation GetPlant(int PlantID) => LoadedPlant[PlantID];

		public static int GetPlantID(string name) => PlantIDs.TryGetValue(name, out GardeningInformation plant) ? plant.type : 0;

		public static int InternalID = -1;

		public static void Load()
		{
			LoadedPlant = new List<GardeningInformation>();

			PlantIDs = new Dictionary<string, GardeningInformation>();

			PlantImageData = new Dictionary<int, Texture2D>();

			InternalID = -1;
		}

		public static void LoadType(Type item)
		{
			if (item.IsSubclassOf(typeof(GardeningInformation)))
			{
				GardeningInformation plant = Activator.CreateInstance(item) as GardeningInformation;
				plant.SetDefaults();
				plant.type = ++InternalID;
				ModLoader.GetMod("Disarray").Logger.InfoFormat(plant.Name + " | " + plant.type);
				LoadedPlant.Add(plant);
				PlantIDs.Add(plant.Name, plant);
				PlantImageData.Add(plant.type, ModContent.GetTexture(plant.Texture));
			}
		}

		public static void Unload()
        {
			LoadedPlant?.Clear();
			PlantIDs?.Clear();
			PlantImageData?.Clear();
			InternalID = 0;
		}
	}
}