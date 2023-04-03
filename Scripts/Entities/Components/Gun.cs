using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Components
{
	public class Gun : Component
	{
		public float Power;

		public Gun(Entity entity) : base(entity)
		{
			Power = 5.0f;
			Texture = Atlas.Gun;
			Origin = new Vector2(0, 32);
			Scale = 0.5f;
			LayerDepth = 0.9f;
		}

		public void Shoot(Vector2 direction)
		{
			Projectile projectile = new Projectile(2);
			projectile.Position = entity.Position + direction * 64;
			projectile.AddForce(direction * Power);
			World.Main.AddEntity(projectile);
		}
	}
}
