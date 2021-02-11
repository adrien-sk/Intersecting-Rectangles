
# Intersecting Rectangles

This is a Console application written with C# .Net Core, used to output the list of intersections between a given list of rectangles.

The application will request to input the path of a Json file containing a formatted list of rectangles coordinates.
It will calculate each possible intersections between all the provided rectangles, and then display the rectangles and their intersections.

## Json example
```json
{  
	"rects": [  
		{"x": 100, "y": 100, "delta_x": 250, "delta_y": 80 },  
		{"x": 120, "y": 200, "delta_x": 250, "delta_y": 150 },  
		{"x": 140, "y": 160, "delta_x": 250, "delta_y": 100 },  
		{"x": 160, "y": 140, "delta_x": 350, "delta_y": 190 }  
	]  
}
```
The input Json follows the rules : 

- Have at least 2 rectangles.
- Rectangles after the 10th first will be discarded.
- Rectangles with negative or null values will be discarded.
- It needs to have the same structure.

## Output result
```sh
Input:
        1: Rectangle  at (100,100), delta_x=250, delta_y=80.
        2: Rectangle  at (120,200), delta_x=250, delta_y=150.
        3: Rectangle  at (140,160), delta_x=250, delta_y=100.
        4: Rectangle  at (160,140), delta_x=350, delta_y=190.

Intersections:
        1: Between rectangle 1, 3 at (140,160), delta_x=210, delta_y=20.
        2: Between rectangle 1, 4 at (160,140), delta_x=190, delta_y=40.
        3: Between rectangle 2, 3 at (140,200), delta_x=230, delta_y=60.
        4: Between rectangle 2, 4 at (160,200), delta_x=210, delta_y=130.
        5: Between rectangle 3, 4 at (160,160), delta_x=230, delta_y=100.
        6: Between rectangle 1, 3, 4 at (160,160), delta_x=190, delta_y=20.
        7: Between rectangle 2, 3, 4 at (160,200), delta_x=210, delta_y=60.
```

## With Visual Studio
If you have Visual Studio installed, you should be able to easily do the following from Visual Studio directly :
- Explore the code.
- Build the application.
- Run the **IntersectingRectangles.Tests** Unit tests project.
- Run the **IntersectingRectangles** application project. 

## Without Visual Studio

With command line : Clone this repository and open the folder
```sh
git clone https://github.com/adrien-sk/Intersecting-Rectangles.git
```
```sh
cd Intersecting-Rectangles
```

### Build app

```sh
dotnet build
```

### Run app

```sh
dotnet run --project src/IntersectingRectangles
```
If you don't have a file for testing, you can use example data by typing the following :
```sh
src/IntersectingRectangles/Data/data.json
```
From debug in Visual Studio, the example data json is in a different place :
```sh
Data/data.json
```

### Run tests

```sh
dotnet test src/IntersectingRectangles.Tests
```

## Structure

### Program.cs

Application is launched from **Program.cs**, where we :
- Validate the input Json file
- Create the **IntersectionFinder** object that holds the rectangles
- Call the search for the intersections from the **IntersectionFinder** and displays to the user

### IntersectionFinder.cs

This class holds a list for rectangles and intersections, and is processing the search of the intersections.
It happens in multiple steps :
- We instantiate the object by loading and serializing the Json, and the **rectangles** list is populated and verified.
- We first find all the intersections involving exactly **2** rectangles, and we store them in a list for the next check.
- We verify for each item in this list, if another rectangle is involved in the intersection.
	- If a new intersection is found, we push it in the list so we can again check if more rectangles comes in.

### Rectangle.cs

This class represents a **Rectangle** or an **Intersection**
**Rectangles** are given an id at initialization, while **Intersections** are having a list of involved rectangles
It holds methods for comparison and checking of intersections of 2 rectangles.

## Licence
MIT
