using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities
{
	public class WrappedEntity
	{
		public Entity entity;
		public float dist;

		public WrappedEntity(Entity entity, float dist)
		{
			this.entity = entity;
			this.dist = dist;
		}
	}

	public class ClosestEntitiesHandler
	{
		private Dictionary<Type, WrappedEntity> dict = new Dictionary<Type, WrappedEntity>();

		public void HandleEntity(Entity entity, float dist)
		{
			Type type = entity.GetType();
			if (dict.ContainsKey(type))
			{
				if (dict[type].dist > dist)
					dict[type] = new WrappedEntity(entity, dist);
			}
			else
				dict.Add(type, new WrappedEntity(entity, dist));
		}

		public WrappedEntity Get<T>()
		{
			Type type = typeof(T);
			if (dict.ContainsKey(type))
				return dict[type];
			else return null;
		}

		public void Clear()
		{
			dict.Clear();
		}
	}
}
