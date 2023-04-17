using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.SmartTextures
{
	public class Bar
	{
		private static readonly Rectangle leftCircleSourceRect = new Rectangle(0, 0, 32, 64);
		private static readonly Rectangle rightCircleSourceRect = new Rectangle(32, 0, 32, 64);
		private static readonly Vector2 offset = new Vector2(32, 0);

		private Vector2 position;
		private Vector2 size;
		private Color color;
		private float layerDepth;

		private Texture2D rectTexture;
		private Texture2D circleTexture;

		private Vector2 middlePos;
		private Vector2 leftCirclePos;
		private Vector2 rightCirclePos;

		private Vector2 middleScale;
		private float circleScale;

		public Rectangle Rect
		{
			set
			{
				Position = value.Location.ToVector2();
				Size = value.Size.ToVector2();
			}
		}

		public Vector2 Position
		{
			get { return position; }
			set
			{
				position = value;

				leftCirclePos = position;

				middlePos = position + offset * circleScale;

				rightCirclePos = middlePos + new Vector2(middleScale.X, 0);
			}
		}

		public Vector2 Size
		{
			get { return size; }
			set
			{
				size = value;

				circleScale = size.Y / 64;

				middlePos = position + offset * circleScale;
				middleScale = size - offset * circleScale * 2;

				rightCirclePos = middlePos + new Vector2(middleScale.X, 0);
			}
		}

		public float Width
		{
			get { return size.X; }
			set
			{
				size.X = value;

				if (size.X < size.Y) size.X = size.Y;

				middleScale = size - offset * circleScale * 2;

				rightCirclePos = middlePos + new Vector2(middleScale.X, 0);
			}
		}

		public Color Color { get { return color; } set { color = value; } }
		public float LayerDepth { get { return layerDepth; } set { layerDepth = value; } }

		public Bar()
		{
			rectTexture = Atlas.Rect;
			circleTexture = Atlas.Circle;
			color = Color.White;
		}

		public void Draw()
		{
			Globals.spriteBatch.Draw(rectTexture, middlePos, null, color, 0, Vector2.Zero, middleScale, SpriteEffects.None, layerDepth);
			Globals.spriteBatch.Draw(circleTexture, leftCirclePos, leftCircleSourceRect, color, 0, Vector2.Zero, circleScale, SpriteEffects.None, layerDepth);
			Globals.spriteBatch.Draw(circleTexture, rightCirclePos, rightCircleSourceRect, color, 0, Vector2.Zero, circleScale, SpriteEffects.None, layerDepth);
		}
	}
}
