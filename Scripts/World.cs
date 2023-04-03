using Angar.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar
{
	public class World
	{
		private HashSet<Entity> entities = new HashSet<Entity>();

		public static World Main;

		public World()
		{
			Main ??= this;

			for (int i = 0; i < 20; i++)
			{
				Polygon point = new Polygon();
				point.Position = new Vector2(Globals.RandomSingle(-512, 512), Globals.RandomSingle(-512, 512));
				entities.Add(point);
			}
		}

		public void Update()
		{
			foreach (Entity entity in entities)
			{
				entity.Update();
			}
		}

		public void Draw()
		{
			Globals.spriteBatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, Camera.Main.TransformMatrix);
			foreach (Entity entity in entities)
			{
				entity.Draw();
			}
			Globals.spriteBatch.End();
		}

		public void AddEntity(Entity entity)
		{
			entities.Add(entity);
		}

		public void RemoveEntity(Entity entity)
		{
			entities.Remove(entity);
		}
	}
}
