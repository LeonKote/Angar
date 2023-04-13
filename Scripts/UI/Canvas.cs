using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.UI
{
	public partial class Canvas
	{
		public HashSet<UIElement> elements = new HashSet<UIElement>();

		public static Canvas Instance;

		public Canvas()
		{
			Instance = this;
			InitializeComponent();
		}

		public void Update()
		{
			foreach (UIElement element in elements)
			{
				if (element.IsActive) element.Update();
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
	}
}
