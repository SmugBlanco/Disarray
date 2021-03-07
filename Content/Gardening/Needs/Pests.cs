using Disarray.Content.Gardening.Needs.PestTypes;
using Disarray.Core.Gardening;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria.ModLoader.IO;

namespace Disarray.Content.Gardening.Needs
{
	public class Pests : PlantNeeds
	{
		public ICollection<PestEntity> CurrentPests = new Collection<PestEntity>();

		public sealed override bool FulfilledNeeds() => CurrentPests.Count == 0;

		public sealed override bool CanDisplayIcon() => CurrentPests.Count > 0;

		public sealed override string DisplayIcon => "Disarray/Content/Gardening/Needs/Pests";

		public override void Update()
		{
			foreach (PestEntity pests in CurrentPests)
			{
				pests.Update();
			}
		}

		public override void DisplayInformation()
		{
			Main.NewText("Number of Pests: " + CurrentPests.Count);
		}

		public sealed override void DrawExtra(SpriteBatch spriteBatch)
		{
			foreach (PestEntity pests in CurrentPests)
			{
				pests.Draw(spriteBatch);
			}
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