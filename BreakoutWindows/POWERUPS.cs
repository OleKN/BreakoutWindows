using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using BreakoutWindows.Entities;

namespace BreakoutWindows
{
	public static class POWERUPS
	{
		public static bool explosive = false;
		//public static bool powerBall = false;
		public static bool sticky = false;
		public static int sizeUpgrade = 0;

		private static int explosiveEnd;
		//private static int powerBallEnd;
		private static int powerBallDamage = 3;
		private static int sizeUpgradeEnd;
		private static int stickyEnd;
		private static readonly int duration = 5000;


		public static void update(GameTime gameTime)
		{
			explosive = true;
			/*if (explosiveEnd > 0)
				explosiveEnd -= gameTime.ElapsedGameTime.Milliseconds;
			else
				explosive = false;
			*/
			/*if (powerBallEnd > 0)
				powerBallEnd -= gameTime.ElapsedGameTime.Milliseconds;
			else
				powerBall = false;*/

			if (sizeUpgradeEnd > 0)
				sizeUpgradeEnd -= gameTime.ElapsedGameTime.Milliseconds;
			else
				sizeUpgrade = 0;

			if (stickyEnd > 0)
				stickyEnd -= gameTime.ElapsedGameTime.Milliseconds;
			else
			{
				sticky = false;
				GameObjects.paddle.fire();
			}
		}

		public static void upgradePowerBall()
		{
			foreach (Ball ball in GameObjects.balls)
			{
				ball.penetrationCounter += powerBallDamage;
			}

			//powerBall = true;
			//explosiveEnd += duration;
		}

		public static void upgradeExplosive()
		{
			explosive = true;
			explosiveEnd += duration;
		}

		public static void upgradeSize()
		{
			sizeUpgrade++;
			sizeUpgradeEnd += duration;
		}

		public static void upgradeSticky()
		{
			sticky = true;
			stickyEnd += duration;
		}
	}
}
