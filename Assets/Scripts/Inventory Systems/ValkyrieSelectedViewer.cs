using DataSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
	/// <summary>
	/// [클래스] 발키리 목록 창에서, 한 발키리를 선택했을 때, 그 정보를 출력합니다.
	/// </summary>
	internal class ValkyrieSelectedViewer : MonoBehaviour
	{
		// [변수] 발키리 목록 관리자
		[SerializeField] private ValkyrieListViewer listViewer;
		
		// [변수] UI 목록
		[SerializeField] private TextMeshProUGUI characterNameText; // 캐릭터 이름
		[SerializeField] private TextMeshProUGUI suitNameText; // 슈트 이름
		[SerializeField] private Image typeImage; // 속성 사진
		[SerializeField] private Image weaponImage; // 무기 사진
		[SerializeField] private Image stigmataTopImage; // 성흔(상) 사진
		[SerializeField] private Image stigmataMiddleImage; // 성흔(중) 사진
		[SerializeField] private Image stigmataBottomImage; // 성흔(하) 사진
		
		// [유니티 생명 주기 함수] OnEnable()
		private void OnEnable()
		{
			listViewer.ValkyrieSelected += UpdateUI; // 이벤트를 등록합니다.
		}
		
		// [유니티 생명 주기 함수] OnDisable()
		private void OnDisable()
		{
			listViewer.ValkyrieSelected -= UpdateUI; // 이벤트를 해제합니다.
		}
		
		// [콜백 함수] UI를 갱신합니다.
		private void UpdateUI(ValkyrieData valkyrieData)
		{
			characterNameText.SetText(valkyrieData.CharacterName);
			suitNameText.SetText(valkyrieData.SuitName);
			
			// 무기, 성흔에 대한 것을 포기하는 것이 더 좋을지도...
			// typeImage.sprite = valkyrieData.Type;
			// weaponImage.sprite = valkyrieData.WeaponID;
			//
			// string weaponResourceName = DataManager.Instance.WeaponDictionary.GetValueOrDefault(valkyrieData.WeaponID).ResourceName;
			// string weaponSpritePath = Path.Combine("Weapon", );
			// Resources.Load<Sprite>();
		}
	}
}
