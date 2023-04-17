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

		private Bar backgroundBar;
		private Bar foregroundBar;
		private Anim anim;

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
			get { return (int)anim.EndValue; }
			set
			{
				anim.Play(value);
			}
		}

		public ProgressBar()
		{
			backgroundBar = new Bar();
			backgroundBar.Color = new Color(85, 85, 85) * 0.75f;

			foregroundBar = new Bar();
			foregroundBar.Color = new Color(255, 232, 105) * 0.9f;

			anim = new Anim();
			anim.Duration = 0.25f;
			anim.IsCurve = true;
			anim.OnPlaying += (float t) =>
			{
				foregroundBar.Width = (rect.Size.X - innerSizeOffset.X) / maxValue * t;
			};
		}

		public override void Update()
		{
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
			backgroundBar.Rect = rect;
			foregroundBar.Rect = new Rectangle(rect.Location + innerPositionOffset, new Point(rect.Height, rect.Height) - innerSizeOffset);
		}
	}
}
