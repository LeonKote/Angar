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
		private float scale = 1.0f;
		private float health;
		private float nextCollision;

		protected Timer regenTimer;
		protected Anim hurtAnim;
		protected Anim destroyAnim;

		protected HashSet<Component> components = new HashSet<Component>();
		protected Body body;
		protected HealthBar healthBar;

		protected AttributesHandler attributes;

		public event Action<Entity> Destroyed;

		public Vector2 Position { get { return position; } set { position = value; } }
		public Vector2 Movement { get { return movement; } }
		public Color Color { get { return body.NativeColor; } set { body.NativeColor = value; } }
		public float Health { get { return health; } set { health = value; } }
		public AttributesHandler Attributes { get { return attributes; } set { attributes = value; } }

		public float Scale
		{
			get { return scale; }
			set
			{
				float diff = value / scale;
				scale = value;
				foreach (Component component in components)
				{
					component.SetScale(component.Scale * diff);
				}
			}
		}

		public Entity()
		{
			attributes = new AttributesHandler(this);
			SetAttributes();

			body = new Body(this);
			components.Add(body);

			regenTimer = new Timer();
			regenTimer.Duration = 3;
			regenTimer.WhileEnded += UpdateRegen;

			hurtAnim = new Anim();
			hurtAnim.Duration = 0.25f;
			hurtAnim.Playing += OnHurtAnim;

			destroyAnim = new Anim();
			destroyAnim.Duration = 0.25f;
			destroyAnim.Playing += OnDestroyAnim;
			destroyAnim.Ended += () =>
			{
				World.Instance.RemoveEntity(this);
			};

			Destroyed += (Entity destroyer) =>
			{
				if (destroyer is Tank tank)
				{
					tank.OnDestroyEntity(this);
				}
				else if (destroyer is Projectile projectile)
				{
					(projectile.Parent as Tank)?.OnDestroyEntity(this);
				}
			};
		}

		protected virtual void SetAttributes()
		{

		}

		public virtual void Update()
		{
			position += movement;
			movement *= friction;

			if (this is not Projectile)
				Utils.ClampVector(ref position);

			foreach (Component component in components)
			{
				component.Update();
			}

			UpdateCollisions();

			regenTimer.Update();
			hurtAnim.Update();
			destroyAnim.Update();
		}

		private void UpdateCollisions()
		{
			if (destroyAnim.IsPlaying || Globals.Time < this.nextCollision) return;

			foreach (Entity entity in World.Instance.Entities)
			{
				if (entity == this || entity.destroyAnim.IsPlaying) continue;

				float dist = Vector2.DistanceSquared(entity.position, this.position);
				float size = entity.body.Size + this.body.Size;

				if (dist < size * size)
				{
					Vector2 vec = entity.position - this.position;
					if (vec.LengthSquared() > 0)
						vec.Normalize();

					entity.OnCollision(this, vec);
					this.OnCollision(entity, -vec);
					return;
				}
			}
		}

		private void UpdateRegen()
		{
			if (health == attributes.MaxHealth) return;

			float regen = (attributes.HealthRegen + 1) * 0.1f;
			if (health + regen < attributes.MaxHealth)
				health += regen;
			else health = attributes.MaxHealth;

			healthBar?.UpdateHealth(true);
		}

		protected virtual void OnCollision(Entity entity, Vector2 vec)
		{
			if (this is Projectile projecttile1 && entity is Projectile projecttile2
				&& projecttile1.Parent == projecttile2.Parent) return;

			entity.Health -= this.attributes.BodyDamage;
			entity.regenTimer.Start();
			entity.healthBar?.UpdateHealth();

			if (entity.Health <= 0)
			{
				entity.Destroyed?.Invoke(this);
				entity.destroyAnim.Play();
			}

			if (this is not Projectile)
			{
				this.AddForce(vec * 2);
				this.hurtAnim.Play();
			}

			this.nextCollision = Globals.Time + 0.5f;
		}

		private void OnHurtAnim(float t)
		{
			foreach (Component component in components)
			{
				if (t < 0.25f)
					component.Color = Utils.SetAlpha(Color.White, component.Color.A);
				else
					component.Color = Utils.LerpColor(Color.Red, component.NativeColor, component.Color.A, t);
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
