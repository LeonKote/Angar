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
		private float nativeSize;
		private float size;

		public Texture2D Texture
		{
			get { return texture; }
			set
			{
				texture = value;
				nativeSize = texture.Width * 0.5f;
			}
		}

		public override float Scale
		{
			get { return base.Scale; }
			set
			{
				base.Scale = value;
				size = nativeSize * value;
			}
		}

		public float Size { get { return size; } }
		public float NativeSize { get { return nativeSize; } set { nativeSize = value; } }

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
