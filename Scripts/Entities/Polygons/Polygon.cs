using Angar.Entities;
using Angar.Entities.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Polygons
{
	public abstract class Polygon : Entity
	{
		private float rotMovement;
		protected int score;

		public int Score { get { return score; } }

		public Polygon()
		{
			friction = 1.0f;
			movement = new Vector2(Utils.RandomFloat(-0.1f, 0.1f), Utils.RandomFloat(-0.1f, 0.1f));
			rotMovement = Utils.RandomFloat(-0.005f, 0.005f);

			body.Rotation = Utils.RandomFloat(-MathF.PI, MathF.PI);
			body.LayerDepth = 0.3f;

			healthBar = new HealthBar(this);
			components.Add(healthBar);
		}

		public override void Update()
		{
			base.Update();
			body.Rotation = MathHelper.WrapAngle(body.Rotation + rotMovement);

			if (movement.LengthSquared() > 0.15f) movement *= 0.9f;
		}
	}
}
