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
		public static SpriteBatch spriteBatch;
		public static float deltaTime;
		public static float time;

		public static void SetGameTime(GameTime gameTime)
		{
			deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
			time = (float)gameTime.TotalGameTime.TotalSeconds;
		}
	}
}
