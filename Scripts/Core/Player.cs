using Angar.Entities;
using Angar.Entities.Components;
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
		private Tank tank;
		private Camera camera;
		private Background background;

		private int lvl = 1;
		private int spentLvls;
		private float screenScale = 1.0f;
		private float sizeScale = 1.0f;

		private static Player instance;

		public Tank Tank { get { return tank; } }
		public int SpentLvls { get { return spentLvls; } }
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

			Canvas.Instance.MinimapPanel.SetPos(tank.Position);
			Canvas.Instance.MinimapPanel.SetRot(tank.Rotation);

			if (Input.GetButtonDown(Keys.D1)) // Set standard gun
				SetGun<StandardGunSet>();
			else if (Input.GetButtonDown(Keys.D2)) // Set twin gun
				SetGun<TwinGunSet>();
			else if (Input.GetButtonDown(Keys.D3)) // Set sniper gun
				SetGun<SniperGunSet>();
		}

		private void SpawnPlayer(float scale = 1.0f)
		{
			tank = new Tank();
			tank.Scale = scale;
			tank.Color = new Color(0, 178, 225);
			tank.Destroyed += OnDestroy;
			tank.ScoreChanged += OnScoreChanged;
			World.Instance.AddEntity(tank);
		}

		private void OnDestroy(Entity destroyer)
		{
			AttributesHandler attributes = tank.Attributes;
			ScoreHandler score = tank.Score;

			SpawnPlayer(tank.Scale);
			tank.Position = Utils.RandomRect();
			tank.Attributes = attributes;
			tank.Score = score;
		}

		private void SetGun<T>() where T : GunSet
		{
			tank.SetGun<T>();
			Tutorial.Instance.AddProgress(TutorialStage.SwitchWeapons, 34);
		}

		private void OnScoreChanged(ScoreHandler score)
		{
			ProgressBar scoreBar = Canvas.Instance.ScoreBar;
			Label scoreText = Canvas.Instance.ScoreText;
			AttributesPanel attributesPanel = Canvas.Instance.AttributesPanel;

			scoreBar.MaxValue = score.NextLvlExp - score.LastLvlExp;
			scoreBar.Value = score.Exp - score.LastLvlExp;

			scoreText.Text = score.Lvl + "  LVL";

			if (score.Lvl - lvl > 0)
			{
				attributesPanel.UpdatePoints(score.Lvl - spentLvls - 1);
				attributesPanel.Show();

				lvl = score.Lvl;

				GrowPlayer();

				Tutorial.Instance.SetProgress(TutorialStage.Lvl5, score.Lvl * 20);
				Tutorial.Instance.SetProgress(TutorialStage.Lvl10, score.Lvl * 10);
			}

			Tutorial.Instance.AddProgress(TutorialStage.Destroy, 50);
		}

		private void OnAttributeAdd(int id)
		{
			AttributesPanel attributesPanel = Canvas.Instance.AttributesPanel;

			tank.Attributes.AddPoint((Attributes)id);

			spentLvls++;

			attributesPanel.UpdatePoints(lvl - spentLvls - 1);

			if (spentLvls == lvl - 1)
			{
				attributesPanel.Hide();
			}

			Tutorial.Instance.AddProgress(TutorialStage.AddAttribute, 100);
			Tutorial.Instance.SetProgress(TutorialStage.AddAttribute15, spentLvls * 7);
		}

		private void GrowPlayer()
		{
			tank.Scale *= 1.01f;
			background.Size *= 1.01f;

			sizeScale *= 0.99f;
			camera.Zoom = screenScale * sizeScale;
		}

		private void SetCameraPosition()
		{
			camera.Position = Vector2.Lerp(camera.Position, tank.Position, Globals.DeltaTime * 5);
		}

		private void RotateAndShoot()
		{
			Vector2 mousePos = camera.ScreenToWorldPoint(Input.MousePosition.ToVector2());

			tank.Rotation = MathF.Atan2(mousePos.Y - tank.Position.Y, mousePos.X - tank.Position.X);

			if (Input.GetMouseButton(0) && !Canvas.IsActive)
			{
				Vector2 rotVec = mousePos - tank.Position;
				rotVec.Normalize();

				tank.Shoot(rotVec);
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
				tank.AddForce(movement * 0.25f * (tank.Attributes.MovementSpeed * 0.1f + 1));
				Tutorial.Instance.AddProgress(TutorialStage.Move, 0.5f, true);
			}
		}

		public void Draw()
		{
			background.Draw();
		}

		public void SetCameraZoom(float scale)
		{
			screenScale = scale;
			camera.Zoom = screenScale * sizeScale;
		}
	}
}
