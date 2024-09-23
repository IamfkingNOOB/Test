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
		
		// [변수] 메인 화면에 보여줄 발키리를 생성할 위치
		[SerializeField] private Transform modelTransform;
		
		// [변수] 생성한 발키리 모델의 참조
		private GameObject _createdModel;
		
		#endregion 변수(필드)
		
		#region 함수(메서드)
		
		// [유니티 생명 주기 함수] OnEnable()
		private void OnEnable()
		{
			SetModel(); // 발키리의 모델을 지정합니다.
		}
		
		// [함수] 발키리의 모델을 지정합니다.
		private void SetModel()
		{
			// 이미 발키리 모델이 생성되어 있다면, 그것을 삭제합니다.
			if (_createdModel) Destroy(_createdModel);
			
			// PlayerPrefs에 저장되어 있는, 메인 화면에 보여줄 발키리의 식별자(ID)를 가져옵니다.
			int id = PlayerPrefs.GetInt("Title_Valkyrie_ID", 0);
			
			// DataManager로부터 그 식별자에 해당하는 발키리의 모델을 가져와, 지정한 위치에 생성합니다.
			_createdModel = Instantiate(DataManager.Instance.GetValkyrieDataByID(id).PrefabModel, modelTransform);
		}
		
		#endregion 함수(메서드)
	}
}
