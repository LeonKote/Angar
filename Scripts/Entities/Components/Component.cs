using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Components
{
	public abstract class Component
	{
		protected Entity entity;

		public Texture2D Texture;
		public float Rotation;
		public Color Color;
		public Vector2 Origin;
		public float Scale;
		public float LayerDepth;

		public Component(Entity entity)
		{
			this.entity = entity;
			Color = Color.White;
		}

		public virtual void Draw()
		{
			Globals.spriteBatch.Draw(Texture, entity.Position, null, Color, Rotation, Origin, Scale, SpriteEffects.None, LayerDepth);
		}
	}
}
