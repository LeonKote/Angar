using Angar.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Angar
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		private World world;
		private Player player;
		private Canvas canvas;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;

			_graphics.PreferredBackBufferWidth = 1280;
			_graphics.PreferredBackBufferHeight = 720;

			Window.AllowUserResizing = true;
			Window.ClientSizeChanged += (sender, e) => OnClientSizeChanged();
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			Globals.graphicsDevice = GraphicsDevice;
			Globals.spriteBatch = _spriteBatch;
			Atlas.Init(Content);

			world = new World();
			canvas = new Canvas();
			player = new Player();

			OnClientSizeChanged();
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			Globals.SetGameTime(gameTime);
			Input.Update();
			canvas.Update();
			player.Update();
			world.Update();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			player.Draw();
			world.Draw();
			canvas.Draw();

			base.Draw(gameTime);
		}

		private void OnClientSizeChanged()
		{
			Rectangle bounds = GraphicsDevice.Viewport.Bounds;
			float scale = MathF.Max(bounds.Width / Globals.nativeResolution.X, bounds.Height / Globals.nativeResolution.Y);
			player.SetCameraZoom(scale);
			canvas.SetScale(scale);
		}
	}
}