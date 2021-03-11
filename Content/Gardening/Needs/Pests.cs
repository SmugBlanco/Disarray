using Disarray.Content.Gardening.Needs.PestTypes;
using Disarray.Core.Gardening;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria.ModLoader.IO;
using System.Linq;
using Disarray.Core.Extensions;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace Disarray.Content.Gardening.Needs
{
	public class Pests : PlantNeeds
	{
		public static int ScaleTimer = 0;

		public ICollection<PestEntity> CurrentPests = new Collection<PestEntity>();

		public sealed override bool FulfilledNeeds() => CurrentPests.Count == 0;

		public sealed override bool CanDisplayIcon() => CurrentPests.Count > 0;

		public sealed override string DisplayIcon => "Disarray/Content/Gardening/Needs/Pests";

		public ICollection<PestEntity> ApplicablePests = new HashSet<PestEntity>();

		public override int Sturdiness { get; set; } = 3600;

		public override void Update()
		{
			ScaleTimer++;

			GetTimer++;
			if (GetTimer % Sturdiness == 0)
			{
				foreach (PestEntity pests in ApplicablePests)
				{
					if (pests.CanSpawn(this, GetTimer))
					{
						CurrentPests.Add(PestEntity.CreateNewInstance(pests, SourcePlant));
					}
				}
			}

			foreach (PestEntity pests in CurrentPests)
			{
				pests.Update();
			}
		}

		public override void DisplayInformation()
		{
			Main.NewText(GetTimer + "/" + Sturdiness + " | Number of Pests: " + CurrentPests.Count);
		}

		public sealed override void DrawExtra(SpriteBatch spriteBatch)
		{
			foreach (PestEntity pests in CurrentPests)
			{
				pests.Draw(spriteBatch);
			}

			if (CurrentPests.Count == 0)
			{
				return;
			}

			Texture2D texture = ModContent.GetTexture("Terraria/Projectile_540");
			float colorSin = (float)(Math.Sin(GetTimer / 60f) / 10);
			if (colorSin > 0.05f)
			{
				colorSin = 0.05f + (colorSin - 0.05f) / 2;
			}
			float colorAlpha = (float)(0.1f + colorSin);
			float scaleSin = (float)(Math.Sin(ScaleTimer / 120f) / 2);
			float scale = 1.5f + (CurrentPests.Count * 0.5f) + scaleSin;
			spriteBatch.Draw(texture, SourcePlant.Position.ToWorldCoordinates() - Main.screenPosition, null, new Color(1.15f, 1.6f, 0.5f) * colorAlpha, 0f, texture.Size() / 2, scale, SpriteEffects.None, 0f);
		}

		public override TagCompound Save()
		{
			return new TagCompound()
			{
				{ "Timer", GetTimer > 999999 ? 999999 : GetTimer },
				{ nameof(CurrentPests), CurrentPests.Select(pest => pest.Name).ToList() },
			};
		}

		public override void Load(TagCompound tagCompound)
		{
			GetTimer = tagCompound.Get<int>("Timer");
			IList<string> savedPestsNames = tagCompound.Get<List<string>>(nameof(CurrentPests));
			IEnumerable<PestEntity> savedPests = savedPestsNames.Select(pest => PestEntity.CreateNewInstance(GetClass<PestEntity>().GetData<PestEntity>(pest), SourcePlant)).ToHashSet();
			CurrentPests = (ICollection<PestEntity>)savedPests;
		}
	}
}