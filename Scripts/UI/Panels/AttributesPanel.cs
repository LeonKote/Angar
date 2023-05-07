﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.UI
{
	public class AttributesPanel : UIElement
	{
		private static readonly string[] attributeTexts =
		{
			"Health Regen",
			"Max Health",
			"Body Damage",
			"Bullet Speed",
			"Bullet Penetration",
			"Bullet Damage",
			"Reload",
			"Movement Speed"
		};

		private static readonly Color[] attributeColors =
		{
			new Color(238, 182, 144),
			new Color(236, 108, 240),
			new Color(154, 108, 240),
			new Color(108, 150, 240),
			new Color(240, 217, 108),
			new Color(240, 108, 108),
			new Color(152, 240, 108),
			new Color(108, 240, 236)
		};

		private static readonly Vector2 showPosition = new Vector2(20, -20);
		private static readonly Vector2 hidePosition = new Vector2(-250, -20);

		private Anim fadeAnim;
		private Timer fadeTimer;
		private bool isShown;

		public event Action<int> AttributeAdded;

		public AttributesPanel()
		{
			for (int i = 0; i < 8; i++)
			{
				ProgressBar bar = new ProgressBar();
				bar.LocalPosition = new Vector2(0, i * 29);
				bar.Size = new Vector2(250, 24);
				bar.Color = attributeColors[i];
				bar.MaxValue = 8;
				AddChild(bar);

				Label label = new Label();
				label.Anchor = Align.Center;
				label.Origin = Align.Center;
				label.Font = Atlas.Rubik;
				label.Text = attributeTexts[i];
				label.LocalScale = 0.45f;
				bar.AddChild(label);

				ImageButton button = new ImageButton();
				button.LocalPosition = new Vector2(-3, 0);
				button.Size = new Vector2(36, 18);
				button.Texture = Atlas.BarAdd;
				button.NativeColor = attributeColors[i];
				button.Anchor = Align.Right;
				button.Origin = Align.Right;
				button.Id = i;
				button.Click += OnAbilityAdded;
				bar.AddChild(button);
			}

			fadeAnim = new Anim();
			fadeAnim.Duration = 0.25f;
			fadeAnim.IsCurve = true;
			fadeAnim.Playing += OnFade;
			fadeAnim.Ended += () => isShown = !isShown;

			fadeTimer = new Timer();
			fadeTimer.Duration = 3f;
			fadeTimer.Ended += () => fadeAnim.Play();
		}

		private void OnFade(float t)
		{
			if (isShown) LocalPosition = Vector2.Lerp(showPosition, hidePosition, t);
			else LocalPosition = Vector2.Lerp(hidePosition, showPosition, t);
		}

		public override void Update()
		{
			base.Update();
			fadeAnim.Update();
			fadeTimer.Update();
		}

		public void Show()
		{
			if (isShown) return;
			SetInteractable(true);
			fadeAnim.Play();
		}

		public void Hide()
		{
			if (!isShown) return;
			SetInteractable(false);
			fadeTimer.Start();
		}

		private void SetInteractable(bool interactable)
		{
			for (int i = 0; i < 8; i++)
			{
				ProgressBar bar = (ProgressBar)Childs[i];
				if (bar.Value == 7) continue;
				((ImageButton)bar.Childs[1]).Interactable = interactable;
			}
		}

		private void OnAbilityAdded(int id)
		{
			AttributeAdded.Invoke(id);
		}
	}
}
