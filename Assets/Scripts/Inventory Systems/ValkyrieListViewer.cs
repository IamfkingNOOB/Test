using System;
using System.Collections.Generic;
using DataSystem;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
	/// <summary>
	/// [클래스] 발키리 목록 창에서, 목록을 생성합니다.
	/// </summary>
	internal class ValkyrieListViewer : MonoBehaviour
	{
		// [변수] 스크롤 뷰(Scroll View) 및 아이템
		[SerializeField] private ScrollRect scrollRect;
		[SerializeField] private ValkyrieListItem itemPrefab; // 스크롤 뷰의 컨텐츠 영역에 들어갈 아이템의 프리팹
		
		// [변수] 버튼 UI 목록
		[SerializeField] private Button selectAsTitleButton; // 메인 화면의 발키리로 선택하는 버튼
		[SerializeField] private Button selectAsPlayerButton; // 출격할 발키리로 선택하는 버튼
		
		// [변수 / 프로퍼티] 선택된 발키리(아이템)
		private ValkyrieData _selectedValkyrie = default;
		internal ValkyrieData SelectedValkyrie
		{
			get => _selectedValkyrie;
			set
			{
				if (_selectedValkyrie != value)
				{
					_selectedValkyrie = value;
					ValkyrieSelected?.Invoke(_selectedValkyrie);
				}
			}
		}
		
		// [이벤트] 발키리(아이템)가 선택되었을 때 호출됩니다.
		internal event Action<ValkyrieData> ValkyrieSelected;
		
		// [유니티 생명 주기 함수] Awake()
		private void Awake()
		{
			InitListItem(); // 스크롤 뷰의 아이템을 초기화합니다.
		}
		
		// [함수] 스크롤 뷰의 아이템을 초기화합니다.
		private void InitListItem()
		{
			// DataManager로부터 발키리 사전을 가져옵니다.
			Dictionary<int, ValkyrieData> valkyrieList = DataManager.Instance.ValkyrieDictionary;
			
			// 가져온 사전을 순회하면서,
			foreach (ValkyrieData valkyrieData in valkyrieList.Values)
			{
				ValkyrieListItem newItem = Instantiate(itemPrefab, scrollRect.content); // 스크롤 뷰의 컨텐츠 영역에 아이템을 생성하고,
				newItem.Init(this, valkyrieData); // 사전의 정보로 초기화합니다.
			}
		}
		
		// [함수] 상황에 따라 적절한 버튼을 활성화 / 비활성화합니다.
		internal void SelectButton(string type)
		{
			switch (type)
			{
				case "Title":
					selectAsPlayerButton.gameObject.SetActive(false);
					selectAsTitleButton.gameObject.SetActive(true);
					break;
				case "Player":
					selectAsTitleButton.gameObject.SetActive(false);
					selectAsPlayerButton.gameObject.SetActive(true);
					break;
			}
		}

		// [버튼 이벤트] OnClick(): selectAsTitleButton
		internal void OnClickTitleButton()
		{
			PlayerPrefs.SetInt("TitleValkyrieID", _selectedValkyrie.ID);
		}

		// [버튼 이벤트] OnClick(): selectAsPlayerButton
		internal void OnClickPlayerButton()
		{
			
		}
	}
}
