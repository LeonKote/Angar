using Angar.Entities.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities
{
	public abstract class Entity
	{
		private Vector2 position;
		protected Vector2 movement;
		protected float friction;
		protected bool isDestroying;

		protected HashSet<Component> components = new HashSet<Component>();
		protected Body body;

		public Vector2 Position { get { return position; } set { position = value; } }

		public Entity()
		{
			body = new Body(this);
			components.Add(body);
		}

		public virtual void Update()
		{
			position += movement;
			movement *= friction;

			if (isDestroying)
			{
				foreach (Component component in components)
				{
					if (component.Color.A > 0)
					{
						float t = (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
						component.Color *= t * 50;
						component.Scale *= t + 1;
					}
					else World.Main.RemoveEntity(this);
				}
			}
		}

		public void Draw()
		{
			foreach (Component component in components)
			{
				component.Draw();
			}
		}

		public void AddForce(Vector2 vec)
		{
			movement += vec;
		}

		public void Destory()
		{
			isDestroying = true;
		}
	}
}
