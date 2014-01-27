using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BreakoutWindows.HelperClasses

{
	public class RectangleFloat
	{
		public Vector2 position;
		public Vector2 size;

		#region Constructors
		public RectangleFloat(float x, float y, float w, float h)
		{
			position = new Vector2(x, y);
			size = new Vector2(w, h);
		}

		public RectangleFloat(Vector2 pos, Vector2 s)
		{
			position = pos;
			size = s;
		}
		#endregion

		#region Getters and setters
		public float X
		{
			get { return position.X; }
			set { position.X = value; }
		}

		public float Y
		{
			get { return position.Y; }
			set { position.Y = value; }
		}

		public float Width
		{
			get { return size.X; }
			set { size.X = value; }
		}

		public float Height
		{
			get { return size.Y; }
			set { size.Y = value; }
		}

		public float Left
		{
			get { return X; }
		}

		public float Right
		{
			get { return X + Width; }
		}

		public float Top
		{
			get { return Y; }
		}

		public float Bottom
		{
			get { return Y + Height; }
		}

		public Rectangle Rectangle
		{
			get { return new Rectangle((int)X, (int)Y, (int)Width, (int)Height); }
		}
		#endregion

		public bool Intersects(RectangleFloat rect)
		{
			bool cond1 = rect.Left > this.Right;
			bool cond2 = rect.Right < this.Left;
			bool cond3 = rect.Top > this.Bottom;
			bool cond4 = rect.Bottom < this.Top;

			return !cond1 && !cond2 && !cond3 && !cond4;
		}
	}
}
