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
		private Point innerPositionOffset = new Point(4, 4);
		private Point innerSizeOffset = new Point(8, 8);

		private float maxValue;

		private Bar backgroundBar;
		private Bar foregroundBar;
		private Anim anim;

		public Color Color { get { return foregroundBar.Color; } set { foregroundBar.Color = value; } }
		public Color BackgroundColor { get { return backgroundBar.Color; } set { backgroundBar.Color = value; } }

		public float MaxValue
		{
			get { return maxValue; }
			set
			{
				maxValue = value;
			}
		}

		public float Value
		{
			get { return anim.EndValue; }
			set
			{
				anim.Play(value);
			}
		}

		public ProgressBar()
		{
			backgroundBar = new Bar();
			backgroundBar.Color = new Color(85, 85, 85) * 0.9f;

			foregroundBar = new Bar();

			anim = new Anim();
			anim.Duration = 0.25f;
			anim.IsCurve = true;
			anim.Playing += UpdateWidth;
		}

		public override void Update()
		{
			base.Update();
			anim.Update();
		}

		public override void Draw()
		{
			backgroundBar.Draw();
			foregroundBar.Draw();
			base.Draw();
		}

		protected override void ApplyTransform()
		{
			base.ApplyTransform();
			innerPositionOffset = new Point(rect.Height / 8);
			innerSizeOffset = new Point(innerPositionOffset.X * 2);
			backgroundBar.Rect = rect;
			foregroundBar.Rect = new Rectangle(rect.Location + innerPositionOffset, new Point((int)((rect.Width - innerSizeOffset.X) / maxValue * anim.CurrentValue), rect.Height - innerSizeOffset.Y));
		}

		private void UpdateWidth(float t)
		{
			foregroundBar.Width = (rect.Width - innerSizeOffset.X) / maxValue * t;
		}

		public void UpdateImmediate(float t)
		{
			anim.CurrentValue = t;
			UpdateWidth(t);
		}
	}
}
