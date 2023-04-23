using Angar.Entities.Components;
using Angar.Entities.Components.Guns;
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

		private ScoreHandler score = new ScoreHandler();

		public float Rotation { get { return gun.Rotation; } set { gun.Rotation = value; } }

		public event Action<ScoreHandler> ScoreChanged;

		public Robot()
		{
			friction = 0.95f;

			body.Texture = Utils.GetOutlineTexture(Atlas.Circle, 0.5f);
			body.Origin = new Vector2(64, 64);
			body.Scale = 0.5f;

			gun = new DefaultGun(this);
			components.Add(gun);

			healthBar = new HealthBar(this);
			components.Add(healthBar);
		}

		protected override void SetAttributes()
		{
			Health = 100;
			attributes.MaxHealth = 100;
			attributes.BodyDamage = 25;
			attributes.BulletSpeed = 5;
			attributes.BulletPenetration = 5;
			attributes.BulletDamage = 25;
		}

		protected override void OnDestroyEntity(Entity entity)
		{
			if (entity is Polygon)
			{
				score.Exp += ((Polygon)entity).Score;
				ScoreChanged?.Invoke(score);
			}
		}

		public void Shoot(Vector2 vec)
		{
			gun.Shoot(vec);
		}
	}
}
