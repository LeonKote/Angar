using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.UI
{
	public partial class Canvas
	{
		private HashSet<UIElement> elements = new HashSet<UIElement>();

		private static Canvas instance;
		private static bool isActive;
		private static bool isClicked;

		public static Canvas Instance { get { return instance; } }
		public static bool IsActive { get { return isActive; } }

		public Canvas()
		{
			instance = this;
			InitializeComponent();
		}

		public void Update()
		{
			isClicked = false;
			foreach (UIElement element in elements)
			{
				if (element.IsActive) element.Update();
			}

			if (isClicked)
			{
				isActive = true;
			}
			else if (Input.GetMouseButtonDown(0))
			{
				isActive = false;
			}
		}

		public void Draw()
		{
			Globals.spriteBatch.Begin();
			foreach (UIElement element in elements)
			{
				if (element.IsActive) element.Draw();
			}
			Globals.spriteBatch.End();
		}

		public void SetScale(float scale)
		{
			foreach (UIElement element in elements)
			{
				element.LocalScale = scale;
			}
		}

		public static void SetActive()
		{
			isClicked = true;
		}
	}
}
