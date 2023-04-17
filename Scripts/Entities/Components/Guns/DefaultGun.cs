using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Components.Guns
{
	public class DefaultGun : Gun
	{
		private static readonly Rectangle sourceRect = new Rectangle(32, 0, 96, 64);

		public Texture2D Texture;

		public DefaultGun(Entity entity) : base(entity)
		{
			Power = 5.0f;
			Delay = 0.5f;

			Texture = Atlas.Gun;
			NativeColor = new Color(153, 153, 153);
			Origin = new Vector2(-32, 32);
			Scale = 0.5f;
		}

		public override void Draw()
		{
			Globals.spriteBatch.Draw(Texture, entity.Position, sourceRect, Color, Rotation, Origin, Scale, SpriteEffects.None, LayerDepth);
		}
	}
}
