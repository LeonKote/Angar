using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities
{
	public class Polygon : Entity
	{
		private float rotMovement;

		public Polygon()
		{
			friction = 1.0f;
			movement = new Vector2(Globals.RandomSingle(-0.1f, 0.1f), Globals.RandomSingle(-0.1f, 0.1f));
			rotMovement = Globals.RandomSingle(-0.005f, 0.005f);
			body.Texture = Atlas.Polygon1;
			body.Color = Color.White;
			body.Rotation = Globals.RandomSingle(-MathF.PI, MathF.PI);
			body.Origin = new Vector2(32, 32);
			body.Scale = 0.75f;
			body.LayerDepth = 0.1f;
		}

		public override void Update()
		{
			base.Update();
			body.Rotation = MathHelper.WrapAngle(body.Rotation + rotMovement);
		}
	}
}
