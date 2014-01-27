using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BreakoutWindows.Entities
{
	public class Powerup : Entity
	{
		private enum TYPE {EXTRA_BALL, PAD_SIZE, STICKY, EXPLOSIVE, POWER_BALL};
		public int type = 0;
		public Powerup(Rectangle box, Texture2D tex)
			: base(box, tex, new Vector2(0,200))
		{
			Random random = new Random(box.X * box.Y);
			this.type = random.Next(0,5);
		}

		public override void update(GameTime gameTime)
		{
			base.update(gameTime);

			if (collidesWith(GameObjects.paddle))
			{
				//EXECUTE POWERUP
				switch (type)
				{
					case (int) TYPE.EXTRA_BALL:
						GameObjects.balls.Add(new Ball(new Rectangle(GameObjects.paddle.boundingBox.X + GameObjects.paddle.boundingBox.Width / 2 - 5,
									GameObjects.paddle.boundingBox.Top - 10,
									20,
									20),
								GameObjects.textureBall,
								new Vector2(-300, -300)));
						break;
					case (int) TYPE.PAD_SIZE:
						POWERUPS.upgradeSize();
						break;
					case (int) TYPE.STICKY:
						POWERUPS.upgradeSticky();
						break;
					case (int) TYPE.POWER_BALL:
						POWERUPS.upgradePowerBall();
						break;
					case (int) TYPE.EXPLOSIVE:
						POWERUPS.upgradeExplosive();
						break;
					default:
						break;
				}
				GameObjects.destroyed.Add(this);
			}
		}

		public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			switch (type)
			{
				case (int)TYPE.EXTRA_BALL:
					spriteBatch.Draw(texture, boundingBox, Color.Blue);
					break;
				case (int)TYPE.PAD_SIZE:
					spriteBatch.Draw(texture, boundingBox, Color.Yellow);
					break;
				case (int)TYPE.STICKY:
					spriteBatch.Draw(texture, boundingBox, Color.Green);
					break;
				case (int)TYPE.POWER_BALL:
					spriteBatch.Draw(texture, boundingBox, Color.Orange);
					break;
				case (int)TYPE.EXPLOSIVE:
					spriteBatch.Draw(texture, boundingBox, Color.Red);
					break;
				default:
					spriteBatch.Draw(texture, boundingBox, Color.Red);
					break;
			}
		}
	}
}
