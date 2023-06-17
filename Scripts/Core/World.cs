using Angar.Entities;
using Angar.Entities.Components;
using Angar.Entities.Polygons;
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

		private Timer spawnTimer;

		private static World instance;

		public HashSet<Entity> Entities { get { return entities; } }
		public static World Instance { get { return instance; } }

		public World()
		{
			instance = this;

			spawnTimer = new Timer();
			spawnTimer.Duration = 30;
			spawnTimer.Ended += () =>
			{
				SpawnPolygons(8, 4, 1);
				SpawnEnemy();

				spawnTimer.Start();
			};
			spawnTimer.Start();

			SpawnPolygons(30, 15, 5);
		}

		public void Update()
		{
			foreach (Entity entity in entities)
			{
				entity.Update();
			}

			while (entitesToAdd.Count > 0)
				entities.Add(entitesToAdd.Dequeue());

			spawnTimer.Update();
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

		public void SpawnPolygons(int count1, int count2, int count3)
		{
			SpawnPolygons<SquarePolygon>(count1);
			SpawnPolygons<TrianglePolygon>(count2);
			SpawnPolygons<PentagonPolygon>(count3);
		}

		public void SpawnPolygons<T>(int count) where T : Polygon, new()
		{
			for (int i = 0; i < count; i++)
			{
				T polygon = new T();
				polygon.Position = Utils.RandomMapPos();
				Entities.Add(polygon);
			}
		}

		public void SpawnEnemy()
		{
			Enemy enemy = new Enemy();
			enemy.Position = Utils.RandomMapPos();
			enemy.Scale = Utils.RandomSingle(1.0f, 1.5f);

			int points = Player.Instance.SpentLvls;
			for (int i = 0; i < points; i++)
				enemy.Attributes.AddPoint((Attributes)Utils.RandomInt(8));

			switch (Utils.RandomInt(3))
			{
				case 0:
					enemy.SetGun<StandardGunSet>();
					break;
				case 1:
					enemy.SetGun<TwinGunSet>();
					break;
				case 2:
					enemy.SetGun<SniperGunSet>();
					break;
			}

			Entities.Add(enemy);
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
