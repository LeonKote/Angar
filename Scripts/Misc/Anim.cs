using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar
{
	public class Anim
	{
		private bool isPlaying;
		private bool isCurve;
		private float time;
		private float duration;
		private float currentValue;
		private float startValue;
		private float endValue = 1.0f;

		public event Action<float> Playing;
		public event Action Ended;

		public bool IsPlaying { get { return isPlaying; } }
		public bool IsCurve { get { return isCurve; } set { isCurve = value; } }
		public float Duration { get { return duration; } set { duration = value; } }
		public float CurrentValue { get { return currentValue; } set { currentValue = value; } }
		public float EndValue { get { return endValue; } }

		public void Update()
		{
			if (!isPlaying) return;

			if (time < duration)
			{
				float t = time / duration;
				if (isCurve) t = t * t * (3f - 2f * t);
				currentValue = MathHelper.Lerp(startValue, endValue, t);
				time += Globals.DeltaTime;
				Playing.Invoke(currentValue);
			}
			else
			{
				currentValue = endValue;
				isPlaying = false;
				Playing.Invoke(currentValue);
				Ended?.Invoke();
			}
		}

		public void Play()
		{
			this.time = 0;
			this.isPlaying = true;
		}

		public void Play(float endValue)
		{
			this.startValue = currentValue;
			this.endValue = endValue;
			this.time = 0;
			this.isPlaying = true;
		}
	}
}
