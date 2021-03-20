using Terraria;
using Terraria.ModLoader.IO;

namespace Disarray.Gardening.Core.Needs
{
	public class Hunger : PlantNeeds
	{
		public override int Sturdiness => 86400;

		public override void Update() => GetTimer++;

		public override bool FulfilledNeeds() => GetTimer < Sturdiness;

		public override bool CanDisplayIcon() => GetTimer >= Sturdiness;

		public override void DisplayInformation()
		{
			Main.NewText("Nutrients Needs: " + GetTimer + "/" + Sturdiness);
		}

		public override TagCompound Save() => new TagCompound() { { "Timer", GetTimer > Sturdiness ? Sturdiness : GetTimer } };

		public override void Load(TagCompound tagCompound) => GetTimer = tagCompound.Get<int>("Timer");
	}
}