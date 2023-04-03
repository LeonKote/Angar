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
		private double deathTime;

		public Projectile(float lifetime)
		{
			friction = 1.0f;
			deathTime = Globals.gameTime.TotalGameTime.TotalSeconds + lifetime;
			body.Texture = Atlas.Projecttile;
			body.Origin = new Vector2(32, 32);
			body.Scale = 0.5f;
		}

		public override void Update()
		{
			base.Update();

			if (!isDestroying && Globals.gameTime.TotalGameTime.TotalSeconds > deathTime)
			{
				Destory();
			}
		}
	}
}
