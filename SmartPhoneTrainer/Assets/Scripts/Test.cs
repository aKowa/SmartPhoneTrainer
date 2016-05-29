using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour 
{
	private void Update ()
	{
		if (Input.touchCount <= 0) return;

		if (Input.touchCount > 1)
		{
			Debug.Log("Too many fingers");
			return;
		}

		if (Input.GetTouch(0).phase != TouchPhase.Began) return;

		var touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		var distance = Vector2.Distance(this.transform.position, touchPosition);

		if (distance - Input.GetTouch(0).radius - 0.625 < 0)
		{
			Debug.Log("Tapped");
		}
		else
		{
			Debug.Log("Outside");
		}
	}
}
