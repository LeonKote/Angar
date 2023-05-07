using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar
{
	public static class Utils
	{
		private static Random rand = new Random();

		public static float RandomSingle(float min, float max)
		{
			return rand.NextSingle() * (max - min) + min;
		}

		public static Color LerpColor(Color value1, Color value2, int alpha, float amount)
		{
			return new Color((int)MathHelper.Lerp(value1.R, value2.R, amount), (int)MathHelper.Lerp(value1.G, value2.G, amount), (int)MathHelper.Lerp(value1.B, value2.B, amount), alpha);
		}

		public static Color SetAlpha(Color value, byte alpha)
		{
			return new Color(value.R, value.G, value.B, alpha);
		}

		public static float LerpAngle(float value1, float value2, float amount)
		{
			return MathHelper.WrapAngle(value1 + MathHelper.WrapAngle(value2 - value1) * amount);
		}

		public static Vector2 RandomRect()
		{
			Vector2 vec;

			switch (rand.Next(4))
			{
				case 0:
					vec = new Vector2(RandomSingle(-384, 384), RandomSingle(-192, 0) - 192);
					break;
				case 1:
					vec = new Vector2(RandomSingle(-384, 384), RandomSingle(0, 192) + 192);
					break;
				case 2:
					vec = new Vector2(RandomSingle(-192, 0) - 192, RandomSingle(-384, 384));
					break;
				default:
					vec = new Vector2(RandomSingle(0, 192) + 192, RandomSingle(-384, 384));
					break;
			}

			return vec;
		}

		public static Texture2D GetOutlineTexture(Texture2D texture, float scale)
		{
			return GetOutlineTexture(texture, scale, Vector2.Zero);
		}

		public static Texture2D GetOutlineTexture(Texture2D texture, float scale, Vector2 offset)
		{
			Vector2 size = new Vector2(texture.Width, texture.Height) * 0.5f;
			RenderTarget2D renderTarget = new RenderTarget2D(Globals.GraphicsDevice, texture.Width, texture.Height);
			Globals.GraphicsDevice.SetRenderTarget(renderTarget);
			Globals.GraphicsDevice.Clear(Color.Transparent);
			Globals.SpriteBatch.Begin();
			Globals.SpriteBatch.Draw(texture, size, null, SetAlpha(Color.White * 0.75f, 255), 0, size, 1, SpriteEffects.None, 0);
			Globals.SpriteBatch.Draw(texture, size + offset * (1 / scale), null, Color.White, 0, size, 1 - 0.075f / scale, SpriteEffects.None, 0);
			Globals.SpriteBatch.End();
			Globals.GraphicsDevice.SetRenderTarget(null);
			return renderTarget;
		}
	}
}
