using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Components
{
	public class Gun : Component
	{
		public Texture2D Texture;
		public float Power;

		public Gun(Entity entity) : base(entity)
		{
			Power = 5.0f;

			Texture = Atlas.Gun;
			NativeColor = new Color(153, 153, 153);
			Origin = new Vector2(0, 32);
			Scale = 0.5f;
			LayerDepth = 0.4f;
		}

		public void Shoot(Vector2 direction)
		{
			Projectile projectile = new Projectile(entity, 2);
			projectile.Position = entity.Position + direction * 64;
			projectile.AddForce(direction * Power);
			World.Instance.AddEntity(projectile);
		}

		public override void Draw()
		{
			Globals.spriteBatch.Draw(Texture, entity.Position, null, Color, Rotation, Origin, Scale, SpriteEffects.None, LayerDepth);
		}
	}
}
