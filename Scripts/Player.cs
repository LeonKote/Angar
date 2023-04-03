﻿using Angar.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar
{
	public class Player
	{
		private Robot robot;
		private Camera camera;

		private Vector2 startVec;
		private Vector2 endSize;

		public Player()
		{
			robot = new Robot();
			World.Main.AddEntity(robot);

			camera = new Camera(Globals.spriteBatch.GraphicsDevice.Viewport.Bounds);

			endSize = camera.Bounds.Size.ToVector2() + new Vector2(32, 32);
		}

		public void Update()
		{
			SetCameraPosition();
			SetBackgroundStartVec();
			RotateAndShoot();
			Move();
		}

		private void SetCameraPosition()
		{
			camera.Position = Vector2.Lerp(camera.Position, robot.Position, (float)Globals.gameTime.ElapsedGameTime.TotalSeconds * 5);
		}

		private void SetBackgroundStartVec()
		{
			startVec = camera.VisibleArea.Location.ToVector2();
			startVec = new Vector2(MathF.Floor(startVec.X / 32) * 32, MathF.Floor(startVec.Y / 32) * 32);
		}

		private void RotateAndShoot()
		{
			Vector2 mousePos = camera.ScreenToWorldPoint(Input.MousePosition.ToVector2());

			robot.Rotation = MathF.Atan2(mousePos.Y - robot.Position.Y, mousePos.X - robot.Position.X);

			if (Input.GetMouseButtonDown(0))
			{
				Vector2 rotVec = mousePos - robot.Position;
				rotVec.Normalize();

				robot.Shoot(rotVec);
			}
		}

		private void Move()
		{
			Vector2 movement = Vector2.Zero;

			if (Input.GetButton(Keys.W)) movement.Y -= 1;
			if (Input.GetButton(Keys.A)) movement.X -= 1;
			if (Input.GetButton(Keys.S)) movement.Y += 1;
			if (Input.GetButton(Keys.D)) movement.X += 1;
			if (movement.LengthSquared() > 0) movement.Normalize();

			robot.AddForce(movement * 0.25f);
		}

		public void Draw()
		{
			Globals.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.TransformMatrix);
			for (int x = 0; x < endSize.X; x += 32)
			{
				for (int y = 0; y < endSize.Y; y += 32)
				{
					Globals.spriteBatch.Draw(Atlas.Cell, startVec + new Vector2(x, y), Color.White);
				}
			}
			Globals.spriteBatch.End();
		}
	}
}
