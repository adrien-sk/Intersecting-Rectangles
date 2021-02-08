using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;

namespace IntersectingRectangles.Classes
{
	class IntersectionFinder
	{
		[JsonPropertyName("rects")]
		public List<Rectangle> rectangles { get; set; }

		public List<Rectangle> intersectionsResult { get; set; }
		public List<Rectangle> intersectionsToVerify { get; set; }

		private static int nextId { get; set; }


		public IntersectionFinder()
		{
			intersectionsResult = new List<Rectangle>();
			intersectionsToVerify = new List<Rectangle>();
			nextId = 1;
		}

		public static int GetNextID()
		{
			return nextId++;
		}
	}
}
