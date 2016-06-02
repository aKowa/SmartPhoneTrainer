using UnityEngine;
using System.Collections;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine.EventSystems;

[CanEditMultipleObjects]
[CustomEditor(typeof(Border))]
public class BorderInspector : Editor
{
	private Border border;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
	}

	private void OnSceneGUI()
	{
		border = target as Border;
		if (border != null)
		{
			EditorGUI.BeginChangeCheck();
			var p1 = border.transform.position;
			var p2 = new Vector2(border.Rect.xMax, border.Rect.yMax);
			p2 = Handles.DoPositionHandle(p2, Quaternion.Euler(0, 0, 180));

			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(border, "Changed Border");
				EditorUtility.SetDirty(border);
				border.Rect.width = Mathf.Abs ( p1.x - p2.x );
				border.Rect.height = Mathf.Abs ( p1.y - p2.y );
				border.Rect.position = (Vector2)border.transform.position;
			}

			DrawRectangle(p1,p2);
			Handles.color = Color.green;
			Handles.SphereCap( 0, new Vector2 ( border.Rect.xMin, border.Rect.yMax ), Quaternion.identity, 0.2f );
			Handles.SphereCap( 0, new Vector2 ( border.Rect.xMax, border.Rect.yMin ), Quaternion.identity, 0.2f );
		}
	}

	private void DrawRectangle(Vector2 p1, Vector2 p2)
	{
		Handles.color = Color.yellow;
		var points = new Vector2[5];
		points[0] = p1;
		points[1] = new Vector2(p2.x, p1.y);
		points[2] = p2;
		points[3] = new Vector2(p1.x, p2.y);
		points[4] = p1;
		for (int i = 0; i < points.Length - 1; i++ )
		{
			Handles.DrawLine(points[i], points[i+1]);
		}
	}
}
