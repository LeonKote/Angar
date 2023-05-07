using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Components
{
	public abstract class GunSet : Component
	{
		protected float shootingDelay;
		private float nextShot;

		protected HashSet<Gun> guns = new HashSet<Gun>();

		public override Color Color
		{
			get { return base.Color; }
			set
			{
				base.Color = value;
				foreach (Gun gun in guns)
				{
					gun.Color = base.Color;
				}
			}
		}

		public override float Rotation
		{
			get { return base.Rotation; }
			set
			{
				base.Rotation = value;
				foreach (Gun gun in guns)
				{
					gun.Rotation = value;
				}
			}
		}

		public override float Scale
		{
			get { return base.Scale; }
			set
			{
				float diff = value / base.Scale;
				base.Scale = value;
				foreach (Gun gun in guns)
				{
					gun.Scale *= diff;
				}
			}
		}

		public GunSet(Entity entity) : base(entity)
		{
			NativeColor = new Color(153, 153, 153);
		}

		public override void Update()
		{
			foreach (Gun gun in guns)
			{
				gun.Update();
			}
		}

		public override void Draw()
		{
			foreach (Gun gun in guns)
			{
				gun.Draw();
			}
		}

		public void ShootDelay(Vector2 vec)
		{
			if (Globals.time < nextShot) return;
			Shoot(vec);
			nextShot = Globals.time + shootingDelay - entity.Attributes.Reload * 0.01f;
		}

		protected virtual void Shoot(Vector2 vec)
		{
			foreach (Gun gun in guns)
			{
				gun.Shoot(vec);
			}
		}
	}
}
