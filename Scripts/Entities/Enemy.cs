﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities
{
	public class Enemy : Tank
	{
		private Vector2 target = new Vector2(1000);

		public Enemy()
		{
			body.NativeColor = new Color(241, 78, 84);
		}

		public override void Update()
		{
			base.Update();

			Tank player = Player.Instance.Tank;
			if (player == null) return;

			RotateAndShoot(player);
			FindPath(player);
			Move();
		}

		private void RotateAndShoot(Entity entity)
		{
			Vector2 pos = entity.Position + entity.Movement * 50;
			float target = MathF.Atan2(pos.Y - Position.Y, pos.X - Position.X);

			Rotation = Utils.LerpAngle(gunSet.Rotation, target, Globals.DeltaTime * 10);

			if (MathF.Abs(target - Rotation) < 0.5f)
			{
				Vector2 rotVec = pos - Position;
				rotVec.Normalize();

				Shoot(rotVec);
			}
		}

		private void FindPath(Entity entity)
		{
			if (Vector2.DistanceSquared(target, Position) > 100
				&& Vector2.DistanceSquared(target, entity.Position) < 262144) return;

			target = entity.Position + Utils.RandomRect();
		}

		private void Move()
		{
			Vector2 vec = target - Position;
			vec.Normalize();

			AddForce(vec * 0.25f);
		}
	}
}
