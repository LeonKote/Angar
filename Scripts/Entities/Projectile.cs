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

		public Entity Parent { get; set; }

		public Projectile()
		{
			friction = 1.0f;
			deathTime = Globals.time + 3;

			body.Texture = Utils.GetOutlineTexture(Atlas.Circle, 0.25f);
			body.Origin = new Vector2(64, 64);
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
