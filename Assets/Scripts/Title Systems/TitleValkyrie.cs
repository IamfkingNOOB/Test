using System.Collections.Generic;
using System.IO;
using DataSystem;
using UnityEngine;

namespace TitleSystem
{
	/// <summary>
	/// [클래스] 메인 화면의 발키리를 관리합니다.
	/// </summary>
	internal class TitleValkyrie : MonoBehaviour
	{
		#region 변수(필드)
		
		// [변수] 메인 화면에 생성한 발키리의 모델(게임 오브젝트)
		private GameObject _createdModel;
		
		#endregion 변수(필드)
		
		#region 함수(메서드)
		
		// [유니티 생명 주기 함수] OnEnable()
		private void OnEnable()
		{
			CreateModel(ref _createdModel); // 메인 화면에 발키리 모델을 생성합니다.
		}
		
		// [함수] 메인 화면에 발키리 모델을 생성합니다.
		private void CreateModel(ref GameObject valkyrieModel)
		{
			DestroyModel(valkyrieModel); // 기존의 발키리 모델을 파괴합니다.
			
			// PlayerPrefs에 저장되어 있는, 메인 화면에 보여줄 발키리의 식별자(ID)를 가져옵니다.
			int id = PlayerPrefs.GetInt("titleValkyrieID", 0);
			
			// Resources 폴더에 접근하여, 식별자에 해당하는 발키리의 모델을 불러옵니다.
			string resourceName = DataManager.Instance.ValkyrieDictionary.GetValueOrDefault(id).ResourceName;
			string modelPath = Path.Combine("Valkyries", resourceName, resourceName); // 예: Valkyries/Kiana_C6/Kiana_C6.prefab
			GameObject titleModel = Resources.Load<GameObject>(modelPath);
			
			// 불러온 모델을 생성합니다.
			valkyrieModel = Instantiate(titleModel, transform);
		}
		
		// [함수] 메인 화면의 발키리 모델을 파괴합니다.
		private void DestroyModel(GameObject valkyrieModel)
		{
			// 이미 발키리 모델이 생성되어 있다면, 그것을 파괴합니다.
			if (valkyrieModel)
			{
				Destroy(valkyrieModel);
			}
		}
		
		#endregion 함수(메서드)
	}
}
