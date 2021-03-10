using Disarray.Core;
using Disarray.Core.Autoload;
using Disarray.Core.Gardening;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Gardening.Needs.PestTypes
{
	[AutoloadedClass]
	public class PestEntity : AutoloadedClass
	{
		public static PestEntity CreateNewInstance(PestEntity pest, GardenEntity sourcePlant)
		{
			if (pest is null)
			{
				Disarray.GetMod.Logger.Info("Null bitch lol");
			}
			PestEntity pestEntity = Activator.CreateInstance(pest.GetType()) as PestEntity;
			pestEntity.Name = pest.Name;
			pestEntity.Type = pest.Type;
			pestEntity.SourcePlant = sourcePlant;
			pestEntity.Position = new Vector2(sourcePlant.Position.X * 16, sourcePlant.Position.Y * 16);
			return pestEntity;
		}

		//------------------------------------------------------------

		public GardenEntity SourcePlant { get; internal set; }

		public virtual string TexturePath => GetType().FullName.Replace('.', '/');

		public Texture2D Texture => ModContent.GetTexture(TexturePath);

		public Vector2 Correction => Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);

		public Vector2 Position;

		public Vector2 Velocity;

		public virtual Color GetColor => Lighting.GetColor(SourcePlant.Position.X, SourcePlant.Position.Y);

		public float Rotation;

		public float Scale = 1f;

		public void Update()
		{
			Position += Velocity;
			AI();
		}

		public virtual void AI() { }

		public virtual bool CanSpawn(Pests pest, int timer) => true;

		public virtual void OnKill() { }

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Position - Main.screenPosition, null, GetColor, Rotation, Texture.Size() / 2, 1f, SpriteEffects.None, 0f);
		}
	}
}