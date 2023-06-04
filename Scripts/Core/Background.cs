using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar
{
	public class Background
	{
		private Camera camera;

		private Vector2 startVec;
		private Vector2 size;

		public Vector2 Size { get { return size; } set { size = value; } }

		public Background(Camera camera)
		{
			this.camera = camera;

			size = Globals.NativeResolution + new Vector2(32, 32);
		}

		public void Update()
		{
			startVec = camera.ScreenToWorldPoint(Vector2.Zero);
			startVec = new Vector2(MathF.Floor(startVec.X / 32) * 32, MathF.Floor(startVec.Y / 32) * 32);
		}

		public void Draw()
		{
			Globals.SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.TransformMatrix);
			for (int x = 0; x < size.X; x += 32)
			{
				for (int y = 0; y < size.Y; y += 32)
				{
					Globals.SpriteBatch.Draw(Resources.Cell, startVec + new Vector2(x, y), Color.White);
				}
			}
			Globals.SpriteBatch.End();
		}
	}
}
