using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Polygons
{
	public class SquarePolygon : Polygon
	{
		public SquarePolygon()
		{
			body.Texture = Resources.Polygon1;
			body.NativeColor = new Color(255, 232, 105);
			body.Origin = new Vector2(32, 32);
			body.Scale = 0.75f;
		}

		protected override void SetAttributes()
		{
			score = 10;
			Health = 50;
			attributes.MaxHealth = 50;
			attributes.BodyDamage = 5;
		}
	}
}
