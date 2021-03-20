using Disarray.Gardening.Core.GE;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader.IO;

namespace Disarray.Gardening.Core.Needs
{
	public class Light : PlantNeeds
	{
		public override int Sturdiness => 54000;

		public int CheckInterval = 600;

		public float MinimumLight = 0.5f;

		public bool HasGottenLight;

		public override void Update()
		{
			GetTimer++;

			if (GetTimer % CheckInterval == 0)
			{
				HasGottenLight = LightCheck(SourcePlant, MinimumLight);

				if (HasGottenLight)
				{
					GetTimer -= CheckInterval * 2;
				}
			}
		}

		public override bool FulfilledNeeds() => GetTimer < Sturdiness;

		public override bool CanDisplayIcon() => GetTimer >= Sturdiness;

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

		public override void DisplayInformation()
		{
			Main.NewText("Light Needs: " + GetTimer + "/" + Sturdiness);
		}

		public override TagCompound Save() => new TagCompound() { { "Timer", GetTimer > Sturdiness ? Sturdiness : GetTimer } };

		public override void Load(TagCompound tagCompound) => GetTimer = tagCompound.Get<int>("Timer");
	}
}