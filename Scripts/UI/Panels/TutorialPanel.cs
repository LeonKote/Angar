using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.UI
{
	public class TutorialPanel : Panel
	{
		private Label label;
		private ProgressBar bar;

		private Anim anim;
		private string currentText;
		private bool once;

		public TutorialPanel()
		{
			label = new Label();
			label.LocalPosition = new Vector2(35, 20);
			label.Anchor = Align.TopLeft;
			label.Origin = Align.TopLeft;
			label.Font = Resources.Rubik;
			label.LocalScale = 0.5f;
			AddChild(label);

			bar = new ProgressBar();
			bar.LocalPosition = new Vector2(0, -10);
			bar.Anchor = Align.Bottom;
			bar.Origin = Align.Bottom;
			bar.Size = new Vector2(270, 8);
			bar.BackgroundColor = Color.Transparent;
			bar.MaxValue = 100;
			AddChild(bar);

			anim = new Anim();
			anim.Duration = 0.5f;
			anim.IsCurve = true;
			anim.Playing += OnStageChangingAnim;
			anim.Ended += () => once = false;
		}

		private void OnStageChangingAnim(float t)
		{
			if (t < 0.5f)
			{
				LocalPosition = new Vector2(20 - 640 * t, 20);
			}
			else
			{
				if (!once)
				{
					SetText(currentText);
					bar.UpdateImmediate(0);
					once = true;
				}
				LocalPosition = new Vector2(640 * t - 620, 20);
			}
		}

		public override void Update()
		{
			base.Update();
			anim.Update();
		}

		public void UpdateProgress(float progress, bool immediate = false)
		{
			if (immediate) bar.UpdateImmediate(progress);
			else bar.Value = progress;
		}

		public void UpdateStage(string description, bool immediate = false)
		{
			if (immediate) SetText(description);
			else
			{
				currentText = description;
				anim.Play();
			}
		}

		private void SetText(string text)
		{
			label.Text = text;

			int count = text.Count(x => x == '\n') + 1;
			Size = new Vector2(300, 35 + count * 19);
		}
	}
}
