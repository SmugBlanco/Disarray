using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Disarray.Core.Map
{
	public struct MapEntry
	{
		public MapEntry(Texture2D texture, Vector2 worldPosition, Rectangle? sourceRectangle = null, Color? drawColor = null, float rotation = 0f, Vector2 origin = default, float scale = 1f, SpriteEffects spriteEffects = SpriteEffects.None)
		{
			WorldPosition = worldPosition;
			Texture = texture;
			SourceRectangle = sourceRectangle;

			if (drawColor.HasValue)
			{
				DrawColor = drawColor.Value;
			}
			else
			{
				DrawColor = Color.White;
			}

			Rotation = rotation;
			Origin = origin;
			Scale = scale;
			SpriteEffects = spriteEffects;
		}

		public Texture2D Texture;

		public Vector2 WorldPosition;

		public Rectangle? SourceRectangle;

		public Color DrawColor;

		public float Rotation;

		public Vector2 Origin;

		public float Scale;

		public SpriteEffects SpriteEffects;
	}
}