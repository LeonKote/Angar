using Microsoft.Xna.Framework;
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
		private Color nativeColor = Color.White;
		protected float scale = 1.0f;

		public virtual Color Color { get; set; } = Color.White;
		public virtual float Rotation { get; set; }
		public Vector2 Origin { get; set; }
		public float LayerDepth { get; set; }

		public virtual float Scale { get { return scale; } set { scale = value; } }

		public Color NativeColor
		{
			get { return nativeColor; }
			set
			{
				nativeColor = value;
				Color = value;
			}
		}

		public Component(Entity entity)
		{
			this.entity = entity;
		}

		public virtual void Update()
		{

		}

		public virtual void Draw()
		{

		}

		public virtual void SetScale(float scale)
		{
			Scale = scale;
		}
	}
}
