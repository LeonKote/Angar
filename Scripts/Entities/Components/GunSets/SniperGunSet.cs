using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Components
{
	public class SniperGunSet : GunSet
	{
		private StandardGun gun;

		public SniperGunSet(Entity entity) : base(entity)
		{
			shootingDelay = 0.75f;

			gun = new StandardGun(entity);
			gun.SourceRect = new Rectangle(48, 0, 80, 64);
			gun.IdlePosition = new Vector2(-64, 32);
			gun.ShootPosition = new Vector2(-48, 32);
			gun.BulletDamage = 1.5f;
			gun.BulletSpeed = 1.5f;
			gun.Knockback = 1.5f;
			guns.Add(gun);
		}
	}
}
