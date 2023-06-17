using Angar.Entities.Components;
using Angar.Entities.Polygons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities
{
	public class Tank : Entity
	{
		public GunSet gunSet; // protected

		private ScoreHandler score = new ScoreHandler();

		public event Action<ScoreHandler> ScoreChanged;

		public float Rotation { get { return gunSet.Rotation; } set { gunSet.Rotation = value; } }
		public ScoreHandler Score { get { return score; } set { score = value; } }

		public Tank()
		{
			friction = 0.95f;

			body.Texture = Utils.GetOutlineTexture(Resources.Circle, 0.5f);
			body.Origin = new Vector2(64, 64);
			body.Scale = 0.5f;

			gunSet = new StandardGunSet(this);
			components.Add(gunSet);

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

		public void OnDestroyEntity(Entity entity)
		{
			if (entity is Polygon polygon)
			{
				score.Exp += polygon.Score;
				ScoreChanged?.Invoke(score);
			}
			else if (entity is Tank tank)
			{
				score.Exp += 40;
				ScoreChanged?.Invoke(score);
			}
		}

		public void Shoot(Vector2 vec)
		{
			gunSet.ShootDelay(vec);
		}

		public void SetGun<T>() where T : GunSet
		{
			if (gunSet is T) return;
			float rotation = gunSet.Rotation;
			float scale = gunSet.Scale;
			components.Remove(gunSet);
			gunSet = (T)Activator.CreateInstance(typeof(T), this);
			gunSet.Rotation = rotation;
			gunSet.SetScale(scale);
			components.Add(gunSet);
		}
	}
}
