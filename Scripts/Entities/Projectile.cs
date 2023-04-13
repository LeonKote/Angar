using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities
{
	public class Projectile : Entity
	{
		private float deathTime;
		private Entity parent;

		public Entity Parent { get { return parent; } }

		public Projectile(Entity parent, float lifetime)
		{
			this.parent = parent;

			friction = 1.0f;

			maxHealth = 10;
			health = 10;
			bodyDamage = 25;

			deathTime = Globals.time + lifetime;

			body.Texture = Atlas.Projecttile;
			body.Origin = new Vector2(32, 32);
			body.Scale = 0.5f;
		}

		public override void Update()
		{
			base.Update();

			if (!isDestroying && Globals.time > deathTime)
			{
				isDestroying = true;
			}
		}
	}
}
