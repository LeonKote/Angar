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
		protected HealthBar healthBar;

		private ScoreHandler score;

		public float Rotation { get { return gun.Rotation; } set { gun.Rotation = value; } }

		public event Action<ScoreHandler> ScoreChanged;

		public Robot()
		{
			friction = 0.95f;

			maxHealth = 100;
			health = 100;
			bodyDamage = 25;

			gun = new Gun(this);
			components.Add(gun);

			healthBar = new HealthBar(this);
			components.Add(healthBar);

			score = new ScoreHandler();
		}

		protected override void OnDestroyEntity(Entity entity)
		{
			if (entity is Polygon)
			{
				score.Exp += ((Polygon)entity).Score;
				ScoreChanged.Invoke(score);
			}
		}

		public void Shoot(Vector2 vec)
		{
			gun.Shoot(vec);
		}
	}
}
