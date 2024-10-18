using Cinemachine;
using UnityEngine;

public class TestScript2 : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera targetGroupCamera;
	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			targetGroupCamera.Priority = (targetGroupCamera.Priority == 9) ? 11 : 9;
		}
	}
}
