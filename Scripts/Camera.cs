using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar
{
	public class Camera
	{
		private Matrix transformMatrix;
		private Rectangle bounds;
		private Rectangle visibleArea;

		private Vector2 position;
		private float zoom = 1f;

		private static Camera main;

		public Matrix TransformMatrix { get { return transformMatrix; } }

		public Vector2 Position
		{
			get { return position; }
			set
			{
				position = value;
				UpdateMatrix();
			}
		}

		public Rectangle Bounds
		{
			get { return bounds; }
			set
			{
				bounds = value;
				UpdateMatrix();
			}
		}

		public float Zoom
		{
			get { return zoom; }
			set
			{
				zoom = value;
				UpdateMatrix();
			}
		}

		public Rectangle VisibleArea { get { return visibleArea; } }
		public static Camera Main { get { return main; } }

		public Camera(Rectangle bounds)
		{
			main ??= this;
			this.bounds = bounds;
			UpdateMatrix();
		}

		private void UpdateMatrix()
		{
			transformMatrix = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0))
				* Matrix.CreateScale(zoom, zoom, 1)
				* Matrix.CreateTranslation(new Vector3(bounds.Width * 0.5f, bounds.Height * 0.5f, 0));

			UpdateVisibleArea();
		}

		private void UpdateVisibleArea()
		{
			Matrix inverseViewMatrix = Matrix.Invert(transformMatrix);

			Vector2 tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
			Vector2 tr = Vector2.Transform(new Vector2(bounds.X, 0), inverseViewMatrix);
			Vector2 bl = Vector2.Transform(new Vector2(0, bounds.Y), inverseViewMatrix);
			Vector2 br = Vector2.Transform(new Vector2(bounds.Width, bounds.Height), inverseViewMatrix);

			Vector2 min = new Vector2(
				MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
				MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
			Vector2 max = new Vector2(
				MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
				MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));

			visibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
		}

		public Vector2 ScreenToWorldPoint(Vector2 vec)
		{
			return Vector2.Transform(vec, Matrix.Invert(transformMatrix));
		}

		public Vector2 WorldToScreenPoint(Vector2 vec)
		{
			return Vector2.Transform(vec, transformMatrix);
		}

		public void SetAsMain()
		{
			main = this;
		}
	}
}
