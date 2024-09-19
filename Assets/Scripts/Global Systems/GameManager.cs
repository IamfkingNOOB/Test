using System.Collections;
using UnityEngine;

internal class GameManager : MonoBehaviour
{
	internal void RunCoroutine(IEnumerator coroutine)
	{
		StartCoroutine(coroutine);
	}
}
