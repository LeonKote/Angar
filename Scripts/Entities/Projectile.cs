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

			body.Texture = Utils.GetOutlineTexture(Atlas.Circle, 0.25f);
			body.Origin = new Vector2(64, 64);
			body.Scale = 0.25f;
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
