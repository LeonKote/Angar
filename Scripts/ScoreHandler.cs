﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angar
{
	public class ScoreHandler
	{
		int lvl = 1;
		int exp;
		int nextLvlExp;
		int lastLvlExp;

		public int Lvl
		{
			get { return lvl; }
			set
			{
				lvl = value;
			}
		}

		public int Exp
		{
			get { return exp; }
			set
			{
				exp = value;
				if (exp > nextLvlExp)
				{
					lvl++;
					lastLvlExp = nextLvlExp;
					nextLvlExp += GetExp(lvl + 1);
				}
			}
		}

		public int NextLvlExp { get { return nextLvlExp; } }
		public int LastLvlExp { get { return lastLvlExp; } }

		public ScoreHandler()
		{
			nextLvlExp = GetExp(2);
		}

		private int GetExp(int exp)
		{
			return (exp + 2) * (exp + 2);
		}
	}
}
