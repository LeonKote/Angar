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
		private Anim fadeAnim;
		private bool isShown;

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
				UpdateWidth(anim.CurrentValue);
			}
		}

		public HealthBar(Entity entity) : base(entity)
		{
			offset = nativeOffset;

			entity.Attributes.AttributeChanged += OnAttributeChanged;

			backgroundBar = new Bar();
			backgroundBar.Size = nativeSize;
			backgroundBar.Color = new Color(65, 65, 65);
			backgroundBar.LayerDepth = 0.6f;

			foregroundBar = new Bar();
			foregroundBar.Size = nativeSize - innerSizeOffset;
			foregroundBar.Color = new Color(133, 227, 125);
			foregroundBar.LayerDepth = 0.7f;

			Color = Utils.SetAlpha(Color, 0);

			anim = new Anim();
			anim.Duration = 0.1f;
			anim.IsCurve = true;
			anim.CurrentValue = entity.Health;
			anim.Playing += UpdateWidth;

			fadeAnim = new Anim();
			fadeAnim.Duration = 0.25f;
			fadeAnim.Playing += OnFade;
		}

		private void OnFade(float t)
		{
			t *= 255;
			if (!isShown) t = 255 - t;
			Color = Utils.SetAlpha(Color, (byte)t);
		}

		private void OnAttributeChanged(Attributes attribute)
		{
			if (attribute != Attributes.MaxHealth) return;
			anim.CurrentValue = entity.Health;
		}

		private void UpdateWidth(float t)
		{
			if (entity.Health < entity.Attributes.MaxHealth)
			{
				if (!isShown)
				{
					fadeAnim.Play();
					isShown = true;
				}
			}
			else
			{
				if (isShown)
				{
					fadeAnim.Play();
					isShown = false;
				}
			}
			foregroundBar.Width = (backgroundBar.Size.X - innerSizeOffset.X) / entity.Attributes.MaxHealth * t;
		}

		public override void Update()
		{
			anim.Update();
			fadeAnim.Update();
		}

		public override void Draw()
		{
			backgroundBar.Position = entity.Position + offset;
			foregroundBar.Position = entity.Position + offset + innerPositionOffset;
			backgroundBar.Draw();
			foregroundBar.Draw();
		}

		public void UpdateHealth(bool immediate = false)
		{
			if (immediate)
			{
				anim.CurrentValue = entity.Health;
				UpdateWidth(anim.CurrentValue);
			}
			else anim.Play(entity.Health);
		}
	}
}
