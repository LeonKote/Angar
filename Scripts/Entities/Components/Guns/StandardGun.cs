using Microsoft.Xna.Framework;
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
			Texture = Utils.GetOutlineTexture(Atlas.Gun1, 0.25f, new Vector2(2f, 0));
			SourceRect = new Rectangle(64, 0, 64, 64);
			Scale = 0.45f;
		}

		public override void Draw()
		{
			Globals.spriteBatch.Draw(Texture, entity.Position, SourceRect, Color, Rotation, Origin, Scale, SpriteEffects.None, LayerDepth);
		}
	}
}
