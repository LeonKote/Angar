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
			Globals.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, null, null, null, null, Camera.Instance.TransformMatrix);
			foreach (Entity entity in entities)
			{
				entity.Draw();
			}
			Globals.SpriteBatch.End();
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
