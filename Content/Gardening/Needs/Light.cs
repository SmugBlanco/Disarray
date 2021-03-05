using Disarray.Core.Gardening;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader.IO;

namespace Disarray.Content.Gardening.Needs
{
	public class Light : PlantNeeds
	{
		public override int Sturdiness => 1800;

		public int CheckInterval = 600;

		public float MinimumLight = 0.5f;

		public bool HasGottenLight;

		public override void Update(GardenEntity gardenEntity)
		{
			GetTimer++;

			if (GetTimer % CheckInterval == 0)
			{
				HasGottenLight = LightCheck(gardenEntity, MinimumLight);

				if (HasGottenLight)
				{
					GetTimer = 0;
				}
			}
		}

		public override bool FulfilledNeeds(GardenEntity gardenEntity) => GetTimer < Sturdiness;

		public override bool CanDisplayIcon(GardenEntity gardenEntity) => GetTimer >= Sturdiness;

		public static float Average(params float[] input) //lmk if there is better alternative
		{
			float total = 0;
			for (int indexer = 0; indexer < input.Length; indexer++)
			{
				total += input[indexer];
			}
			return total / input.Length;
		}

		public static bool LightCheck(GardenEntity entity, float minimumLight, float forgiveness = 1.2f)
		{
			Vector2 worldPosition = entity.Position.ToWorldCoordinates();
			Vector3 light = Lighting.GetSubLight(worldPosition);
			return Average(light.X, light.Y, light.Z) * forgiveness >= minimumLight;
		}

		public override void DisplayInformation(GardenEntity gardenEntity)
		{
			Main.NewText("Light Needs: " + GetTimer + "/" + Sturdiness);
		}

		public override TagCompound Save()
		{
			return new TagCompound()
			{
				{ "Timer", GetTimer > Sturdiness ? Sturdiness : GetTimer}
			};
		}

		public override void Load(TagCompound tagCompound)
		{
			GetTimer = tagCompound.Get<int>("Timer");
		}
	}
}