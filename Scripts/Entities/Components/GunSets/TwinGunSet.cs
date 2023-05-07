using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Components
{
	public class TwinGunSet : GunSet
	{
		private StandardGun firstGun;
		private StandardGun secondGun;

		private bool next;

		public TwinGunSet(Entity entity) : base(entity)
		{
			shootingDelay = 0.25f;

			firstGun = new StandardGun(entity);
			firstGun.Angle = -MathF.PI / 15;
			firstGun.IdlePosition = new Vector2(-32, 66);
			firstGun.ShootPosition = new Vector2(-16, 66);
			firstGun.SourceRect = new Rectangle(32, 0, 96, 64);
			guns.Add(firstGun);

			secondGun = new StandardGun(entity);
			secondGun.Angle = MathF.PI / 15;
			secondGun.IdlePosition = new Vector2(-32, -2);
			secondGun.ShootPosition = new Vector2(-16, -2);
			secondGun.SourceRect = new Rectangle(32, 0, 96, 64);
			guns.Add(secondGun);
		}

		protected override void Shoot(Vector2 vec)
		{
			if (next) firstGun.Shoot(vec);
			else secondGun.Shoot(vec);
			next = !next;
		}
	}
}
