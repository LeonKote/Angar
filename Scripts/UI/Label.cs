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

		private float rotation;
		private string text;
		private SpriteFont font;
		private Vector2 textOrigin;

		public float Rotation { get { return rotation; } set { rotation = value; } }

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
			Globals.SpriteBatch.DrawString(font, text, position + Vector2.One, shadowColor, rotation, textOrigin, scale, SpriteEffects.None, 0);
			Globals.SpriteBatch.DrawString(font, text, position, Color.White, rotation, textOrigin, scale, SpriteEffects.None, 0);
			base.Draw();
		}
	}
}
