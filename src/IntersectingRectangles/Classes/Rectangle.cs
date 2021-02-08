using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IntersectingRectangles.Classes
{
	class Rectangle
	{
		[JsonIgnore]
		public int id { get; set; }

		[JsonPropertyName("x")]
		public double x { get; set; }

		[JsonPropertyName("y")]
		public double y { get; set; }

		[JsonPropertyName("delta_x")]
		public double deltaX { get; set; }

		[JsonPropertyName("delta_y")]
		public double deltaY { get; set; }
		public List<Rectangle> overlapingRectangles { get; set; }

		public Rectangle()
		{
			id = IntersectionFinder.GetNextID();
		}

		public Rectangle(Rectangle rectangle)
		{
			x = rectangle.x;
			y = rectangle.y;
			deltaX = rectangle.deltaX;
			deltaY = rectangle.deltaY;
			overlapingRectangles = new List<Rectangle>();
		}

		public Rectangle(int id, double x, double y, double deltaX, double deltaY)
		{
			this.id = id;
			this.x = x;
			this.y = y;
			this.deltaX = deltaX;
			this.deltaY = deltaY;
			overlapingRectangles = new List<Rectangle>();
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			return Equals(obj as Rectangle);
		}

		public bool Equals(Rectangle rectangle)
		{
			if (x == rectangle.x &&
				y == rectangle.y &&
				deltaX == rectangle.deltaX &&
				deltaY == rectangle.deltaY)
			{
				return true;
			}

			return false;
		}

		public bool IntersectWith(Rectangle otherRectangle)
		{
			double newX = Math.Max(x, otherRectangle.x);
			double newY = Math.Max(y, otherRectangle.y);
			double newDeltaX = Math.Min(x + deltaX, otherRectangle.x + otherRectangle.deltaX) - newX;
			double newDeltaY = Math.Min(y + deltaY, otherRectangle.y + otherRectangle.deltaY) - newY;

			if (newDeltaX <= 0 || newDeltaY <= 0)
				return false;

			return true;
		}

		public Rectangle GetIntersection(Rectangle otherRectangle)
		{
			double newX = Math.Max(x, otherRectangle.x);
			double newY = Math.Max(y, otherRectangle.y);
			double newDeltaX = Math.Min(x + deltaX, otherRectangle.x + otherRectangle.deltaX) - newX;
			double newDeltaY = Math.Min(y + deltaY, otherRectangle.y + otherRectangle.deltaY) - newY;

			Rectangle intersection = new Rectangle(0, newX, newY, newDeltaX, newDeltaY);

			return intersection;
		}

		public void AddOverlapingRectangles(Rectangle rectangle)
		{
			overlapingRectangles.Add(rectangle);
		}

		public void AddOverlapingRectangles(List<Rectangle> rectangles)
		{
			overlapingRectangles = rectangles;
		}

		public void DisplayCoordinates(int intersectionId = 0)
		{
			if (overlapingRectangles != null && overlapingRectangles.Count > 0)
			{
				Console.Write(intersectionId + ": Between rectangle ");
				overlapingRectangles.ForEach(x => Console.Write(x.id.ToString() + ", "));

			}
			else
			{
				Console.Write(id + ": Rectangle ");
			}

			Console.WriteLine("at (" + x + "," + y + "), delta_x=" + deltaX + ", delta_y=" + deltaY + ".");
		}
	}
}
