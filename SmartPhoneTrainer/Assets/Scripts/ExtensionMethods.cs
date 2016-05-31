using UnityEngine;
using System.Collections;

public static class ExtensionMethods 
{
	public static Vector2 GetInRectangle ( this Rect rect, Vector2 recOffset )
	{
		return RandomPoint.GetInRectangle ( rect, recOffset );
	}

	public static Vector2 GetInRectangle ( this Rect rect )
	{
		return RandomPoint.GetInRectangle ( rect );
	}
}
