using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CircleController : MonoBehaviour
{
	public RectTransform SpawnRect;
	public Text ScoreText;
	public Text RecordText;
	public bool ResetPlayerPrefs = false;
	public float Decrease = 5f;
	public float MinSize = 10f;
	public float TextWaitToFadeTime = 1f;
	public float TextFadeTime = 1f;
	private Vector2 initSize;
	private Vector2 initPosition;
	public Text Feedback;
	public string TooMany = "Zu viele Finger registriert.",
		Tapped = "Geschafft! Nochmal!",
		Outside = "Außerhalb des Kreises.",
		Score = " mal geschafft",
		Record = "Rekord: ";
	private RectTransform rect;
	private Color initFeedbackColor;
	private int currentScore = 0;

	private void Awake ()
	{
		rect = GetComponent<RectTransform>();
		initSize = rect.sizeDelta;
		initPosition = rect.localPosition;
		this.Feedback.text = "";
		this.initFeedbackColor = this.Feedback.color;

		ScoreText.text = "0" + Score;

		if (!PlayerPrefs.HasKey("Record"))
		{
			PlayerPrefs.SetInt( "Record", 0);
		}
		RecordText.text = Record + PlayerPrefs.GetInt("Record");
		RecordText.color = Color.red;
	}
	
	private void Update ()
	{
		if (ResetPlayerPrefs)
		{
			ResetPlayerPrefs = false;
			PlayerPrefs.DeleteAll();
			RecordText.text = Record + 0;
			RecordText.color = Color.red;
		}

		if (Input.touchCount <= 0) return;

		if (Input.touchCount > 1)
		{
			Reset();
			StopAllCoroutines ();
			StartCoroutine ( FadeOutText ( TooMany ) );
			return;
		}

		var touch = Input.GetTouch ( 0 );

		if (touch.phase == TouchPhase.Began)
		{
			var distance = Vector2.Distance(touch.position, this.transform.position);
			if (distance - rect.rect.height/2 < 0)
			{
				SetScore();
				DecreaseSize ();
				Reposition ();
				StopAllCoroutines ();
				StartCoroutine(FadeOutText( Tapped ) );
				return;
			}
			else
			{
				Reset();
				StopAllCoroutines ();
				StartCoroutine ( FadeOutText ( Outside ) );
				return;
			}
		}
	}

	private void SetScore()
	{
		currentScore++;
		ScoreText.text = currentScore + Score;
		if (currentScore > PlayerPrefs.GetInt( "Record" ))
		{
			PlayerPrefs.SetInt("Record", currentScore);
			RecordText.color = Color.green;
			RecordText.text = Record + PlayerPrefs.GetInt( "Record" );
		}
	}

	private void Reposition()
	{
		var offset = new Vector2( this.rect.rect.width / 2, this.rect.rect.height / 2 );
		this.transform.position = SpawnRect.rect.GetInRectangle( offset ) + (Vector2)SpawnRect.position;
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
		currentScore = 0;
		ScoreText.text = currentScore + Score;
		this.rect.localPosition = initPosition;
		this.rect.sizeDelta = initSize;
	}

	private IEnumerator FadeOutText(string s)
	{
		Feedback.text = s;
		Feedback.color = s == Tapped ? Color.green : Color.red;

		yield return new WaitForSeconds(TextWaitToFadeTime);

		var targetColor = Feedback.color;
		targetColor.a = 0;
		for (float t = 0; t <= 1; t += TextFadeTime * Time.deltaTime)
		{
			this.Feedback.color = Color.Lerp(Feedback.color, targetColor, t);
			yield return null;
		}
	}

	private void DebugMousePosition ()
	{
		var mousePos = Input.mousePosition;
		var viewort = Camera.main.ScreenToViewportPoint ( mousePos );
		var world = Camera.main.ScreenToWorldPoint ( mousePos );

		Debug.Log ( "Screen: " + mousePos + "  Viewport: " + viewort + "  World: " + world );
	}
}
