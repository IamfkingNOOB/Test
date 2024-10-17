using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace MonsterSystem
{
	// [클래스] 몬스터의 정보(이름, 체력 등)를 관리합니다.
	[CreateAssetMenu(fileName = "New_MonsterStatus", menuName = "Monsters/Monster Status")]
	internal class MonsterStatus : ScriptableObject, INotifyPropertyChanged
	{
		#region 프로퍼티 변경 이벤트

		// [이벤트] 프로퍼티 변경 이벤트
		public event PropertyChangedEventHandler PropertyChanged;

		// [함수] 프로퍼티의 값을 변경하면서, 프로퍼티 변경 이벤트를 호출합니다.
		private void SetField<T>(ref T field, T value, string propertyName)
		{
			if (!EqualityComparer<T>.Default.Equals(field, value)) // 값이 바뀌었을 경우에만,
			{
				field = value; // 프로퍼티의 값을 바꾸고,
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // 이벤트를 호출합니다.
			}
		}
		
		#endregion 프로퍼티 변경 이벤트
		
		#region 정보(데이터)
		
		#region 이름

		[SerializeField] private string name; // 몬스터의 이름
		internal string Name => name;
		
		#endregion 이름
		
		#region 체력(HP)
		
		// [변수] 현재 체력
		private int _currentHP;
		internal int CurrentHP
		{
			get => _currentHP;
			private set => SetField(ref _currentHP, value, nameof(CurrentHP));
		}
		
		// [변수] 최대 체력
		[SerializeField] private int maxHP;
		internal int MaxHP
		{
			get => maxHP;
			private set => SetField(ref maxHP, value, nameof(MaxHP));
		}
		
		#endregion 체력(HP)
		
		#region 공격력(ATK)
		
		// [변수] 현재 공격력
		private int _currentATK;
		internal int CurrentATK
		{
			get => _currentATK;
			private set => SetField(ref _currentATK, value, nameof(CurrentATK));
		}
		
		// [변수] 원래 공격력
		[SerializeField] private int originalATK;
		internal int OriginalATK => originalATK;
		
		#endregion 공격력(ATK)
		
		#region 방어력(DEF)
		
		// [변수] 현재 방어력
		private int _currentDEF;
		internal int CurrentDEF
		{
			get => _currentDEF;
			private set => SetField(ref _currentDEF, value, nameof(CurrentDEF));
		}
		
		// [변수] 원래 방어력
		[SerializeField] private int originalDEF;
		internal int OriginalDEF => originalDEF;
		
		#endregion 방어력(DEF)
		
		#region 탐지 범위(Range)
		
		// [변수] 공격 탐지 범위
		[SerializeField] private float attackRange;
		internal float AttackRange => attackRange;
		
		// [변수] 추적 탐지 범위
		[SerializeField] private float chaseRange;
		internal float ChaseRange => chaseRange;
		
		#endregion 탐지 범위(Range)
		
		#endregion 정보(데이터)

		#region 데이터 변경 함수
		
		// [함수] 모든 정보를 기본 값으로 초기화합니다.
		internal void Reset()
		{
			// [속성(프로퍼티) = 변수(필드)] 형태로 대입합니다.
			CurrentHP = maxHP;
			CurrentATK = originalATK;
			CurrentDEF = originalDEF;
		}
		
		// [함수] 체력을 회복합니다. 최대치를 초과할 수 없습니다.
		internal void HealHP(int amount) => CurrentHP = Mathf.Min(_currentHP + amount, maxHP);
		
		// [함수] 체력을 잃습니다. 0 미만으로 감소할 수 없습니다.
		internal void DamageHP(int amount) => CurrentHP = Mathf.Max(_currentHP - amount, 0);
		
		// [함수] 공격력을 강화합니다.
		internal void IncreaseATK(int amount) => CurrentATK = _currentATK + amount;
		
		// [함수] 공격력을 약화합니다.
		internal void DecreaseATK(int amount) => CurrentATK = _currentATK - amount;
		
		// [함수] 방어력을 강화합니다.
		internal void IncreaseDEF(int amount) => CurrentDEF = _currentDEF + amount;
		
		// [함수] 방어력을 약화합니다.
		internal void DecreaseDEF(int amount) => CurrentDEF = _currentDEF - amount;
		
		#endregion 데이터 변경 함수
		
	}
}
