using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar.Entities
{
	public enum Attributes
	{
		HealthRegen,
		MaxHealth,
		BodyDamage,
		BulletSpeed,
		BulletPenetration,
		BulletDamage,
		Reload,
		MovementSpeed
	}

	public class AttributesHandler
	{
		private static readonly int attributeCount = Enum.GetNames(typeof(Attributes)).Length;
		private static readonly int[] attrubuteDeltas = new int[] { 1, 20, 5, 1, 5, 5, 1, 1 };

		private Entity entity;

		private int[] attributes = new int[attributeCount];

		public event Action<Attributes> AttributeChanged;

		public int HealthRegen { get { return Get(Attributes.HealthRegen); } set { Set(Attributes.HealthRegen, value); } }
		public int MaxHealth { get { return Get(Attributes.MaxHealth); } set { Set(Attributes.MaxHealth, value); } }
		public int BodyDamage { get { return Get(Attributes.BodyDamage); } set { Set(Attributes.BodyDamage, value); } }
		public int BulletSpeed { get { return Get(Attributes.BulletSpeed); } set { Set(Attributes.BulletSpeed, value); } }
		public int BulletPenetration { get { return Get(Attributes.BulletPenetration); } set { Set(Attributes.BulletPenetration, value); } }
		public int BulletDamage { get { return Get(Attributes.BulletDamage); } set { Set(Attributes.BulletDamage, value); } }
		public int Reload { get { return Get(Attributes.Reload); } set { Set(Attributes.Reload, value); } }
		public int MovementSpeed { get { return Get(Attributes.MovementSpeed); } set { Set(Attributes.MovementSpeed, value); } }

		public AttributesHandler(Entity entity)
		{
			this.entity = entity;
		}

		public int Get(Attributes attribute)
		{
			return attributes[(int)attribute];
		}

		public void Set(Attributes attribute, int value)
		{
			attributes[(int)attribute] = value;
			AttributeChanged?.Invoke(attribute);
		}

		public void AddPoint(Attributes attribute)
		{
			if (attribute == Attributes.MaxHealth)
				UpdateMaxHealth();

			attributes[(int)attribute] += attrubuteDeltas[(int)attribute];
			AttributeChanged?.Invoke(attribute);
		}

		private void UpdateMaxHealth()
		{
			int oldValue = Get(Attributes.MaxHealth);
			int newValue = oldValue + 20;

			entity.Health = entity.Health / oldValue * newValue;
		}
	}
}
