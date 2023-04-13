using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.UI
{
	public static class Align
	{
		public static Vector2 TopLeft = new Vector2(0, 0);
		public static Vector2 Top = new Vector2(0.5f, 0);
		public static Vector2 TopRight = new Vector2(1, 0);
		public static Vector2 Left = new Vector2(0, 0.5f);
		public static Vector2 Center = new Vector2(0.5f, 0.5f);
		public static Vector2 Right = new Vector2(1, 0.5f);
		public static Vector2 BottomLeft = new Vector2(0, 1);
		public static Vector2 Bottom = new Vector2(0.5f, 1);
		public static Vector2 BottomRight = new Vector2(1, 1);
	}

	public abstract class UIElement
	{
		protected Rectangle rect;
		protected Vector2 position;
		private Vector2 localPosition;
		private Vector2 size;
		private Vector2 anchor;
		protected Vector2 origin;
		protected float scale;
		private float localScale = 1f;
		private bool isActive = true;
		private UIElement parent;

		private HashSet<UIElement> childs = new HashSet<UIElement>();

		public Vector2 Position
		{
			get { return position; }
		}

		public Vector2 LocalPosition
		{
			get { return localPosition; }
			set
			{
				localPosition = value;
				ApplyTransform();
			}
		}

		public Vector2 Size
		{
			get { return size; }
			set
			{
				size = value;
				ApplyTransform();
			}
		}

		public Vector2 Anchor
		{
			get { return anchor; }
			set
			{
				anchor = value;
				ApplyTransform();
			}
		}

		public Vector2 Origin
		{
			get { return origin; }
			set
			{
				origin = value;
				ApplyTransform();
			}
		}

		public float LocalScale
		{
			get { return localScale; }
			set
			{
				localScale = value;
				ApplyTransform();
			}
		}

		public bool IsActive { get { return isActive; } set { isActive = value; } }

		public virtual void Update()
		{
			foreach (UIElement child in childs)
			{
				if (child.IsActive) child.Draw();
			}
		}

		public virtual void Draw()
		{
			foreach (UIElement child in childs)
			{
				if (child.IsActive) child.Draw();
			}
		}

		private void ApplyTransform()
		{
			scale = localScale;

			if (parent != null)
			{
				scale *= parent.scale;
				position = parent.rect.Location.ToVector2() + parent.rect.Size.ToVector2() * anchor + localPosition * scale;
			}
			else
			{
				position = Globals.spriteBatch.GraphicsDevice.Viewport.Bounds.Size.ToVector2() * anchor + localPosition;
			}

			Vector2 realSize = size * scale;

			rect.Location = (position - realSize * origin).ToPoint();
			rect.Size = realSize.ToPoint();

			foreach (UIElement child in childs)
			{
				child.ApplyTransform();
			}
		}

		public void AddChild(UIElement element)
		{
			element.parent = this;
			element.ApplyTransform();
			childs.Add(element);
		}
	}
}
