using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class WelcomeScreenController : MonoBehaviour
{
	private Animator anim;

	public void Awake ()
	{
		this.anim = GetComponent<Animator> ();
		var state = anim.GetCurrentAnimatorStateInfo(0);
		StartCoroutine ( LoadAfterTime(state.length) );
	}

	private IEnumerator LoadAfterTime( float t )
	{
		yield return new WaitForSeconds(t);
		SceneManager.LoadScene(1);
	}
}
