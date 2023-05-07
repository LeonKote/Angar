using Angar.SmartTextures;
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
		public ProgressBar ScoreBar;
		public Label ScoreText;
		public TutorialPanel TutorialPanel;
		public AttributesPanel AttributesPanel;

		private void InitializeComponent()
		{
			//
			// ScoreBar
			//
			ScoreBar = new ProgressBar();
			ScoreBar.LocalPosition = new Vector2(0, -5);
			ScoreBar.Size = new Vector2(300, 32);
			ScoreBar.Anchor = Align.Bottom;
			ScoreBar.Origin = Align.Bottom;
			ScoreBar.Color = new Color(255, 232, 105) * 0.9f;
			elements.Add(ScoreBar);
			//
			// ScoreText
			//
			ScoreText = new Label();
			ScoreText.Anchor = Align.Center;
			ScoreText.Origin = Align.Center;
			ScoreText.Font = Resources.Rubik;
			ScoreText.Text = "1  LVL";
			ScoreText.LocalScale = 0.5f;
			ScoreBar.AddChild(ScoreText);
			//
			// TutorialPanel
			//
			TutorialPanel = new TutorialPanel();
			TutorialPanel.LocalPosition = new Vector2(20, 20);
			TutorialPanel.Size = new Vector2(300, 80);
			TutorialPanel.Anchor = Align.TopLeft;
			TutorialPanel.Origin = Align.TopLeft;
			TutorialPanel.Color = new Color(85, 85, 85) * 0.5f;
			elements.Add(TutorialPanel);
			//
			// AbilitiesPanel
			//
			AttributesPanel = new AttributesPanel();
			AttributesPanel.LocalPosition = new Vector2(-250, -20);
			AttributesPanel.Size = new Vector2(250, 227);
			AttributesPanel.Anchor = Align.BottomLeft;
			AttributesPanel.Origin = Align.BottomLeft;
			elements.Add(AttributesPanel);
		}
	}
}
