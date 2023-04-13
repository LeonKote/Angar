using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Components
{
	public class Body : Component
	{
		private Texture2D texture;
		public float realSize;

		public Texture2D Texture
		{
			get { return texture; }
			set
			{
				texture = value;
				realSize = texture.Width * 0.5f;
			}
		}

		public override float Scale
		{
			get { return base.Scale; }
			set
			{
				base.Scale = value;
				realSize = Texture.Width * 0.5f * value;
			}
		}

		public Body(Entity entity) : base(entity)
		{
			Texture = Atlas.Body;
			NativeColor = new Color(0, 178, 225);
			Origin = new Vector2(64, 64);
			Scale = 0.5f;
			LayerDepth = 0.5f;
		}

		public override void Draw()
		{
			Globals.spriteBatch.Draw(Texture, entity.Position, null, Color, Rotation, Origin, Scale, SpriteEffects.None, LayerDepth);
		}
	}
}
