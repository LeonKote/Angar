using Angar.Entities.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities
{
	public class Polygon : Entity
	{
		private float rotMovement;
		private int score;

		public int Score { get { return score; } }

		public Polygon()
		{
			friction = 1.0f;
			movement = new Vector2(Utils.RandomSingle(-0.1f, 0.1f), Utils.RandomSingle(-0.1f, 0.1f));
			rotMovement = Utils.RandomSingle(-0.005f, 0.005f);
			score = 10;

			body.Texture = Atlas.Polygon1;
			body.NativeColor = new Color(255, 232, 105);
			body.Rotation = Utils.RandomSingle(-MathF.PI, MathF.PI);
			body.Origin = new Vector2(32, 32);
			body.Scale = 0.75f;
			body.LayerDepth = 0.3f;

			healthBar = new HealthBar(this);
			components.Add(healthBar);
		}

		protected override void SetAttributes()
		{
			Health = 50;
			attributes.MaxHealth = 50;
			attributes.BodyDamage = 5;
		}

		public override void Update()
		{
			base.Update();
			body.Rotation = MathHelper.WrapAngle(body.Rotation + rotMovement);

			if (movement.LengthSquared() > 0.15f) movement *= 0.9f;
		}
	}
}
