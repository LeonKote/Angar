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

		public static World Instance;

		public HashSet<Entity> Entities { get { return entities; } }

		public World()
		{
			Instance ??= this;

			for (int i = 0; i < 50; i++)
			{
				Polygon point = new Polygon();
				point.Position = new Vector2(Utils.RandomSingle(-1024, 1024), Utils.RandomSingle(-1024, 1024));
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
			Globals.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, null, null, null, null, Camera.Main.TransformMatrix);
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
