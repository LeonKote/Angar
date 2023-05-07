using Angar.SmartTextures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.UI
{
	public class Panel : UIElement
	{
		private RoundedRect roundedRect;

		public Color Color { get { return roundedRect.Color; } set { roundedRect.Color = value; } }

		public Panel()
		{
			roundedRect = new RoundedRect();
		}

		public override void Draw()
		{
			roundedRect.Draw();
			base.Draw();
		}

		protected override void ApplyTransform()
		{
			base.ApplyTransform();
			roundedRect.Rect = rect;
		}
	}
}
