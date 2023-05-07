using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar
{
	public static class Input
	{
		private static int mouseScrollDelta;
		private static int lastMouseScroll;

		private static MouseState mouseState;
		private static MouseState lastMouseState;

		private static KeyboardState keyboardState;
		private static KeyboardState lastKeyboardState;

		public static Point MousePosition { get { return mouseState.Position; } }
		public static int MouseScrollDelta { get { return mouseScrollDelta; } }

		public static void Update()
		{
			lastMouseState = mouseState;
			lastMouseScroll = mouseState.ScrollWheelValue;
			lastKeyboardState = keyboardState;

			mouseState = Mouse.GetState();
			keyboardState = Keyboard.GetState();

			if (mouseState.ScrollWheelValue > lastMouseScroll)
			{
				mouseScrollDelta = 1;
			}
			else if (mouseState.ScrollWheelValue < lastMouseScroll)
			{
				mouseScrollDelta = -1;
			}
			else if (mouseScrollDelta != 0) mouseScrollDelta = 0;
		}

		public static bool GetButton(Keys button)
		{
			return keyboardState.IsKeyDown(button);
		}

		public static bool GetButtonDown(Keys button)
		{
			return keyboardState.IsKeyDown(button) && !lastKeyboardState.IsKeyDown(button);
		}

		public static bool GetMouseButton(int button)
		{
			if (button == 0)
			{
				return mouseState.LeftButton == ButtonState.Pressed;
			}
			if (button == 1)
			{
				return mouseState.RightButton == ButtonState.Pressed;
			}
			if (button == 2)
			{
				return mouseState.MiddleButton == ButtonState.Pressed;
			}
			return false;
		}

		public static bool GetMouseButtonDown(int button)
		{
			if (button == 0)
			{
				return mouseState.LeftButton == ButtonState.Pressed
					&& lastMouseState.LeftButton == ButtonState.Released;
			}
			if (button == 1)
			{
				return mouseState.RightButton == ButtonState.Pressed
					&& lastMouseState.RightButton == ButtonState.Released;
			}
			if (button == 2)
			{
				return mouseState.MiddleButton == ButtonState.Pressed
					&& lastMouseState.MiddleButton == ButtonState.Released;
			}
			return false;
		}
	}
}
