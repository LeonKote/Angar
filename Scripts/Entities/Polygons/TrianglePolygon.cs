using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Polygons
{
	public class TrianglePolygon : Polygon
	{
		public TrianglePolygon()
		{
			body.Texture = Atlas.Polygon2;
			body.NativeSize = 16;
			body.NativeColor = new Color(252, 118, 119);
			body.Origin = new Vector2(32, 45);
			body.Scale = 1f;
		}

		protected override void SetAttributes()
		{
			score = 25;
			Health = 150;
			attributes.MaxHealth = 150;
			attributes.BodyDamage = 5;
		}
	}
}
