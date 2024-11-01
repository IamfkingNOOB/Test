using System.IO;
using DataSystem;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
	/// <summary>
	/// [클래스] 발키리 목록 창에서, 목록 아이템의 속성을 설정합니다.
	/// </summary>
	internal class ValkyrieListItem : MonoBehaviour
	{
		// [변수] 이미지 UI
		[SerializeField] private Image portraitImage;

		// [변수] 참조 데이터
		private ValkyrieListViewer _listViewer; // 버튼 이벤트에서 데이터를 넘겨줄 클래스
		private ValkyrieData _valkyrieData; // 각 아이템이 가지는 발키리 정보
		
		// [함수] 정보를 초기화합니다.
		internal void Init(ValkyrieListViewer listViewer, ValkyrieData valkyrieData)
		{
			_listViewer = listViewer; // 참조 데이터를 저장합니다.
			_valkyrieData = valkyrieData;
			
			// 이미지를 받은 데이터로 갱신합니다. (경로: "Valkyrie/Sprites/Kiana_C6_Portrait.png)
			string portraitPath = Path.Combine("Valkyrie", "Sprites", $"{valkyrieData.ResourceName}_Portrait");
			portraitImage.sprite = Resources.Load<Sprite>(portraitPath);
		}

		// [버튼 이벤트] OnClick()
		internal void OnClick()
		{
			_listViewer.SelectedValkyrie = _valkyrieData;
		}
	}
}
