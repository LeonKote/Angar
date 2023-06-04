using Angar.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar
{
	public enum TutorialStage
	{
		Move,
		Destroy,
		AddAttribute,
		Lvl5,
		Lvl10
	}

	public class Tutorial
	{
		private static readonly string[] stageDescriptions =
		{
			"Для передвижения используйте\nWASD",
			"Уничтожьте пару полигонов\n\nПолигоны - это геометрические\nфигуры, которые дают\nнебольшое количество очков\nопыта",
			"Выберите один из восьми\nатрибутов для прокачки",
			"Наберите 5-й уровень,\nуничтожая полигоны",
			"Достигнете 10-го уровня\nуничтожая врагов или полигоны",
			"Развлекайтесь)"
		};

		private static Tutorial instance;

		private TutorialPanel tutorialPanel;
		private TutorialStage stage;
		private float progress;

		public static Tutorial Instance { get { return instance; } }

		public Tutorial()
		{
			instance = this;
			tutorialPanel = Canvas.Instance.TutorialPanel;
			tutorialPanel.UpdateStage(stageDescriptions[(int)stage], true);
		}

		public void AddProgress(TutorialStage stage, float progress, bool immediate = false)
		{
			if (this.stage != stage) return;
			this.progress += progress;
			UpdateProgress(immediate);
		}

		public void SetProgress(TutorialStage stage, float progress, bool immediate = false)
		{
			if (this.stage != stage) return;
			this.progress = progress;
			UpdateProgress(immediate);
		}

		private void UpdateProgress(bool immediate = false)
		{
			if (progress >= 100)
			{
				stage++;
				progress = 0;
				tutorialPanel.UpdateStage(stageDescriptions[(int)stage]);
				return;
			}

			tutorialPanel.UpdateProgress(progress, immediate);
		}
	}
}
