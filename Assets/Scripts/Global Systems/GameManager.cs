using System.Collections;
using UnityEngine;

internal class GameManager : MonoBehaviour
{
	public void RunCoroutine(IEnumerator coroutine)
	{
		StartCoroutine(coroutine);
	}
}
