using Disarray.Core.Autoload;
using Disarray.Gardening.Core.GE;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader.IO;

namespace Disarray.Gardening.Core
{
	[AutoloadedClass]
	public class PlantNeeds : AutoloadedClass
	{
		public static PlantNeeds CreateNewInstance(PlantNeeds needs, GardenEntity sourcePlant)
		{
			PlantNeeds plantNeeds = Activator.CreateInstance(needs.GetType()) as PlantNeeds;
			plantNeeds.Name = needs.Name;
			plantNeeds.Type = needs.Type;
			plantNeeds.SourcePlant = sourcePlant;
			return plantNeeds;
		}

		public GardenEntity SourcePlant { get; internal set; }

		public override bool Equals(object obj) => obj is PlantNeeds plantNeeds && plantNeeds.GetHashCode() == GetHashCode();

		public override int GetHashCode() => Type;

		//--------------------------------------------------------------------------

		public virtual int Sturdiness { get; set; }

		public int GetTimer { get => Timer; set => Timer = Utils.Clamp(value, 0, int.MaxValue); }

		private int Timer;

		public virtual string DisplayIcon => GetType().FullName.Replace('.', '/');

		public virtual bool FulfilledNeeds() => true;

		public virtual void Update() { }

		public virtual bool CanDisplayIcon() => false;

		public virtual void DisplayInformation() { }

		public virtual void DrawExtra(SpriteBatch spriteBatch) { }


		public virtual TagCompound Save() => null;

		public virtual void Load(TagCompound tagCompound) { }
	}
}