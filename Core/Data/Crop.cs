using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria.UI;
using System;
using Terraria.ID;
using System.Linq;
using Terraria.GameInput;
using System.IO;
using Terraria.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using System.Reflection;

namespace Disarray.Core.Data
{
	public class Crop
	{
		public Crop() { }

		public static List<Crop> LoadedCrops = new List<Crop>();

		public static Dictionary<string, Crop> CropIDs = new Dictionary<string, Crop>();

		public static Dictionary<int, Texture2D> CropsImageData = new Dictionary<int, Texture2D>();

		public int type;

		public string name => GetType().Name;

		public virtual string texture => (GetType().Namespace + "." + name).Replace('.', '/');

		public string DisplayName;

		public string Description;

		public string Origin;

		public string PlantingMonths;

		public string HarvestMonths;

		public string PricePerPound;

		public List<int> MonthsInSeason = new List<int>();

		public static explicit operator int(Crop crop)
		{
			return crop.type;
		}

		public virtual void SetDefaults()
		{

		}

		public static Crop GetCrop(int CropID)
		{
			return LoadedCrops[CropID];
		}

		public static int GetCropID(string name)
		{
			return CropIDs.TryGetValue(name, out Crop crop) ? crop.type : 0;
		}

		public static int InternalID = -1;

		public static void AutoloadCrops(Assembly assembly)
		{
			LoadedCrops = new List<Crop>();

			CropIDs = new Dictionary<string, Crop>();

			CropsImageData = new Dictionary<int, Texture2D>();

			InternalID = -1;

			if (assembly == null)
			{
				return;
			}

			foreach (Type item in assembly.GetTypes())
			{
				if (!item.IsAbstract && item.IsSubclassOf(typeof(Crop)) && !(item.GetConstructor(new Type[0]) == null))
				{
					Crop crop = Activator.CreateInstance(item) as Crop;
					crop.SetDefaults();
					crop.type = ++InternalID;
					ModLoader.GetMod("Disarray").Logger.InfoFormat(crop.name + " | " + crop.type);
					LoadedCrops.Add(crop);
					CropIDs.Add(crop.name, crop);
					CropsImageData.Add(crop.type, ModContent.GetTexture(crop.texture));
				}
			}
		}

		public static void Unload()
        {
			LoadedCrops.Clear();
			CropIDs.Clear();
			CropsImageData.Clear();
			InternalID = 0;
		}
	}
}