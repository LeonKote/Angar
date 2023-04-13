using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar
{
	public class Utils
	{
		private static Random rand = new Random();

		public static float RandomSingle(float min, float max)
		{
			return rand.NextSingle() * (max - min) + min;
		}

		public static Color Lerp(Color value1, Color value2, int alpha, float amount)
		{
			return new Color((int)MathHelper.Lerp(value1.R, value2.R, amount), (int)MathHelper.Lerp(value1.G, value2.G, amount), (int)MathHelper.Lerp(value1.B, value2.B, amount), alpha);
		}

		public static Color SetAlpha(Color value, byte alpha)
		{
			return new Color(value.R, value.G, value.B, alpha);
		}
	}
}
