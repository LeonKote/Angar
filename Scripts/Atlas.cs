using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar
{
	public static class Atlas
	{
		public static Texture2D Body;
		public static Texture2D Gun;
		public static Texture2D Cell;
		public static Texture2D Polygon1;
		public static Texture2D Projecttile;
		public static Texture2D Rect;
		public static Texture2D Circle;
		public static SpriteFont Rubik;

		public static void Init(ContentManager content)
		{
			Body = content.Load<Texture2D>("Components/body");
			Gun = content.Load<Texture2D>("Components/gun");
			Cell = content.Load<Texture2D>("World/cell");
			Polygon1 = content.Load<Texture2D>("Components/polygon1");
			Projecttile = content.Load<Texture2D>("Components/projecttile");
			Rect = content.Load<Texture2D>("Primitives/rect");
			Circle = content.Load<Texture2D>("Primitives/circle");
			Rubik = content.Load<SpriteFont>("Fonts/Rubik-Medium");
		}
	}
}
