using System.Collections.Generic;
using Frameworks.Singleton;

namespace DataSystem
{
	/// <summary>
	/// [클래스] 외부 파일로부터 데이터를 읽어, 저장하고 관리합니다.
	/// </summary>
	internal class DataManager : Singleton<DataManager>
	{
		private readonly XMLParser _xmlParser = new XMLParser();
		
		// [변수] 발키리의 사전
		private Dictionary<int, ValkyrieData> _valkyrieDictionary;
		private Dictionary<int, WeaponData> _weaponDictionary;
		private Dictionary<int, StigmataData> _stigmataDictionary;

		internal Dictionary<int, ValkyrieData> ValkyrieDictionary
		{
			get
			{
				if (_valkyrieDictionary == null)
				{
					_valkyrieDictionary = _xmlParser.ParseValkyrieData("Valkyrie");
				}
				
				return _valkyrieDictionary;
			}
		}

		internal Dictionary<int, WeaponData> WeaponDictionary
		{
			get
			{
				if (_weaponDictionary == null)
				{
					_weaponDictionary = _xmlParser.ParseWeaponData("Weapon");
				}

				return _weaponDictionary;
			}
		}

		internal Dictionary<int, StigmataData> StigmataDictionary
		{
			get
			{
				if (_stigmataDictionary == null)
				{
					_stigmataDictionary = _xmlParser.ParseStigmataData("Stigmata");
				}

				return _stigmataDictionary;
			}
		}
	}
}
