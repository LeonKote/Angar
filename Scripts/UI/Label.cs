using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.UI
{
	public class Label : UIElement
	{
		private static readonly Color shadowColor = new Color(85, 85, 85);

		private string text;
		private SpriteFont font;
		private Vector2 textOrigin;

		public string Text
		{
			get { return text; }
			set
			{
				text = value;
				textOrigin = font.MeasureString(text) * origin;
			}
		}

		public SpriteFont Font { get { return font; } set { font = value; } }

		public override void Draw()
		{
			Globals.spriteBatch.DrawString(font, text, position + Vector2.One, shadowColor, 0, textOrigin, scale, SpriteEffects.None, 0);
			Globals.spriteBatch.DrawString(font, text, position, Color.White, 0, textOrigin, scale, SpriteEffects.None, 0);
			base.Draw();
		}
	}
}
