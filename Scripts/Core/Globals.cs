using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar
{
	public static class Globals
	{
		public static readonly Vector2 NativeResolution = new Vector2(1920, 1080);
		public static GraphicsDevice GraphicsDevice;
		public static SpriteBatch SpriteBatch;
		public static float DeltaTime;
		public static float Time;

		public static void SetGameTime(GameTime gameTime)
		{
			DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
			Time += DeltaTime;
		}
	}
}
