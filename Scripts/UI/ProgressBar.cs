using Angar.SmartTextures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.UI
{
	public class ProgressBar : UIElement
	{
		private static readonly Point innerPositionOffset = new Point(4, 4);
		private static readonly Point innerSizeOffset = new Point(8, 8);

		private float maxValue;

		private bool isChanging;
		private float timeElapsed;
		private float currentValue;
		private float startValue;
		private float endValue;

		private Bar backgroundBar;
		private Bar foregroundBar;

		public int MaxValue
		{
			get { return (int)maxValue; }
			set
			{
				maxValue = value;
			}
		}

		public int Value
		{
			get { return (int)endValue; }
			set
			{
				startValue = currentValue;
				endValue = value;
				timeElapsed = 0;
				isChanging = true;
			}
		}

		public ProgressBar()
		{
			backgroundBar = new Bar();
			backgroundBar.Color = new Color(85, 85, 85) * 0.75f;

			foregroundBar = new Bar();
			foregroundBar.Color = new Color(255, 232, 105) * 0.9f;
		}

		public override void Update()
		{
			if (!isChanging) return;

			if (timeElapsed < 0.25f)
			{
				float t = timeElapsed / 0.25f;
				t = t * t * (3f - 2f * t);
				currentValue = MathHelper.Lerp(startValue, endValue, t);
				foregroundBar.Width = (rect.Size.X - innerSizeOffset.X) / maxValue * currentValue;
				timeElapsed += Globals.deltaTime;
			}
			else
			{
				currentValue = endValue;
				foregroundBar.Width = (rect.Size.X - innerSizeOffset.X) / maxValue * currentValue;
				isChanging = false;
			}
		}

		public override void Draw()
		{
			backgroundBar.Draw();
			foregroundBar.Draw();
			base.Draw();
		}

		public void Apply()
		{
			backgroundBar.Rect = rect;
			foregroundBar.Rect = new Rectangle(rect.Location + innerPositionOffset, new Point(rect.Height, rect.Height) - innerSizeOffset);
		}
	}
}
