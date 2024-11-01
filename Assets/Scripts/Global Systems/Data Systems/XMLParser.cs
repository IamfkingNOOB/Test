using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

namespace DataSystem
{
	internal class XMLParser
	{
		// [변수] XML 파일의 경로 (Application.dataPath = Assets)
		private readonly string _rootPath = Path.Combine(Application.dataPath, "Databases", "XMLs");

		// [함수] 발키리의 정보를 담은 XML 파일을 분석하여 저장합니다.
		internal Dictionary<int, ValkyrieData> ParseValkyrieData(string fileName)
		{
			// 반환할 사전을 생성합니다.
			var valkyrieDictionary = new Dictionary<int, ValkyrieData>();
			
			// XML 파일을 불러와서, 'data' 태그 요소만을 고릅니다.
			XDocument xDocument = XDocument.Load($"{_rootPath}/{fileName}.xml");
			IEnumerable<XElement> xElements = xDocument.Descendants("data");
			
			// 요소를 순회하면서,
			foreach (XElement xElement in xElements)
			{
				ValkyrieData newValkyrie = new ValkyrieData // 발키리의 정보를 분석합니다.
				{
					ID = int.Parse(xElement.Attribute(nameof(newValkyrie.ID)).Value),
					CharacterName = xElement.Attribute(nameof(newValkyrie.CharacterName)).Value,
					SuitName = xElement.Attribute(nameof(newValkyrie.SuitName)).Value,
					Type = xElement.Attribute(nameof(newValkyrie.Type)).Value,
					HP = int.Parse(xElement.Attribute(nameof(newValkyrie.HP)).Value),
					SP = int.Parse(xElement.Attribute(nameof(newValkyrie.SP)).Value),
					ATK = int.Parse(xElement.Attribute(nameof(newValkyrie.ATK)).Value),
					DEF = int.Parse(xElement.Attribute(nameof(newValkyrie.DEF)).Value),
					CRT = int.Parse(xElement.Attribute(nameof(newValkyrie.CRT)).Value),
					WeaponID = int.Parse(xElement.Attribute(nameof(newValkyrie.WeaponID)).Value),
					StigmataTopID = int.Parse(xElement.Attribute(nameof(newValkyrie.StigmataTopID)).Value),
					StigmataMiddleID = int.Parse(xElement.Attribute(nameof(newValkyrie.StigmataMiddleID)).Value),
					StigmataBottomID = int.Parse(xElement.Attribute(nameof(newValkyrie.StigmataBottomID)).Value),
					ResourceName = xElement.Attribute(nameof(newValkyrie.ResourceName)).Value
				};
				
				valkyrieDictionary.Add(newValkyrie.ID, newValkyrie); // 분석한 정보를 사전에 추가합니다.
			}
			
			// 분석을 끝낸 사전을 반환합니다.
			return valkyrieDictionary;
		}
		
		// [함수] 무기의 정보를 담은 XML 파일을 분석하여 저장합니다.
		internal Dictionary<int, WeaponData> ParseWeaponData(string fileName)
		{
			// 반환할 사전을 생성합니다.
			var weaponDictionary = new Dictionary<int, WeaponData>();
			
			// XML 파일을 불러와서, 'data' 태그 요소만을 고릅니다.
			XDocument xDocument = XDocument.Load($"{_rootPath}/{fileName}.xml");
			IEnumerable<XElement> xElements = xDocument.Descendants("data");
			
			// 요소를 순회하면서,
			foreach (XElement xElement in xElements)
			{
				WeaponData newWeapon = new WeaponData // 무기의 정보를 분석합니다.
				{
					ID = int.Parse(xElement.Attribute(nameof(newWeapon.ID)).Value),
					ATK = int.Parse(xElement.Attribute(nameof(newWeapon.ATK)).Value),
					SP = int.Parse(xElement.Attribute(nameof(newWeapon.SP)).Value),
					ResourceName = xElement.Attribute(nameof(newWeapon.ResourceName)).Value
				};
				
				weaponDictionary.Add(newWeapon.ID, newWeapon); // 분석한 정보를 사전에 추가합니다.
			}
			
			// 분석을 끝낸 사전을 반환합니다.
			return weaponDictionary;
		}
		
		// [함수] 성흔의 정보를 담은 XML 파일을 분석하여 저장합니다.
		internal Dictionary<int, StigmataData> ParseStigmataData(string fileName)
		{
			// 반환할 사전을 생성합니다.
			var stigmataDictionary = new Dictionary<int, StigmataData>();
			
			// XML 파일을 불러와서, 'data' 태그 요소만을 고릅니다.
			XDocument xDocument = XDocument.Load($"{_rootPath}/{fileName}.xml");
			IEnumerable<XElement> xElements = xDocument.Descendants("data");
			
			// 요소를 순회하면서,
			foreach (XElement xElement in xElements)
			{
				StigmataData newStigmata = new StigmataData // 성흔의 정보를 분석합니다.
				{
					ID = int.Parse(xElement.Attribute(nameof(newStigmata.ID)).Value),
					HP = int.Parse(xElement.Attribute(nameof(newStigmata.HP)).Value),
					SP = int.Parse(xElement.Attribute(nameof(newStigmata.SP)).Value),
					ATK = int.Parse(xElement.Attribute(nameof(newStigmata.ATK)).Value),
					DEF = int.Parse(xElement.Attribute(nameof(newStigmata.DEF)).Value),
					CRT = int.Parse(xElement.Attribute(nameof(newStigmata.CRT)).Value),
					ResourceName = xElement.Attribute(nameof(newStigmata.ResourceName)).Value
				};
				
				stigmataDictionary.Add(newStigmata.ID, newStigmata); // 분석한 정보를 사전에 추가합니다.
			}
			
			// 분석을 끝낸 사전을 반환합니다.
			return stigmataDictionary;
		}
	}
}
