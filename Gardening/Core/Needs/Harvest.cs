using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Disarray.Gardening.Core.Needs
{
	public class Harvest : PlantNeeds
	{
		public static int ScaleTimer;

		public float MinimumGrowth { get; set; } = 20;

		public override int Sturdiness { get; set; } = 86400;

		public override void Update()
		{
			ScaleTimer++;

			if (SourcePlant.GetGrowth >= MinimumGrowth)
			{
				GetTimer++;
			}
		}

		public override bool FulfilledNeeds() => GetTimer < Sturdiness;

		public override bool CanDisplayIcon() => GetTimer >= Sturdiness;

		public override void DrawExtra(SpriteBatch spriteBatch)
		{
			if (CanDisplayIcon())
			{
				Texture2D texture = ModContent.GetTexture("Disarray/Gardening/Core/Needs/HarvestStar");
				float colorSin = (float)(Math.Sin(GetTimer / 60f) / 20);
				if (colorSin > 0.025f)
				{
					colorSin = 0.025f + (colorSin - 0.025f) / 2;
				}
				float colorAlpha = (float)(0.1f + colorSin);
				float scaleSin = (float)(Math.Sin(ScaleTimer / 180f) / 2);
				float scale = 1.5f + scaleSin;
				spriteBatch.Draw(texture, SourcePlant.Position.ToWorldCoordinates() - Main.screenPosition, null, new Color(2f, 1.4f, 0.5f) * colorAlpha, MathHelper.ToRadians(ScaleTimer / 4f), texture.Size() / 2, scale, SpriteEffects.None, 0f);
			}
		}

		public override void DisplayInformation()
		{
			Main.NewText("Harvestable: " + GetTimer + "/" + Sturdiness);
		}

		public override TagCompound Save() => new TagCompound() { { "Timer", GetTimer > Sturdiness ? Sturdiness : GetTimer} };

		public override void Load(TagCompound tagCompound) => GetTimer = tagCompound.Get<int>("Timer");
	}
}