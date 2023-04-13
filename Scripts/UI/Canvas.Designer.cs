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
			ScoreBar.Apply();
			elements.Add(ScoreBar);
			//
			// LevelText
			//
			ScoreText = new Label();
			ScoreText.Anchor = Align.Center;
			ScoreText.Origin = Align.Center;
			ScoreText.Font = Atlas.Rubik;
			ScoreText.Text = "1  LVL";
			ScoreText.LocalScale = 1f;
			ScoreBar.AddChild(ScoreText);
		}
	}
}
