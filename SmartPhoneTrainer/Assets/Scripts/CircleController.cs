using UnityEngine;
using UnityEngine.UI;

public class CircleController : MonoBehaviour
{
	public RectTransform SpawnRect;
	public float Decrease = 5f;
	public float MinSize = 10f;
	private Vector2 initSize;
	private Vector2 initPosition;
	public Text Feedback;
	public string TooMany = "Zu viele Finger registriert.",
		Tapped = "Geschafft! Nochmal!",
		Outside = "Außerhalb des Kreises.";
	private RectTransform rect;

	private void Awake ()
	{
		rect = GetComponent<RectTransform>();
		initSize = rect.sizeDelta;
		initPosition = rect.localPosition;
	}

	private void Update ()
	{
		var mousePos = Input.mousePosition;
		var viewort = Camera.main.ScreenToViewportPoint(mousePos);
		var world = Camera.main.ScreenToWorldPoint( mousePos );

		Debug.Log("Screen: " + mousePos + "  Viewport: " + viewort + "  World: " + world);

		if (Input.touchCount <= 0) return;

		if (Input.touchCount > 1)
		{
			Reset();
			Feedback.text = TooMany;
			return;
		}

		var touch = Input.GetTouch ( 0 );

		if (touch.phase == TouchPhase.Began)
		{
			var distance = Vector2.Distance(touch.position, this.transform.position);
			if (distance - rect.rect.height/2 < 0)
			{
				Feedback.text = Tapped;
				DecreaseSize();
				//Reposition();
				return;
			}
			else
			{
				Reset();
				Feedback.text = Outside;
				return;
			}
		}
	}

	private void Reposition()
	{
		var offset = rect.rect.height/2;
		var x = Random.Range ( SpawnRect.rect.xMin + offset, SpawnRect.rect.xMax - offset );
		var y = Random.Range ( SpawnRect.rect.yMin + offset, SpawnRect.rect.yMax - offset );
		this.rect.localPosition = Camera.main.ViewportToScreenPoint(new Vector2(x,y));
	}

	private void DecreaseSize()
	{
		var h = rect.sizeDelta.x - Decrease;
		var w = rect.sizeDelta.y - Decrease;
		h = Mathf.Clamp ( h, MinSize, 100f ); 
		w = Mathf.Clamp ( w, MinSize, 100f );

		rect.sizeDelta = new Vector2(h, w);
	}

	private void Reset()
	{
		this.rect.localPosition = initPosition;
		this.rect.sizeDelta = initSize;
	}
}
