using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Angar.Entities.Polygons
{
	public class PentagonPolygon : Polygon
	{
		public PentagonPolygon()
		{
			body.Texture = Resources.Polygon3;
			body.NativeSize = 56;
			body.NativeColor = new Color(118, 141, 252);
			body.Origin = new Vector2(64, 77);
			body.Scale = 0.6f;
		}

		protected override void SetAttributes()
		{
			score = 130;
			Health = 500;
			attributes.MaxHealth = 500;
			attributes.BodyDamage = 10;
		}
	}
}
