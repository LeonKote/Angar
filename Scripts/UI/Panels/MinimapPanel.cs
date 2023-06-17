using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.UI
{
	public class MinimapPanel : UIElement
	{
		private Vector2 pos;
		private Vector2 posOffset;
		private float sizeOffset;
		private float rot;

		public MinimapPanel()
		{

		}

		public override void Draw()
		{
			Globals.SpriteBatch.Draw(Resources.Minimap, rect, Color.White * 0.75f);
			Globals.SpriteBatch.Draw(Resources.MinimapPlayer, pos, null, Color.White, rot, new Vector2(4, 6), 0.75f, SpriteEffects.None, 0);
			base.Draw();
		}

		protected override void ApplyTransform()
		{
			base.ApplyTransform();
			posOffset = rect.Location.ToVector2() + rect.Size.ToVector2() * 0.5f;
			sizeOffset = 2200 / (float)rect.Size.X;
		}

		public void SetPos(Vector2 vec)
		{
			pos = vec / sizeOffset + posOffset;
		}

		public void SetRot(float rot)
		{
			this.rot = rot;
		}
	}
}
