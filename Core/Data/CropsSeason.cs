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

namespace Disarray.Core.Data
{
	public static class CropsSeason
	{
		public enum Crops
        {
			Apples,
			Asparagus,
			BeetGreens,
			Beets,
			Blackberries,

			Blueberries,
			Broccoli,
			BrusselSprouts,
			Cabbages,
			Cantaloupe,
			Carrots,
			Cauliflower,
			Celery,
			Cherries,
			CollardGreens,
			Corn,
			Cucumbers,
			Currants,
			DriedBeans,
			Eggplant,
			Garlic,
			Grapes,
			Herbs,
			Kale,
			Leaks,
			Lettuce,
			LimaBeans,
			MustardGreens,
			Onions,
			Parsnips,
			Peaches,
			Pears,
			Peas,
			Peppers,
			Plums,
			Potatoes,
			Prunes,
			Pumpkin,
			Radishes,
			Rhubarb,
			Raspberries,
			SnapBeans,
			Spinach,
			Strawberries,
			SummerSquash,
			SwissChard,
			Tomatoes,
			TurnipGreens,
			Turnips,
			Watermelon,
			WinterSquash,
			Zucchini,
		}

		public static List<int> LoadedCrops = new List<int>();

		public static Dictionary<Crops, List<int>> CropsPeakData = new Dictionary<Crops, List<int>>();

		public static Dictionary<Crops, Texture2D> CropsImageData = new Dictionary<Crops, Texture2D>();

		public static bool IsCropInSeason(Crops crop, DateTime refTime)
        {
			CropsPeakData.TryGetValue(crop, out List<int> SeasonalMonths);
			if (SeasonalMonths.Contains(refTime.Month))
            {
				return true;
            }
			return false;
        }
	}
}