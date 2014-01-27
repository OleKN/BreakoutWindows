using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BreakoutWindows.Entities;

namespace BreakoutWindows
{
	public static class Effects
	{
		private static float maxSpeed = 100;
		private static int particleAmount = 1000;
		private static int lifeTime = 500;
		private static int defaultSize = 3;
		private static float degPerParticle = (float)Math.PI * 2.0f / (float)particleAmount;

		public static void explode(Entity entity, int magnitude = 1)
		{
			float currentDeg = degPerParticle;
			for (int i = 0; i < particleAmount + particleAmount * (magnitude-1) / 4; i++)
			{
				Random random = new Random((int)(currentDeg * 1000 * entity.Center.X));
				Random random2 = new Random((int)(i * random.Next(1000) * entity.Center.Y));

				GameObjects.particles.Add(new Particle(new Vector2((float)random.NextDouble() * (entity.boundingBox.Right - entity.boundingBox.Left) + entity.boundingBox.Left,
																	(float)random.NextDouble() * (entity.boundingBox.Bottom - entity.boundingBox.Top) + entity.boundingBox.Top),
														magnitude * lifeTime * (0.5 * random.NextDouble()) + 0.5 * lifeTime,
														new Vector2((magnitude / 2 + 1) * (float)random.NextDouble() * maxSpeed * (float)Math.Cos(currentDeg),
																	(magnitude / 2 + 1) * (float)random2.NextDouble() * maxSpeed * (float)Math.Sin(currentDeg)),
														new Vector2(defaultSize + magnitude - 1, defaultSize + magnitude - 1)));
				currentDeg += degPerParticle;
			}
		}
	}
}
