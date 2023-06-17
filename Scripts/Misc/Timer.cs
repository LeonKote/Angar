using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar
{
	public class Timer
	{
		private bool isPlaying;
		private float time;
		private float duration;

		public event Action Ended;
		public event Action WhileEnded;

		public float Duration { get { return duration; } set { duration = value; } }

		public void Update()
		{
			if (!isPlaying)
			{
				WhileEnded?.Invoke();
				return;
			}

			if (time < duration)
			{
				time += Globals.DeltaTime;
			}
			else
			{
				isPlaying = false;
				Ended?.Invoke();
			}
		}

		public void Start()
		{
			this.time = 0;
			this.isPlaying = true;
		}
	}
}
