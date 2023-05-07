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
		private Matrix inverseTransformMatrix;

		private Vector2 position;
		private float zoom = 1.0f;

		private static Camera instance;

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

		public float Zoom
		{
			get { return zoom; }
			set
			{
				zoom = value;
				UpdateMatrix();
			}
		}

		public static Camera Instance { get { return instance; } }

		public Camera()
		{
			instance = this;
			UpdateMatrix();
		}

		private void UpdateMatrix()
		{
			Rectangle bounds = Globals.GraphicsDevice.Viewport.Bounds;

			transformMatrix = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0))
				* Matrix.CreateScale(zoom, zoom, 1)
				* Matrix.CreateTranslation(new Vector3(bounds.Width * 0.5f, bounds.Height * 0.5f, 0));

			inverseTransformMatrix = Matrix.Invert(transformMatrix);
		}

		public Vector2 ScreenToWorldPoint(Vector2 vec)
		{
			return Vector2.Transform(vec, inverseTransformMatrix);
		}

		public Vector2 WorldToScreenPoint(Vector2 vec)
		{
			return Vector2.Transform(vec, transformMatrix);
		}
	}
}
