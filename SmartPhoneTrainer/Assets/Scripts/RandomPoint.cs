using UnityEngine;
using System.Collections;

public static class RandomPoint
{
	/// <summary>
	/// Gets a random point as vector2 inside the passed rectangle.
	/// </summary>
	/// <param name="rect">The Rectangle to describe.</param>
	/// <returns>A random Vector2 inside the passed rectangle.</returns>
	public static Vector2 GetInRectangle(Rect rect)
	{
		return GetInRectangle(rect, Vector2.zero);
	}

	/// <summary>
	/// Gets a random point as vector2 inside the passed rectangle while not crossing the border.
	/// </summary>
	/// <param name="rect">The Rectangle to describe.</param>
	/// <param name="recOffset">The offset to the border. Use the rectangles size to prevent it crossing the rectangle</param>
	/// <returns>A random Vector2 inside the passed rectangle.</returns>
	public static Vector2 GetInRectangle ( Rect rect, Vector2 recOffset )
	{
		var x = Random.Range ( rect.xMin + recOffset.x, rect.xMax - recOffset.x );
		var y = Random.Range ( rect.yMin + recOffset.y, rect.yMax - recOffset.y );
		var vec = new Vector2 ( x, y );
		return vec;
	}
}
