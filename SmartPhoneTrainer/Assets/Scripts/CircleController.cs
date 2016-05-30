using UnityEngine;
using System.Collections;

public class CircleController : MonoBehaviour
{
	private RectTransform rect;

	private void Awake ()
	{
		rect = GetComponent<RectTransform>();
	}

	private void Update ()
	{

		if (Input.touchCount <= 0) return;

		if (Input.touchCount > 1)
		{
			Debug.Log ( "Too many fingers." );
			return;
		}

		var touch = Input.GetTouch ( 0 );

		if (touch.phase == TouchPhase.Began)
		{
			var distance = Vector2.Distance(touch.position, this.transform.position);
			if (distance - rect.rect.height/2 < 0)
			{
				Debug.Log("Tapped");
			}
			else
			{
				Debug.Log("Outside");
			}
		}
	}
}
