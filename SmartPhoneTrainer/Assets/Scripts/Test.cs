using UnityEngine;

public class Test : MonoBehaviour
{
	public RectTransform BorderRectTransform;

	public void Start ()
	{
		Set();
	}

	public void Set()
	{
		var targetPosition = new Vector2( BorderRectTransform.rect.xMin, BorderRectTransform.rect.yMin );
		Debug.Log(targetPosition);
		transform.position = targetPosition;
	}
}
