using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.UI
{
	public partial class Canvas
	{
		private string[] abilitiesTexts =
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

		private Color[] abilitiesColors =
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

		public ProgressBar ScoreBar;
		public Label ScoreText;
		public Panel TutorialPanel;
		public BlankPanel AbilitiesPanel;

		private void InitializeComponent()
		{
			//
			// LevelBar
			//
			ScoreBar = new ProgressBar();
			ScoreBar.LocalPosition = new Vector2(0, -5);
			ScoreBar.Size = new Vector2(300, 32);
			ScoreBar.Anchor = Align.Bottom;
			ScoreBar.Origin = Align.Bottom;
			ScoreBar.Color = new Color(255, 232, 105) * 0.9f;
			elements.Add(ScoreBar);
			//
			// LevelText
			//
			ScoreText = new Label();
			ScoreText.Anchor = Align.Center;
			ScoreText.Origin = Align.Center;
			ScoreText.Font = Atlas.Rubik;
			ScoreText.Text = "1  LVL";
			ScoreText.LocalScale = 0.5f;
			ScoreBar.AddChild(ScoreText);
			//
			// TutorialPanel
			//
			/*TutorialPanel = new Panel();
			TutorialPanel.Size = new Vector2(300, 150);
			TutorialPanel.Anchor = Align.TopLeft;
			TutorialPanel.Origin = Align.TopLeft;
			elements.Add(TutorialPanel);*/

			//
			// AbilitiesPanel
			//
			CreateAbilitiesPanel();
		}

		private void CreateAbilitiesPanel()
		{
			AbilitiesPanel = new BlankPanel();
			AbilitiesPanel.LocalPosition = new Vector2(20, -20);
			AbilitiesPanel.Size = new Vector2(250, 227);
			AbilitiesPanel.Anchor = Align.BottomLeft;
			AbilitiesPanel.Origin = Align.BottomLeft;
			elements.Add(AbilitiesPanel);

			for (int i = 0; i < 8; i++)
			{
				ProgressBar bar = new ProgressBar();
				bar.LocalPosition = new Vector2(0, i * 29);
				bar.Size = new Vector2(250, 24);
				bar.Color = abilitiesColors[i];
				bar.MaxValue = 8;
				AbilitiesPanel.AddChild(bar);

				Label text = new Label();
				text.Anchor = Align.Center;
				text.Origin = Align.Center;
				text.Font = Atlas.Rubik;
				text.Text = abilitiesTexts[i];
				text.LocalScale = 0.45f;
				bar.AddChild(text);

				ImageButton button = new ImageButton();
				button.LocalPosition = new Vector2(-3, 0);
				button.Size = new Vector2(36, 18);
				button.Texture = Atlas.BarAdd;
				button.NativeColor = abilitiesColors[i];
				button.Anchor = Align.Right;
				button.Origin = Align.Right;
				button.Id = i;
				button.Click += OnAbilityAdded;
				bar.AddChild(button);
			}
		}
	}
}
