using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar
{
	public static class Resources
	{
		public static Texture2D Cell;
		public static SpriteFont Rubik;
		public static Texture2D BarAdd;
		public static Texture2D Rect;
		public static Texture2D Circle;
		public static Texture2D RoundedRect;
		public static Texture2D RoundedRect2;
		public static Texture2D Triangle;
		public static Texture2D Pentagon;
		public static Texture2D Polygon1;
		public static Texture2D Polygon2;
		public static Texture2D Polygon3;
		public static Texture2D Minimap;
		public static Texture2D MinimapPlayer;

		public static void Init(ContentManager content)
		{
			Cell = content.Load<Texture2D>("World/cell");
			Rubik = content.Load<SpriteFont>("Fonts/Rubik-Bold");
			BarAdd = content.Load<Texture2D>("UI/barAdd");
			Rect = content.Load<Texture2D>("Primitives/rect");
			Circle = content.Load<Texture2D>("Primitives/circle");
			RoundedRect = content.Load<Texture2D>("Primitives/roundedRect");
			RoundedRect2 = content.Load<Texture2D>("Primitives/roundedRect2");
			Triangle = content.Load<Texture2D>("Primitives/triangle");
			Pentagon = content.Load<Texture2D>("Primitives/pentagon");
			Minimap = content.Load<Texture2D>("UI/minimap");
			MinimapPlayer = content.Load<Texture2D>("UI/minimapPlayer");

			Polygon1 = Utils.GetOutlineTexture(RoundedRect, 0.4f);
			Polygon2 = Utils.GetOutlineTexture(Triangle, 0.3f, new Vector2(0, 1));
			Polygon3 = Utils.GetOutlineTexture(Pentagon, 0.6f, new Vector2(0, 0.5f));
		}
	}
}
