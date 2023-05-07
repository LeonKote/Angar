using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Components
{
	public class StandardGunSet : GunSet
	{
		StandardGun gun;

		public StandardGunSet(Entity entity) : base(entity)
		{
			shootingDelay = 0.5f;

			gun = new StandardGun(entity);
			gun.IdlePosition = new Vector2(-64, 32);
			gun.ShootPosition = new Vector2(-48, 32);
			guns.Add(gun);
		}
	}
}
