using System.IO;
using DataSystem;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
	internal class ValkyrieListItem : MonoBehaviour
	{
		[SerializeField] private Image portraitImage;

		private ValkyrieListViewer _listViewer;
		private ValkyrieData _valkyrieData;
		
		public void Init(ValkyrieListViewer listViewer, ValkyrieData valkyrieData)
		{
			_listViewer = listViewer;
			_valkyrieData = valkyrieData;
			
			string portraitPath = Path.Combine("Valkyrie", valkyrieData.ResourceName, valkyrieData.ResourceName, "_Portrait");
			portraitImage.sprite = Resources.Load<Sprite>(portraitPath);
		}

		public void OnClick()
		{
			_listViewer.SelectedValkyrie = _valkyrieData;
		}
	}
}