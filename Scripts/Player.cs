﻿using Angar.Entities;
using Angar.UI;
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
			robot.Color = new Color(0, 178, 225);
			robot.ScoreChanged += OnScoreChanged;
			World.Instance.AddEntity(robot);
			Canvas.Instance.AttributeAdded += OnAttributeAdd;

			camera = new Camera();

			endSize = Globals.nativeResolution + new Vector2(32, 32);
		}

		public void Update()
		{
			SetCameraPosition();
			SetBackgroundStartVec();
			RotateAndShoot();
			Move();

			if (Input.GetButtonDown(Keys.Q))
			{
				for (int i = 0; i < 50; i++)
				{
					Polygon point = new Polygon();
					point.Position = robot.Position + new Vector2(Utils.RandomSingle(-1024, 1024), Utils.RandomSingle(-1024, 1024));
					World.Instance.Entities.Add(point);
				}
			}
			else if (Input.GetButtonDown(Keys.E))
			{
				Enemy enemy = new Enemy();
				enemy.Position = robot.Position + new Vector2(Utils.RandomSingle(-512, 512), Utils.RandomSingle(-512, 512));
				World.Instance.Entities.Add(enemy);
			}
		}

		private void OnScoreChanged(ScoreHandler score)
		{
			ProgressBar scoreBar = Canvas.Instance.ScoreBar;
			Label scoreText = Canvas.Instance.ScoreText;

			scoreBar.MaxValue = score.NextLvlExp - score.LastLvlExp;
			scoreBar.Value = score.Exp - score.LastLvlExp;

			scoreText.Text = score.Lvl + "  LVL";
		}

		private void OnAttributeAdd(int id)
		{
			ProgressBar progressBar = (ProgressBar)Canvas.Instance.AbilitiesPanel.Childs[id];

			progressBar.Value++;

			robot.Attributes.AddPoint((Attributes)id);

			if (progressBar.Value == 7)
				((ImageButton)progressBar.Childs[1]).Interactable = false;
		}

		private void SetCameraPosition()
		{
			camera.Position = Vector2.Lerp(camera.Position, robot.Position, Globals.deltaTime * 5);
		}

		private void SetBackgroundStartVec()
		{
			startVec = camera.ScreenToWorldPoint(Vector2.Zero);
			startVec = new Vector2(MathF.Floor(startVec.X / 32) * 32, MathF.Floor(startVec.Y / 32) * 32);
		}

		private void RotateAndShoot()
		{
			Vector2 mousePos = camera.ScreenToWorldPoint(Input.MousePosition.ToVector2());

			robot.Rotation = MathF.Atan2(mousePos.Y - robot.Position.Y, mousePos.X - robot.Position.X);

			if (Input.GetMouseButton(0) && !Canvas.IsActive)
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

			if (movement.LengthSquared() > 0)
			{
				movement.Normalize();
				robot.AddForce(movement * 0.25f * (robot.Attributes.MovementSpeed * 0.1f + 1));
			}
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

		public void SetCameraZoom(float scale)
		{
			camera.Zoom = scale;
		}
	}
}
