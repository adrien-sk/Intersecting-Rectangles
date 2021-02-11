using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace IntersectingRectangles.Classes
{
	/// <summary>
	///	Object representing a Rectangle. 
	///		Imported rectangle have an ID
	///	Intersections are Rectangles as well
	///		Intersections have involved rectangles
	/// </summary>
	public class Rectangle
	{
		// Auto-incremented ID
		private int id { get; set; }

		[JsonPropertyName("x")]
		public int x { get; set; }

		[JsonPropertyName("y")]
		public int y { get; set; }

		[JsonPropertyName("delta_x")]
		public int deltaX { get; set; }

		[JsonPropertyName("delta_y")]
		public int deltaY { get; set; }
		
		// List of the involved rectangles for an intersection rectangle
		private List<int> involvedRectangles { get; set; }


		/// <summary>
		/// Constructor for JSON Deserializer
		/// </summary>
		public Rectangle()
		{
			id = IntersectionFinder.GetNextID();
		}

		/// <summary>
		/// Constructor with Rectangle object
		/// </summary>
		/// <param name="rectangle">A Rectangle Object</param>
		public Rectangle(Rectangle rectangle)
		{
			id = 0;
			x = rectangle.x;
			y = rectangle.y;
			deltaX = rectangle.deltaX;
			deltaY = rectangle.deltaY;
			involvedRectangles = new List<int>();
		}

		/// <summary>
		/// Constructor with Rectangle values
		/// </summary>
		/// <param name="x">X position</param>
		/// <param name="y">Y position</param>
		/// <param name="deltaX">Width</param>
		/// <param name="deltaY">Height</param>
		public Rectangle(int x, int y, int deltaX, int deltaY)
		{
			id = 0;
			this.x = x;
			this.y = y;
			this.deltaX = deltaX;
			this.deltaY = deltaY;
			involvedRectangles = new List<int>();
		}

		/// <summary>
		/// Public Getter for ID
		/// </summary>
		/// <returns></returns>
		public int GetId()
		{
			return id;
		}

		/// <summary>
		/// Public Getter for InvolvedRectangles
		/// </summary>
		/// <returns></returns>
		public List<int> GetInvolvedRectangles()
		{
			return involvedRectangles;
		}

		/// <summary>
		/// Is the provided rectangle ID involved in the current intersection ?
		/// </summary>
		/// <param name="rectangleID"></param>
		/// <returns>True if is the rectangle is involved</returns>
		public bool IsInvolvedInIntersection(int rectangleID)
		{
			if (involvedRectangles.Contains(rectangleID))
				return true;
			else
				return false;
		}

		/// <summary>
		/// Verify if the Current rectangle intersect with the provided rectangle
		/// </summary>
		/// <param name="otherRectangle"></param>
		/// <returns>True if there is an intersection</returns>
		public bool IntersectWith(Rectangle otherRectangle)
		{
			int newX = Math.Max(x, otherRectangle.x);
			int newY = Math.Max(y, otherRectangle.y);
			int newDeltaX = Math.Min(x + deltaX, otherRectangle.x + otherRectangle.deltaX) - newX;
			int newDeltaY = Math.Min(y + deltaY, otherRectangle.y + otherRectangle.deltaY) - newY;

			if (newDeltaX <= 0 || newDeltaY <= 0)
				return false;

			return true;
		}

		/// <summary>
		/// Get the intersection rectangle between Current rectangle and Parameter rectangle
		/// </summary>
		/// <param name="otherRectangle"></param>
		/// <returns>The intersection rectangle object</returns>
		public Rectangle GetIntersection(Rectangle otherRectangle)
		{
			int newX = Math.Max(x, otherRectangle.x);
			int newY = Math.Max(y, otherRectangle.y);
			int newDeltaX = Math.Min(x + deltaX, otherRectangle.x + otherRectangle.deltaX) - newX;
			int newDeltaY = Math.Min(y + deltaY, otherRectangle.y + otherRectangle.deltaY) - newY;

			Rectangle intersection = new Rectangle(newX, newY, newDeltaX, newDeltaY);

			return intersection;
		}

		/// <summary>
		/// Add an involved rectangle for this intersection
		/// </summary>
		/// <param name="rectangle"></param>
		public void AddInvolvedRectangle(int rectangleID)
		{
			involvedRectangles.Add(rectangleID);
		}

		/// <summary>
		/// Add a list of involved rectangles
		/// </summary>
		/// <param name="rectangles"></param>
		public void AddInvolvedRectangles(List<int> rectanglesIDs)
		{
			if(rectanglesIDs != null)
				involvedRectangles = rectanglesIDs;
		}

		/// <summary>
		/// Return the informations about the current Rectangle
		/// Add involved rectangles if it's an intersection
		/// </summary>
		/// <param name="intersectionId"></param>
		/// <returns>
		/// A string like : 
		///		1: Rectangle at (100,100), delta_x=250, delta_y=80.
		///			or
		///		1: Between rectangle 1 and 3 at (140,160), delta_x=210, delta_y=20.
		/// </returns>
		public string GetCoordinates(int intersectionId = 0)
		{
			string coordinates = "\t";

			if (involvedRectangles != null && involvedRectangles.Count > 0)
			{
				coordinates += intersectionId + ": Between rectangle ";
				coordinates += string.Join(", ", involvedRectangles.Select(t => t));

			}
			else
			{
				coordinates += id + ": Rectangle ";
			}

			coordinates += " at (" + x + "," + y + "), delta_x=" + deltaX + ", delta_y=" + deltaY + "." + Environment.NewLine;

			return coordinates;
		}

		// Redefine Equals and GetHashCode for Object comparison
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
				deltaY == rectangle.deltaY &&
				id == rectangle.id)
			{
				return true;
			}

			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
