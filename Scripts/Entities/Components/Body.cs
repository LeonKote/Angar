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
		private float realSize;

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

		public float RealSize { get { return realSize; } }

		public Body(Entity entity) : base(entity)
		{
			LayerDepth = 0.5f;
		}

		public override void Draw()
		{
			Globals.spriteBatch.Draw(Texture, entity.Position, null, Color, Rotation, Origin, Scale, SpriteEffects.None, LayerDepth);
		}
	}
}
