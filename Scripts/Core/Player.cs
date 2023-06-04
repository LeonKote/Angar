using Angar.Entities;
using Angar.Entities.Components;
using Angar.Entities.Polygons;
using Angar.UI;
using Microsoft.Xna.Framework;
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
		private Background background;

		private static Player instance;

		private int lvl = 1;
		private int spentLvls;

		public Robot Robot { get { return robot; } }
		public static Player Instance { get { return instance; } }

		public Player()
		{
			instance = this;
			camera = new Camera();

			SpawnPlayer();
			Canvas.Instance.AttributesPanel.AttributeAdded += OnAttributeAdd;
			
			background = new Background(camera);
		}

		public void Update()
		{
			SetCameraPosition();
			RotateAndShoot();
			Move();
			background.Update();


			// ==================== REFACTORING NEEDED ====================
			Canvas.Instance.MinimapPanel.SetPos(robot.Position);
			Canvas.Instance.MinimapPanel.SetRot(robot.Rotation);

			if (Input.GetButtonDown(Keys.Q)) // Spawn polygons
			{
				for (int i = 0; i < 25; i++)
				{
					SquarePolygon point = new SquarePolygon();
					point.Position = robot.Position + new Vector2(Utils.RandomSingle(-1024, 1024), Utils.RandomSingle(-1024, 1024));
					World.Instance.Entities.Add(point);
				}
				for (int i = 0; i < 25; i++)
				{
					TrianglePolygon point = new TrianglePolygon();
					point.Position = robot.Position + new Vector2(Utils.RandomSingle(-1024, 1024), Utils.RandomSingle(-1024, 1024));
					World.Instance.Entities.Add(point);
				}
			}
			else if (Input.GetButtonDown(Keys.E)) // Spawn enemy
			{
				Enemy enemy = new Enemy();
				enemy.Position = robot.Position + new Vector2(Utils.RandomSingle(-512, 512), Utils.RandomSingle(-512, 512));
				World.Instance.Entities.Add(enemy);
			}
			else if (Input.GetButtonDown(Keys.R)) // Grow player
			{
				robot.Scale *= 1.01f;
				camera.Zoom *= 0.99f;
				background.Size *= 1.01f;
			}
			else if (Input.GetButtonDown(Keys.T)) // Set twin gun
			{
				float scale = robot.gunSet.Scale;
				robot.components.Remove(robot.gunSet);
				robot.gunSet = new TwinGunSet(robot);
				robot.gunSet.SetScale(scale);
				robot.components.Add(robot.gunSet);
			}
			else if (Input.GetButtonDown(Keys.Y)) // Set sniper gun
			{
				float scale = robot.gunSet.Scale;
				robot.components.Remove(robot.gunSet);
				robot.gunSet = new SniperGunSet(robot);
				robot.gunSet.SetScale(scale);
				robot.components.Add(robot.gunSet);
			}
			else if (Input.GetButtonDown(Keys.U)) // Set standard gun
			{
				float scale = robot.gunSet.Scale;
				robot.components.Remove(robot.gunSet);
				robot.gunSet = new StandardGunSet(robot);
				robot.gunSet.SetScale(scale);
				robot.components.Add(robot.gunSet);
			}
			// ==================== REFACTORING NEEDED ====================
		}

		private void SpawnPlayer()
		{
			robot = new Robot();
			robot.Color = new Color(0, 178, 225);
			robot.Destroyed += OnDestroy;
			robot.ScoreChanged += OnScoreChanged;
			World.Instance.AddEntity(robot);

			camera.Zoom = 1.0f;
		}

		private void OnDestroy(Entity destroyer)
		{
			AttributesHandler attributes = robot.Attributes;
			ScoreHandler score = robot.Score;

			SpawnPlayer();
			robot.Attributes = attributes;
			robot.Score = score;
			robot.Position = Utils.RandomRect();
		}

		private void OnScoreChanged(ScoreHandler score)
		{
			ProgressBar scoreBar = Canvas.Instance.ScoreBar;
			Label scoreText = Canvas.Instance.ScoreText;

			scoreBar.MaxValue = score.NextLvlExp - score.LastLvlExp;
			scoreBar.Value = score.Exp - score.LastLvlExp;

			scoreText.Text = score.Lvl + "  LVL";

			if (score.Lvl - lvl > 0)
			{
				Canvas.Instance.AttributesPanel.Show();

				lvl = score.Lvl;

				Tutorial.Instance.SetProgress(TutorialStage.Lvl5, score.Lvl * 20);
				Tutorial.Instance.SetProgress(TutorialStage.Lvl10, score.Lvl * 10);
			}

			Tutorial.Instance.AddProgress(TutorialStage.Destroy, 50);
		}

		private void OnAttributeAdd(int id)
		{
			ProgressBar progressBar = (ProgressBar)Canvas.Instance.AttributesPanel.Childs[id];

			progressBar.Value++;

			robot.Attributes.AddPoint((Attributes)id);

			if (progressBar.Value == 7)
				((ImageButton)progressBar.Childs[1]).Interactable = false;

			spentLvls++;

			if (spentLvls == lvl - 1)
			{
				Canvas.Instance.AttributesPanel.Hide();
			}

			Tutorial.Instance.AddProgress(TutorialStage.AddAttribute, 100);
		}

		private void SetCameraPosition()
		{
			camera.Position = Vector2.Lerp(camera.Position, robot.Position, Globals.DeltaTime * 5);
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
				Tutorial.Instance.AddProgress(TutorialStage.Move, 0.5f, true);
			}
		}

		public void Draw()
		{
			background.Draw();
		}

		public void SetCameraZoom(float scale)
		{
			camera.Zoom = scale;
		}
	}
}
