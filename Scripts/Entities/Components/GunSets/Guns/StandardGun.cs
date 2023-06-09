﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Components
{
	public class StandardGun : Gun
	{
		public StandardGun(Entity entity) : base(entity)
		{
			Texture = Utils.GetOutlineTexture(Resources.RoundedRect2, 0.25f, new Vector2(2f, 0));
			Scale = 0.45f;
		}

		public override void Draw()
		{
			Globals.SpriteBatch.Draw(Texture, entity.Position, SourceRect, Color, Rotation, Origin, Scale, SpriteEffects.None, LayerDepth);
		}

		public override void SetScale(float scale)
		{
			Scale = scale;
			Texture = Utils.GetOutlineTexture(Resources.RoundedRect2, scale * (5 / 9f), new Vector2(2f, 0));
		}
	}
}
