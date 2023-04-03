using Angar.Entities.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities
{
	public class Robot : Entity
	{
		protected Gun gun;

		public float Rotation { get { return gun.Rotation; } set { gun.Rotation = value; } }

		public Robot()
		{
			friction = 0.95f;
			gun = new Gun(this);
			components.Add(gun);
		}

		public void Shoot(Vector2 vec)
		{
			gun.Shoot(vec);
		}
	}
}
