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
		private Queue<Entity> entitesToAdd = new Queue<Entity>();

		private static World instance;

		public HashSet<Entity> Entities { get { return entities; } }
		public static World Instance { get { return instance; } }

		public World()
		{
			instance = this;

			/*for (int i = 0; i < 50; i++)
			{
				Polygon point = new Polygon();
				point.Position = new Vector2(Utils.RandomSingle(-1024, 1024), Utils.RandomSingle(-1024, 1024));
				entities.Add(point);
			}*/

			Enemy enemy = new Enemy();
			enemy.Position = new Vector2(512, -256);
			entities.Add(enemy);
		}

		public void Update()
		{
			foreach (Entity entity in entities)
			{
				entity.Update();
			}
			while (entitesToAdd.Count > 0)
				entities.Add(entitesToAdd.Dequeue());
		}

		public void Draw()
		{
			Globals.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, null, null, null, null, Camera.Instance.TransformMatrix);
			foreach (Entity entity in entities)
			{
				entity.Draw();
			}
			Globals.spriteBatch.End();
		}

		public void AddEntity(Entity entity)
		{
			entitesToAdd.Enqueue(entity);
		}

		public void RemoveEntity(Entity entity)
		{
			entities.Remove(entity);
		}
	}
}
