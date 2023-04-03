using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Components
{
	public class Body : Component
	{
		public Body(Entity entity) : base(entity)
		{
			Texture = Atlas.Body;
			Color = new Color(0, 178, 225);
			Origin = new Vector2(64, 64);
			Scale = 0.5f;
			LayerDepth = 1;
		}
	}
}
