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

		public float Power;
		public float Delay;

		public Gun(Entity entity) : base(entity)
        {
            LayerDepth = 0.4f;
        }

        public void Shoot(Vector2 direction)
        {
            if (Globals.time < NextShot) return;

            Projectile projectile = new Projectile();
            projectile.Color = entity.Color;
            projectile.Parent = entity;
            projectile.LifeTime = 2f;
            projectile.Position = entity.Position + direction * 64;
            projectile.AddForce(direction * Power);
            World.Instance.AddEntity(projectile);

            NextShot = Globals.time + Delay;
        }
    }
}
