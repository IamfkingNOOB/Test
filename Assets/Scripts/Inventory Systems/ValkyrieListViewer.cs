using System;
using System.Collections.Generic;
using DataSystem;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
	internal class ValkyrieListViewer : MonoBehaviour
	{
		[SerializeField] private ScrollRect scrollRect; // 스크롤 뷰 (Scroll View)
		[SerializeField] private ValkyrieListItem itemPrefab; // 스크롤 뷰의 컨텐츠 영역에 들어갈 아이템의 프리팹

		private ValkyrieData _selectedValkyrie = default;

		public ValkyrieData SelectedValkyrie
		{
			set
			{
				if (_selectedValkyrie != value)
				{
					_selectedValkyrie = value;
					ValkyrieSelected?.Invoke(_selectedValkyrie);
				}
			}
		}

		internal event Action<ValkyrieData> ValkyrieSelected;
		
		// [유니티 생명 주기 함수] Awake()
		private void Awake()
		{
			InitListItem(); // 스크롤 뷰의 아이템을 초기화합니다.
		}
		
		// [함수] 스크롤 뷰의 아이템을 초기화합니다.
		private void InitListItem()
		{
			// DataManager로부터 발키리의 사전을 가져옵니다.
			Dictionary<int, ValkyrieData> valkyrieList = DataManager.Instance.ValkyrieDictionary;
			
			// 가져온 사전을 순회하면서,
			foreach (ValkyrieData valkyrieData in valkyrieList.Values)
			{
				ValkyrieListItem newItem = Instantiate(itemPrefab, scrollRect.content); // 스크롤 뷰의 컨텐츠 영역에 아이템을 생성하고,
				newItem.Init(this, valkyrieData); // 사전의 정보로 초기화합니다.
			}
		}
	}
}
