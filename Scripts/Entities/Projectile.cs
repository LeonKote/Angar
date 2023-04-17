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

		public Entity Parent { get { return parent; } set { parent = value; } }
		public float LifeTime { set { deathTime = Globals.time + value; } }

		public Projectile()
		{
			friction = 1.0f;

			maxHealth = 10;
			health = 10;
			bodyDamage = 25;

			body.Texture = Atlas.Projecttile;
			body.Origin = new Vector2(32, 32);
			body.Scale = 0.5f;
		}

		public override void Update()
		{
			base.Update();

			if (!destroyAnim.IsPlaying && Globals.time > deathTime)
			{
				destroyAnim.Play();
			}
		}
	}
}
