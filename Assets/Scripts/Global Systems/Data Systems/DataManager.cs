using System.Collections.Generic;
using EntityDatabase;
using Frameworks.Singleton;

namespace DataSystem
{
	/// <summary>
	/// [클래스] 외부 파일로부터 데이터를 읽어, 저장하고 관리합니다.
	/// </summary>
	internal class DataManager : Singleton<DataManager>
	{
		// [변수] 발키리의 사전
		private Dictionary<int, ValkyrieDataBase> _valkyrieDictionary;
		
		// [함수] 발키리의 사전에서 원하는 값을 ID로 접근하여 가져옵니다. 
		internal ValkyrieDataBase GetValkyrieDataByID(int id) => _valkyrieDictionary.GetValueOrDefault(id, _valkyrieDictionary[0]);
	}
}
