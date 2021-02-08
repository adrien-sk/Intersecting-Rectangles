using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using IntersectingRectangles.Classes;
using IntersectingRectangles.Helpers;

namespace IntersectingRectangles
{
	class Program
	{

		static void Main(string[] args)
		{
			string jsonFile = @"C:/Users/Nadriel/source/repos/Intersecting-Rectangles/IntersectingRectangles/src/IntersectingRectangles/data.json";

			if (File.Exists(jsonFile))
			{
				var json = File.ReadAllText(jsonFile);
				IntersectionFinder intersectionFinder = JsonSerializer.Deserialize<IntersectionFinder>(json);





				// Treat 2 Levels intersections
				foreach (Rectangle rectangle in intersectionFinder.rectangles)
				{
					foreach (Rectangle rectangle2 in intersectionFinder.rectangles)
					{
						if (!rectangle.Equals(rectangle2) && rectangle.IntersectWith(rectangle2))
						{
							Rectangle inter = ObjectExtensions.Copy(new Rectangle(rectangle.GetIntersection(rectangle2)));
							if (!intersectionFinder.intersectionsResult.Contains(inter))
							{
								inter.AddOverlapingRectangles(ObjectExtensions.Copy(rectangle));
								inter.AddOverlapingRectangles(ObjectExtensions.Copy(rectangle2));

								intersectionFinder.intersectionsResult.Add(inter);
								intersectionFinder.intersectionsToVerify.Add(inter);
							}
						}
					}
				}

				// Treat higher Levels intersections
				while (intersectionFinder.intersectionsToVerify.Any())
				{
					Rectangle intersection = intersectionFinder.intersectionsToVerify.First();

					foreach (Rectangle rectangle in intersectionFinder.rectangles)
					{
						if (!intersection.overlapingRectangles.Contains(rectangle) && intersection.IntersectWith(rectangle))
						{
							Rectangle newIntersection = ObjectExtensions.Copy(intersection.GetIntersection(rectangle));
							newIntersection.AddOverlapingRectangles(ObjectExtensions.Copy(intersection.overlapingRectangles));
							newIntersection.AddOverlapingRectangles(rectangle);

							if (!intersectionFinder.intersectionsResult.Contains(newIntersection))
							{
								intersectionFinder.intersectionsToVerify.Add(newIntersection);
								intersectionFinder.intersectionsResult.Add(newIntersection);
							}
						}
					}

					intersectionFinder.intersectionsToVerify.Remove(intersectionFinder.intersectionsToVerify.First());
				}



				foreach (Rectangle rectangle in intersectionFinder.rectangles)
				{
					rectangle.DisplayCoordinates();
				}

				Console.WriteLine();

				var i = 0;

				foreach (Rectangle rectangle in intersectionFinder.intersectionsResult)
				{
					rectangle.DisplayCoordinates(++i);
				}

				Console.WriteLine();

			}
		}
	}
}
