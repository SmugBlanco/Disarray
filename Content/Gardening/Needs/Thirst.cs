using Disarray.Core.Gardening;
using Terraria;
using Terraria.ModLoader.IO;

namespace Disarray.Content.Gardening.Needs
{
	public class Thirst : PlantNeeds
	{
		public override int Sturdiness => 18000;

		public override void Update(GardenEntity gardenEntity) => GetTimer++;

		public override bool FulfilledNeeds(GardenEntity gardenEntity) =>  GetTimer < Sturdiness;

		public override bool CanDisplayIcon(GardenEntity gardenEntity) => GetTimer >= Sturdiness;

		public override void DisplayInformation(GardenEntity gardenEntity)
		{
			Main.NewText("Water Needs: " + GetTimer + "/" + Sturdiness);
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