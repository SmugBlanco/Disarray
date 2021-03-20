using Terraria;
using Terraria.ModLoader.IO;

namespace Disarray.Gardening.Core.Needs
{
	public class Thirst : PlantNeeds
	{
		public override int Sturdiness { get; set; } = 18000;

		public override void Update()
		{
			GetTimer++;

			if (Main.raining)
			{
				GetTimer -= 4;
			}
		}

		public override bool FulfilledNeeds() =>  GetTimer < Sturdiness;

		public override bool CanDisplayIcon() => GetTimer >= Sturdiness;

		public override void DisplayInformation()
		{
			Main.NewText("Water Needs: " + GetTimer + "/" + Sturdiness);
		}

		public override TagCompound Save() => new TagCompound() { { "Timer", GetTimer > Sturdiness ? Sturdiness : GetTimer } };

		public override void Load(TagCompound tagCompound) => GetTimer = tagCompound.Get<int>("Timer");
	}
}