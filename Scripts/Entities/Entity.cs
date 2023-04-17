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

		protected int maxHealth;
		protected int health;
		protected int bodyDamage;
		private float nextCollision;

		protected Anim hurtAnim;
		protected Anim destroyAnim;

		protected HashSet<Component> components = new HashSet<Component>();
		protected Body body;

		public Vector2 Position { get { return position; } set { position = value; } }
		public Vector2 Movement { get { return movement; } }
		public Color Color { get { return body.NativeColor; } set { body.NativeColor = value; } }
		public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
		public int Health { get { return health; } set { health = value; } }

		protected ClosestEntitiesHandler closestEntities = new ClosestEntitiesHandler();

		public Entity()
		{
			body = new Body(this);
			components.Add(body);

			hurtAnim = new Anim();
			hurtAnim.Duration = 0.25f;
			hurtAnim.OnPlaying += OnHurtAnim;

			destroyAnim = new Anim();
			destroyAnim.Duration = 0.25f;
			destroyAnim.OnPlaying += OnDestroyAnim;
			destroyAnim.OnEnd += () =>
			{
				World.Instance.RemoveEntity(this);
			};
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

			hurtAnim.Update();
			destroyAnim.Update();
		}

		private void UpdateCollisions()
		{
			if (destroyAnim.IsPlaying || Globals.time < this.nextCollision) return;

			closestEntities.Clear();

			foreach (Entity entity in World.Instance.Entities)
			{
				if (entity == this || entity.destroyAnim.IsPlaying) continue;

				float dist = Vector2.DistanceSquared(entity.position, this.position);
				float size = entity.body.realSize + this.body.realSize;

				closestEntities.HandleEntity(entity, dist);

				if (dist < size * size)
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
			if (this is Projectile && entity is Projectile)
			{
				if ((this as Projectile).Parent == (entity as Projectile).Parent) return;
			}

			entity.health -= bodyDamage;
			if (entity.health <= 0)
			{
				OnDestroyEntity(entity);
				entity.destroyAnim.Play();
			}

			if (this is not Projectile)
			{
				AddForce(vec * 2);
				hurtAnim.Play();
			}

			nextCollision = Globals.time + 0.5f;
		}

		protected virtual void OnDestroyEntity(Entity entity)
		{
			(this as Projectile)?.Parent.OnDestroyEntity(entity);
		}

		private void OnHurtAnim(float t)
		{
			foreach (Component component in components)
			{
				if (t < 0.25f)
					component.Color = Utils.SetAlpha(Color.White, component.Color.A);
				else
					component.Color = Utils.Lerp(Color.Red, component.NativeColor, component.Color.A, t);
			}
		}

		private void OnDestroyAnim(float t)
		{
			foreach (Component component in components)
			{
				component.Color = Utils.SetAlpha(component.Color, (byte)(255 - t * 255));
				component.Scale *= 1.01f;
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
