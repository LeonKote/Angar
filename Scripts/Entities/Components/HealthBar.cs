using Angar.SmartTextures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities.Components
{
	public class HealthBar : Component
	{
		private static readonly Vector2 nativeOffset = new Vector2(-32, 48);
		private static readonly Vector2 nativeSize = new Vector2(64, 10);
		private static readonly Vector2 innerPositionOffset = new Vector2(2, 2);
		private static readonly Vector2 innerSizeOffset = new Vector2(4, 4);

		private Bar backgroundBar;
		private Bar foregroundBar;
		private Anim anim;

		private Vector2 offset;

		public override Color Color
		{
			get { return base.Color; }
			set
			{
				base.Color = value;
				backgroundBar.Color = Utils.SetAlpha(backgroundBar.Color, value.A);
				foregroundBar.Color = Utils.SetAlpha(foregroundBar.Color, value.A);
			}
		}

		public override float Scale
		{
			get { return base.Scale; }
			set
			{
				base.Scale = value;

				offset = nativeOffset * value;
				backgroundBar.Size = new Vector2(nativeSize.X * value, nativeSize.Y);
				foregroundBar.Width = (backgroundBar.Size.X - innerSizeOffset.X) * value / entity.MaxHealth * anim.CurrentValue;
			}
		}

		public HealthBar(Entity entity) : base(entity)
		{
			offset = nativeOffset;

			backgroundBar = new Bar();
			backgroundBar.Size = nativeSize;
			backgroundBar.Color = new Color(65, 65, 65);
			backgroundBar.LayerDepth = 0.6f;

			foregroundBar = new Bar();
			foregroundBar.Size = nativeSize - innerSizeOffset;
			foregroundBar.Color = new Color(133, 227, 125);
			foregroundBar.LayerDepth = 0.7f;

			anim = new Anim();
			anim.Duration = 0.1f;
			anim.IsCurve = true;
			anim.CurrentValue = entity.Health;
			anim.OnPlaying += (float t) =>
			{
				foregroundBar.Width = (backgroundBar.Size.X - innerSizeOffset.X) * Scale / entity.MaxHealth * t;
			};
		}

		public override void Update()
		{
			if (MathF.Abs(entity.Health - anim.EndValue) > 1) anim.Play(entity.Health);

			anim.Update();
		}

		public override void Draw()
		{
			backgroundBar.Position = entity.Position + offset;
			foregroundBar.Position = entity.Position + offset + innerPositionOffset;
			backgroundBar.Draw();
			foregroundBar.Draw();
		}
	}
}
