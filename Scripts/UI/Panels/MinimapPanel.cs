using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.UI
{
	// Minimap made in a hurry)
	// For 1920x1080 screen resolution
	public class MinimapPanel : UIElement
	{
		private Vector2 pos;
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

		public void SetPos(Vector2 vec)
		{
			pos = vec / 50 + new Vector2(1800, 895);
		}

		public void SetRot(float rot)
		{
			this.rot = rot;
		}
	}
}
