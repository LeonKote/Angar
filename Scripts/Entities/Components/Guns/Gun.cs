using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Components.Guns
{
	public class Gun : Component
	{
		private float NextShot;

		public Gun(Entity entity) : base(entity)
		{
			LayerDepth = 0.4f;
		}

		public void Shoot(Vector2 direction)
		{
			if (Globals.time < NextShot) return;

			Projectile projectile = new Projectile();
			projectile.Health = entity.Attributes.BulletPenetration;
			projectile.Attributes.MaxHealth = entity.Attributes.BulletPenetration;
			projectile.Attributes.BodyDamage = entity.Attributes.BulletDamage;
			projectile.Color = entity.Color;
			projectile.Parent = entity;
			projectile.LifeTime = 2f;
			projectile.Position = entity.Position + direction * 64;
			projectile.AddForce(direction * entity.Attributes.BulletSpeed);
			World.Instance.AddEntity(projectile);

			NextShot = Globals.time + 0.5f - entity.Attributes.Reload * 0.05f;
		}
	}
}
