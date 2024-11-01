using DataSystem;
using TMPro;
using UnityEngine;

namespace InventorySystem
{
	/// <summary>
	/// [클래스] 발키리 목록 창에서, 한 발키리를 선택했을 때, 그 정보를 출력합니다.
	/// </summary>
	internal class ValkyrieSelectedViewer : MonoBehaviour
	{
		// [변수] 발키리 목록 관리자
		[SerializeField] private ValkyrieListViewer listViewer;
		
		// [변수] 정보 UI 목록
		[SerializeField] private TextMeshProUGUI characterNameText; // 캐릭터 이름
		[SerializeField] private TextMeshProUGUI suitNameText; // 슈트 이름
		
		[SerializeField] private TextMeshProUGUI hpText; // 체력
		[SerializeField] private TextMeshProUGUI spText; // 기력
		[SerializeField] private TextMeshProUGUI atkText; // 공격력
		[SerializeField] private TextMeshProUGUI defText; // 방어력
		[SerializeField] private TextMeshProUGUI crtText; // 회심
		
		// [유니티 생명 주기 함수] OnEnable()
		private void OnEnable()
		{
			listViewer.ValkyrieSelected += OnUpdateUI; // 이벤트를 등록합니다.
		}
		
		// [유니티 생명 주기 함수] OnDisable()
		private void OnDisable()
		{
			listViewer.ValkyrieSelected -= OnUpdateUI; // 이벤트를 해제합니다.
		}
		
		// [콜백 함수] UI를 갱신합니다.
		private void OnUpdateUI(ValkyrieData valkyrieData)
		{
			characterNameText.SetText(valkyrieData.CharacterName);
			suitNameText.SetText(valkyrieData.SuitName);
			
			hpText.SetText($"체력: {valkyrieData.HP}");
			spText.SetText($"기력: {valkyrieData.SP}");
			atkText.SetText($"공격력: {valkyrieData.ATK}");
			defText.SetText($"방어력: {valkyrieData.DEF}");
			crtText.SetText($"회심: {valkyrieData.CRT}");
		}
	}
}
