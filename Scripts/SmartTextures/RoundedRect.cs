﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.SmartTextures
{
	public class RoundedRect
	{
		private static readonly Rectangle topLeftCicleSourceRect = new Rectangle(0, 0, 64, 64);
		private static readonly Rectangle topRightCicleSourceRect = new Rectangle(64, 0, 64, 64);
		private static readonly Rectangle bottomLeftCicleSourceRect = new Rectangle(0, 64, 64, 64);
		private static readonly Rectangle bottomRightCicleSourceRect = new Rectangle(64, 64, 64, 64);

		private Texture2D rectTexture;
		private Texture2D circleTexture;

		private Rectangle topLeftCircle;
		private Rectangle topRect;
		private Rectangle topRightCircle;
		private Rectangle centerRect;
		private Rectangle bottomLeftCircle;
		private Rectangle bottomRect;
		private Rectangle bottomRightCircle;
		private int radius = 10;

		private Rectangle rect;
		private Color color;

		public Rectangle Rect
		{
			get { return rect; }
			set
			{
				rect = value;

				Point circleSize = new Point(radius / 2);

				topLeftCircle = new Rectangle(rect.Location, circleSize);
				topRect = new Rectangle(topLeftCircle.Location + new Point(topLeftCircle.Width, 0), new Point(rect.Width - radius, circleSize.Y));
				topRightCircle = new Rectangle(topRect.Location + new Point(topRect.Width, 0), circleSize);
				centerRect = new Rectangle(topLeftCircle.Location + new Point(0, topLeftCircle.Height), new Point(rect.Width, rect.Height - radius));
				bottomLeftCircle = new Rectangle(centerRect.Location + new Point(0, centerRect.Height), circleSize);
				bottomRect = new Rectangle(bottomLeftCircle.Location + new Point(bottomLeftCircle.Width, 0), new Point(rect.Width - radius, circleSize.Y));
				bottomRightCircle = new Rectangle(bottomRect.Location + new Point(bottomRect.Width, 0), circleSize);
			}
		}

		public int Radius { get { return radius; } set { radius = value; } }
		public Color Color { get { return color; } set { color = value; } }

		public RoundedRect()
		{
			rectTexture = Resources.Rect;
			circleTexture = Resources.Circle;
			color = Color.White;
		}

		public void Draw()
		{
			Globals.SpriteBatch.Draw(circleTexture, topLeftCircle, topLeftCicleSourceRect, color);
			Globals.SpriteBatch.Draw(rectTexture, topRect, color);
			Globals.SpriteBatch.Draw(circleTexture, topRightCircle, topRightCicleSourceRect, color);
			Globals.SpriteBatch.Draw(rectTexture, centerRect, color);
			Globals.SpriteBatch.Draw(circleTexture, bottomLeftCircle, bottomLeftCicleSourceRect, color);
			Globals.SpriteBatch.Draw(rectTexture, bottomRect, color);
			Globals.SpriteBatch.Draw(circleTexture, bottomRightCircle, bottomRightCicleSourceRect, color);
		}
	}
}
