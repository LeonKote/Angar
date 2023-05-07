using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.UI
{
	public class ImageButton : UIElement
	{
		private Texture2D texture;
		private Color nativeColor;
		private Color color;
		private bool interactable = true;
		private int id;

		public Texture2D Texture { get { return texture; } set { texture = value; } }
		public Color NativeColor
		{
			get { return nativeColor; }
			set
			{
				nativeColor = value;
				color = value;
			}
		}
		public bool Interactable
		{
			get { return interactable; }
			set
			{
				interactable = value;

				if (interactable)
				{
					color = nativeColor;
					if (rect.Contains(Input.MousePosition))
						Mouse.SetCursor(MouseCursor.Hand);
				}
				else
				{
					color = nativeColor * 0.5f;
					if (rect.Contains(Input.MousePosition))
						Mouse.SetCursor(MouseCursor.Arrow);
				}
			}
		}
		public int Id { get { return id; } set { id = value; } }

		public event Action<int> Click;

		public override void Draw()
		{
			Globals.SpriteBatch.Draw(texture, rect, color);
			base.Draw();
		}

		protected override void OnPointerClick()
		{
			if (!interactable) return;

			color = nativeColor * 0.9f;
			Click.Invoke(id);
		}

		protected override void OnPointerEnter()
		{
			if (!interactable) return;

			color = nativeColor * 1.1f;
			Mouse.SetCursor(MouseCursor.Hand);
		}

		protected override void OnPointerExit()
		{
			if (!interactable) return;

			color = nativeColor;
			Mouse.SetCursor(MouseCursor.Arrow);
		}

		protected override void OnPointerUp()
		{
			if (!interactable) return;

			if (isHover) color = nativeColor * 1.1f;
			else color = nativeColor;
		}
	}
}
