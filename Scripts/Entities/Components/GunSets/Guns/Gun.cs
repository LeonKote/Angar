using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Components
{
	public abstract class Gun : Component
	{
		private Vector2 idlePosition;
		private Anim anim;

		public Texture2D Texture { get; set; }
		public Rectangle SourceRect { get; set; }
		public float Angle { get; set; }
		public Vector2 ShootPosition { get; set; }
		public float BulletSpeed { get; set; } = 1.0f;
		public float Knockback { get; set; } = 0.5f;

		public Vector2 IdlePosition
		{
			get { return idlePosition; }
			set
			{
				idlePosition = value;
				Origin = value;
			}
		}

		public Gun(Entity entity) : base(entity)
		{
			NativeColor = new Color(153, 153, 153);
			LayerDepth = 0.4f;

			anim = new Anim();
			anim.Duration = 0.5f;
			anim.Playing += OnShootAnim;
		}

		private void OnShootAnim(float t)
		{
			if (t < 0.1f)
			{
				Origin = Vector2.Lerp(IdlePosition, ShootPosition, t * 10);
			}
			else
			{
				Origin = Vector2.Lerp(ShootPosition, IdlePosition, t);
			}
		}

		public override void Update()
		{
			anim.Update();
		}

		public void Shoot(Vector2 vec)
		{
			Projectile projectile = new Projectile();
			projectile.Health = entity.Attributes.BulletPenetration;
			projectile.Attributes.MaxHealth = entity.Attributes.BulletPenetration;
			projectile.Attributes.BodyDamage = entity.Attributes.BulletDamage;
			if (Angle == 0)
			{
				projectile.Position = entity.Position + vec * entity.Scale * 60;
			}
			else
			{
				(float sin, float cos) = MathF.SinCos(Rotation + Angle);
				projectile.Position = entity.Position + new Vector2(cos, sin) * entity.Scale * 60;
			}
			projectile.Color = entity.Color;
			projectile.Scale = entity.Scale * 0.25f;
			projectile.Parent = entity;
			projectile.AddForce(vec * BulletSpeed * entity.Attributes.BulletSpeed);
			World.Instance.AddEntity(projectile);
			entity.AddForce(-vec * Knockback);
			anim.Play();
		}
	}
}
