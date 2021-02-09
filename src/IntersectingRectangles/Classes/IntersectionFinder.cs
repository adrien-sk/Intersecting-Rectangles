using IntersectingRectangles.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace IntersectingRectangles.Classes
{
	/// <summary>
	///		Main Class for Intersection Finding treatment
	/// </summary>
	class IntersectionFinder
	{
		/// <summary>
		///		List of the Rectangles from Json file
		/// </summary>
		[JsonPropertyName("rects")]
		public List<Rectangle> rectangles { get; set; }

		/// <summary>
		///		Final list that store found intersections
		/// </summary>
		public List<Rectangle> intersectionsResult { get; set; }
		
		/// <summary>
		///		List that stores new intersections found along treatement to check if it has more rectangles overlapping
		/// </summary>
		public List<Rectangle> intersectionsToVerify { get; set; }

		/// <summary>
		///		Auto-incremented ID for Rectangles
		/// </summary>
		private static int nextId { get; set; }

		/// <summary>
		///		Constructor
		/// </summary>
		public IntersectionFinder()
		{
			intersectionsResult = new List<Rectangle>();
			intersectionsToVerify = new List<Rectangle>();
			nextId = 1;
		}

		/// <summary>
		///		Method called in Rectangle for ID incrementing
		/// </summary>
		/// <returns>Next available ID</returns>
		public static int GetNextID()
		{
			return nextId++;
		}

		public static IntersectionFinder LoadJson()
		{
			string jsonFile = @"C:/Users/Nadriel/source/repos/Intersecting-Rectangles/IntersectingRectangles/src/IntersectingRectangles/data.json";
			//if (File.Exists(jsonFile))
			//{
				var json = File.ReadAllText(jsonFile);
				IntersectionFinder instance = JsonSerializer.Deserialize<IntersectionFinder>(json);

				return instance;
			//}
		}

		public string FindIntersections()
		{
			Find2RectanglesIntersections();
			FindMultipleRectanglesIntersections();

			return GetInformations();
		}

		private void Find2RectanglesIntersections()
		{
			foreach (Rectangle rectangle in rectangles)
			{
				foreach (Rectangle rectangle2 in rectangles)
				{
					if (!rectangle.Equals(rectangle2) && rectangle.IntersectWith(rectangle2))
					{
						Rectangle inter = ObjectExtensions.Copy(new Rectangle(rectangle.GetIntersection(rectangle2)));
						if (!intersectionsResult.Contains(inter))
						{
							inter.AddOverlappingRectangles(ObjectExtensions.Copy(rectangle));
							inter.AddOverlappingRectangles(ObjectExtensions.Copy(rectangle2));

							intersectionsResult.Add(inter);
							intersectionsToVerify.Add(inter);
						}
					}
				}
			}
		}

		private void FindMultipleRectanglesIntersections()
		{
			while (intersectionsToVerify.Any())
			{
				Rectangle intersection = intersectionsToVerify.First();

				foreach (Rectangle rectangle in rectangles)
				{
					if (!intersection.overlappingRectangles.Contains(rectangle) && intersection.IntersectWith(rectangle))
					{
						Rectangle newIntersection = ObjectExtensions.Copy(intersection.GetIntersection(rectangle));
						newIntersection.AddOverlappingRectangles(ObjectExtensions.Copy(intersection.overlappingRectangles));
						newIntersection.AddOverlappingRectangles(rectangle);

						if (!intersectionsResult.Contains(newIntersection))
						{
							intersectionsToVerify.Add(newIntersection);
							intersectionsResult.Add(newIntersection);
						}
					}
				}

				intersectionsToVerify.Remove(intersectionsToVerify.First());
			}
		}


		private string GetInformations()
		{
			string informations = "Input:"+ Environment.NewLine;

			foreach (Rectangle rectangle in rectangles)
			{
				informations += rectangle.GetCoordinates();
			}

			var i = 0;

			informations += Environment.NewLine + Environment.NewLine +"Intersections:" + Environment.NewLine;

			foreach (Rectangle rectangle in intersectionsResult)
			{
				informations += rectangle.GetCoordinates(++i);
			}


			return informations;
		}
	}
}
