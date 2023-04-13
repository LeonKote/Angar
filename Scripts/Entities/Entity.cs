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

		protected int maxHealth;
		protected int health;
		protected int bodyDamage;
		private float nextCollision;

		private bool isHurt;
		private float hurtTime;

		protected HashSet<Component> components = new HashSet<Component>();
		protected Body body;

		public Vector2 Position { get { return position; } set { position = value; } }
		public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
		public int Health { get { return health; } set { health = value; } }

		public Entity()
		{
			body = new Body(this);
			components.Add(body);
		}

		public virtual void Update()
		{
			position += movement;
			movement *= friction;

			foreach (Component component in components)
			{
				component.Update();
			}

			UpdateCollisions();
			UpdateHurt();
			UpdateDestroying();
		}

		private void UpdateCollisions()
		{
			if (this.isDestroying || Globals.time < this.nextCollision) return;

			foreach (Entity entity in World.Instance.Entities)
			{
				if (entity == this || entity.isDestroying) continue;

				float dist = entity.body.realSize + this.body.realSize;
				if (Vector2.DistanceSquared(entity.position, this.position) < dist * dist)
				{
					Vector2 vec = entity.position - this.position;
					vec.Normalize();

					entity.OnCollision(this, vec);
					this.OnCollision(entity, -vec);
					return;
				}
			}
		}

		protected virtual void OnCollision(Entity entity, Vector2 vec)
		{
			entity.health -= bodyDamage;
			if (entity.health <= 0)
			{
				entity.isDestroying = true;
				OnDestroyEntity(entity);
			}

			if (this is not Projectile)
			{
				AddForce(vec * 2);
				isHurt = true;
			}

			nextCollision = Globals.time + 0.5f;
		}

		protected virtual void OnDestroyEntity(Entity entity)
		{
			(this as Projectile)?.Parent.OnDestroyEntity(entity);
		}

		private void UpdateHurt()
		{
			if (!isHurt) return;

			if (hurtTime < 0.25f)
			{
				float t = hurtTime / 0.25f;
				foreach (Component component in components)
				{
					if (t < 0.25f)
						component.Color = Utils.SetAlpha(Color.White, component.Color.A);
					else
						component.Color = Utils.Lerp(Color.Red, component.NativeColor, component.Color.A, t);
				}
				hurtTime += Globals.deltaTime;
			}
			else
			{
				foreach (Component component in components)
				{
					component.Color = Utils.SetAlpha(component.NativeColor, component.Color.A);
				}
				hurtTime = 0;
				isHurt = false;
			}
		}

		private void UpdateDestroying()
		{
			if (!isDestroying) return;

			foreach (Component component in components)
			{
				if (component.Color.A > 0)
				{
					float t = Globals.deltaTime;
					component.Color = Utils.SetAlpha(component.Color, (byte)(component.Color.A * t * 50));
					component.Scale *= t + 1;
				}
				else World.Instance.RemoveEntity(this);
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
	}
}
