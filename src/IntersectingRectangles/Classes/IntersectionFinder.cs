using IntersectingRectangles.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IntersectingRectangles.Classes
{
	/// <summary>
	/// 
	///	Main Class for Intersection Finding algorithm.
	///	
	/// Different steps :
	/// - First :	GetIntersections is called from outside.
	/// - Second :	We loop through all the rectangles to find all intersections involving exactly 2 rectangles.
	///				These are stored in the final list "intersections", but also in "intersectionsToVerify", for the next step.
	///	- Third :	We loop through all "intersectionsToVerify" to see if it involves more than 2 rectangles.
	///					If yes : It is added to the final list, and we add this new intersection to be verified again for more.
	///					If no : We don't need to check anymore, and we can remove the intersection from the list "intersectionsToVerify".
	///	- Fourth :	When don't have anymore "intersectionsToVerify", we can return all the informations about the Rectangles and the Intersections.
	///	
	/// </summary>
	class IntersectionFinder
	{
		//	List of the Rectangles from Json file
		[JsonPropertyName("rects")]
		public List<Rectangle> rectangles { get; set; }

		//	Final list that stores all Intersections to output
		private List<Rectangle> intersections { get; set; }

		//	List that stores new intersections found along processing, and requires to check if it has more rectangles overlapping
		private List<Rectangle> intersectionsToVerify { get; set; }

		//	Auto-incremented ID for Rectangles import
		private static int nextId { get; set; }


		/// <summary>
		///	Constructor
		/// </summary>
		public IntersectionFinder()
		{
			intersections = new List<Rectangle>();
			intersectionsToVerify = new List<Rectangle>();
			nextId = 1;
		}

		/// <summary>
		///	Method called in Rectangle Class for ID incrementing
		/// </summary>
		/// <returns>Next available ID</returns>
		public static int GetNextID()
		{
			return nextId++;
		}

		/// <summary>
		/// Static function called to load JSON file into an IntersectionFinder Object
		/// Json file needs to be validated first using JsonFileVerifications Class
		/// </summary>
		/// <param name="jsonFilePath">Path to the JSON file</param>
		/// <returns>IntersectionFinder Object with a List of Rectangle</returns>
		public static IntersectionFinder LoadJson(string jsonFilePath)
		{
			IntersectionFinder instance = null;

			try
			{
				//	Get the Json and deserialize into an Object
				var json = File.ReadAllText(jsonFilePath);
				instance = JsonSerializer.Deserialize<IntersectionFinder>(json);

				//	Remove all rectangles having null or negatives value
				List<Rectangle> temporaryList = ObjectExtensions.Copy(instance.rectangles);
				foreach (Rectangle item in temporaryList)
				{
					if(item.x <= 0 || item.y <= 0 || item.deltaX <= 0 || item.deltaY <= 0)
					{
						instance.rectangles.Remove(item);
					}
				}

				//	Limit the list to the first 10 elements
				if (instance.rectangles.Count() > 10)
					instance.rectangles.RemoveRange(10, instance.rectangles.Count()-10);

				//	If we have less than 2 rectangles, we exit here
				if (instance.rectangles.Count() < 2)
				{
					Console.WriteLine("There's not enough rectangles for having intersections.");
					return null;
				}
			}
			catch (JsonException)
			{
				return null;
			}

			return instance;
		}

		/// <summary>
		/// Starting method to find all intersections and return the recap
		/// </summary>
		/// <returns>Multiline string with informations from GetInformations()</returns>
		public string GetIntersections()
		{
			FindIntersectionsWithTwoRectangles();
			FindIntersectionsWithMultipleRectangles();

			return GetInformations();
		}

		/// <summary>
		/// Check all pairs of rectangles to find intersections involving 2 rectangles
		/// </summary>
		private void FindIntersectionsWithTwoRectangles()
		{
			foreach (Rectangle rectangleA in rectangles)
			{
				foreach (Rectangle rectangleB in rectangles)
				{
					//	If : The pair is not the same rectangle && They have an intersection
					if (rectangleA.GetId() != rectangleB.GetId() && rectangleA.IntersectWith(rectangleB))
					{
						//	Create a new Intersection Rectangle
						Rectangle newIntersection = new Rectangle(rectangleA.GetIntersection(rectangleB));

						//	List the involved rectangles
						newIntersection.AddInvolvedRectangles(rectangleA.GetId());
						newIntersection.AddInvolvedRectangles(rectangleB.GetId());

						//	If : we don't already have this intersection
						if (IsIntersectionDistinct(newIntersection))
						{
							//	Add the intersection rectangle to the list of intersections
							intersections.Add(newIntersection);
							//	Add the intersection rectangle to the Verification list, Where we'll check if other rectangles are involved
							intersectionsToVerify.Add(newIntersection);
						}
					}
				}
			}
		}

		/// <summary>
		/// Go through all intersections to verify if more than 2 rectangles are involved in the intersection
		/// </summary>
		private void FindIntersectionsWithMultipleRectangles()
		{
			//	While we have intersections to verify
			while (intersectionsToVerify.Any())
			{
				//	Take the first one
				Rectangle intersection = intersectionsToVerify.First();

				//	For each rectangle from JSON
				foreach (Rectangle rectangle in rectangles)
				{
					//	If : The rectangle is not yet involved && It has an intersection with current intersection
					if (!intersection.IsInvolvedInIntersection(rectangle.GetId()) && intersection.IntersectWith(rectangle))
					{
						//	Create a new intersection Rectangle
						Rectangle newIntersection = intersection.GetIntersection(rectangle);

						//	List the involved rectangles
						newIntersection.AddInvolvedRectangles(ObjectExtensions.Copy(intersection.GetInvolvedRectangles()));
						newIntersection.AddInvolvedRectangles(rectangle.GetId());

						//	If : we don't already have this intersection
						if (IsIntersectionDistinct(newIntersection))
						{
							intersectionsToVerify.Add(newIntersection);
							intersections.Add(newIntersection);
						}
					}
				}

				intersectionsToVerify.Remove(intersectionsToVerify.First());
			}
		}

		/// <summary>
		/// Verify if there is an existing intersection with the involving the same rectangles
		/// </summary>
		/// <param name="newIntersection"></param>
		/// <returns></returns>
		private bool IsIntersectionDistinct(Rectangle newIntersection)
		{
			foreach (Rectangle intersection in intersections)
			{
				if (intersection.x == newIntersection.x &&
					intersection.y == newIntersection.y &&
					intersection.deltaX == newIntersection.deltaX &&
					intersection.deltaY == newIntersection.deltaY 
					)
				{
					if (intersection.GetInvolvedRectangles().All(newIntersection.GetInvolvedRectangles().Contains) && intersection.GetInvolvedRectangles().Count == newIntersection.GetInvolvedRectangles().Count)
					{
						return false;
					}

				}
			}

			return true;
		}

		/// <summary>
		/// Return the formatted output informations for Rectangles and Intersections
		/// </summary>
		/// <returns>Multiline string with informations</returns>
		private string GetInformations()
		{
			//	Imported rectangles informations
			string informations = "Input:"+ Environment.NewLine;

			foreach (Rectangle rectangle in rectangles)
			{
				informations += rectangle.GetCoordinates();
			}

			//	Rectangles intersections informations
			var i = 0;
			informations += Environment.NewLine + Environment.NewLine +"Intersections:" + Environment.NewLine;

			foreach (Rectangle rectangle in intersections)
			{
				informations += rectangle.GetCoordinates(++i);
			}

			return informations;
		}
	}
}
