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
		private static Random rand = new Random();

		public static SpriteBatch spriteBatch;
		public static GameTime gameTime;

		public static float RandomSingle(float min, float max)
		{
			return rand.NextSingle() * (max - min) + min;
		}
	}
}
