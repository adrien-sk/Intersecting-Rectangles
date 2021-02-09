using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace IntersectingRectangles.Classes
{
	/// <summary>
	///		Object representing a Rectangle
	///		Intersections are Rectangles as well
	/// </summary>
	class Rectangle
	{
		/// <summary>
		///		Auto-incremented ID
		/// </summary>
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

		public List<Rectangle> overlappingRectangles { get; set; }


		/// <summary>
		///		Constructor for JSON Deserializer
		/// </summary>
		public Rectangle()
		{
			id = IntersectionFinder.GetNextID();
		}

		/// <summary>
		///		Constructor
		/// </summary>
		/// <param name="rectangle">A Rectangle Object</param>
		public Rectangle(Rectangle rectangle)
		{
			id = 0;
			x = rectangle.x;
			y = rectangle.y;
			deltaX = rectangle.deltaX;
			deltaY = rectangle.deltaY;
			overlappingRectangles = new List<Rectangle>();
		}

		/// <summary>
		///		Constructor
		/// </summary>
		/// <param name="x">X position</param>
		/// <param name="y">Y position</param>
		/// <param name="deltaX">Width</param>
		/// <param name="deltaY">Height</param>
		public Rectangle(double x, double y, double deltaX, double deltaY)
		{
			id = 0;
			this.x = x;
			this.y = y;
			this.deltaX = deltaX;
			this.deltaY = deltaY;
			overlappingRectangles = new List<Rectangle>();
		}

		/// <summary>
		///		Verify if the Current rectangle intersect with the Parameter rectangle
		/// </summary>
		/// <param name="otherRectangle"></param>
		/// <returns>Boolean</returns>
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

		/// <summary>
		///		Get the intersection rectangle between Current rectangle and Parameter rectangle
		/// </summary>
		/// <param name="otherRectangle"></param>
		/// <returns>The intersection rectangle</returns>
		public Rectangle GetIntersection(Rectangle otherRectangle)
		{
			double newX = Math.Max(x, otherRectangle.x);
			double newY = Math.Max(y, otherRectangle.y);
			double newDeltaX = Math.Min(x + deltaX, otherRectangle.x + otherRectangle.deltaX) - newX;
			double newDeltaY = Math.Min(y + deltaY, otherRectangle.y + otherRectangle.deltaY) - newY;

			Rectangle intersection = new Rectangle(newX, newY, newDeltaX, newDeltaY);

			return intersection;
		}

		/// <summary>
		///		Add an Overlapping rectangle
		/// </summary>
		/// <param name="rectangle"></param>
		public void AddOverlappingRectangles(Rectangle rectangle)
		{
			overlappingRectangles.Add(rectangle);
		}

		/// <summary>
		///		Add list of Overlapping rectangles
		/// </summary>
		/// <param name="rectangles"></param>
		public void AddOverlappingRectangles(List<Rectangle> rectangles)
		{
			overlappingRectangles = rectangles;
		}

		/// <summary>
		///		Return the informations about the current Rectangle
		///		Add intersecting rectangles if it's an intersection
		/// </summary>
		/// <param name="intersectionId"></param>
		/// <returns>
		///		A string like : 
		///			1: Rectangle at (100,100), delta_x=250, delta_y=80.
		///				or
		///			1: Between rectangle 1 and 3 at (140,160), delta_x=210, delta_y=20.
		/// </returns>
		public string GetCoordinates(int intersectionId = 0)
		{
			string coordinates = "\t";

			if (overlappingRectangles != null && overlappingRectangles.Count > 0)
			{
				coordinates += intersectionId + ": Between rectangle ";
				coordinates += string.Join(", ", overlappingRectangles.Select(t => t.id));

			}
			else
			{
				coordinates += id + ": Rectangle ";
			}

			coordinates += " at (" + x + "," + y + "), delta_x=" + deltaX + ", delta_y=" + deltaY + "." + Environment.NewLine;

			return coordinates;
		}

		/// <summary>
		///		An overriden Equals function
		///		To compare object as Rectangle
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>Does the object Equals ?</returns>
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
	}
}
