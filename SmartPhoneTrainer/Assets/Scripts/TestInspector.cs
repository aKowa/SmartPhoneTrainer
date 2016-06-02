using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Test))]
public class TestInspector :  Editor
{
	private Test test;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		test = target as Test;
		if (GUILayout.Button("SetPosition"))
		{
			if (test != null) test.Set();
		}

	
	}

	private void OnSceneGUI()
	{
		if (test != null)
		{
			var rectTrans = test.BorderRectTransform.rect;
			DrawRectangle ( rectTrans );
		}
	}

	private void DrawRectangle(Rect rect)
	{
		var bottomLeft = new Vector2 ( rect.xMin, rect.yMin ) + rect.position;
		var bottomRight = new Vector2 ( rect.xMax, rect.yMin ) + rect.position;
		var topRight = new Vector2 ( rect.xMax, rect.yMax ) + rect.position;
		var topLeft = new Vector2 ( rect.xMin, rect.yMax ) + rect.position;
		Handles.color = Color.white;
		Handles.DrawLine ( bottomLeft, bottomRight );
		Handles.DrawLine ( bottomRight, topRight );
		Handles.DrawLine ( topRight, topLeft );
		Handles.DrawLine ( topLeft, bottomLeft );
	}
}
