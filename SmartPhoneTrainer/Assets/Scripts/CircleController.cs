using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CircleController : MonoBehaviour
{
	public Rect SpawnRect;
	public Text ScoreText;
	public Text RecordText;
	public bool ResetPlayerPrefs = false;
	public float Decrease = 0.2f;
	public float MinSize = 0.1f;
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
	private int currentScore = 0;

	private void Awake ()
	{
		this.initPosition = this.transform.localPosition;
		this.initSize = this.transform.localScale;
		this.Feedback.text = "";

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
			var touchPoint = Camera.main.ScreenToWorldPoint(touch.position);
			var distance = Vector2.Distance(touchPoint, this.transform.position);
			if (distance - this.transform.localScale.x/2 < 0)
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
		var offset = this.transform.localScale.x / 2;
		this.transform.position = SpawnRect.GetInRectangle( new Vector2(offset, offset) ) + (Vector2)SpawnRect.position;
	}

	private void DecreaseSize()
	{
		var scale = this.transform.localScale.x - Decrease;
		this.transform.localScale = new Vector3(scale, scale, scale);
	}

	private void Reset()
	{
		currentScore = 0;
		ScoreText.text = currentScore + Score;
		this.transform.localPosition = initPosition;
		this.transform.localScale = initSize;
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
