using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 스탯(데이터)을 표현합니다.
	/// </summary>
	internal class MonsterStatus : INotifyPropertyChanged
	{
		#region 변수(필드)
		
		// [변수] 체력(HP)
		private int _currentHealthPoint; // 현재 체력
		internal int CurrentHealthPoint
		{
			get => _currentHealthPoint;
			private set => SetField(ref _currentHealthPoint, value, nameof(CurrentHealthPoint));
		}
		internal int MaxHealthPoint { get; init; } // 최대 체력

		private int _atk;
		internal int ATK
		{
			get => _atk;
			private set => SetField(ref _atk, value, nameof(ATK));
		}
		
		// [변수] 공격 범위
		internal float AttackRange { get; init; }
		
		// [변수] 추적 범위
		internal float ChaseRange { get; init; }
		
		#endregion 변수(필드)
		
		#region 함수(메서드)
	
		#region 프로퍼티 변경 이벤트
		
		// [이벤트] 프로퍼티 변경 이벤트
		public event PropertyChangedEventHandler PropertyChanged;

		// [함수] 프로퍼티 변경 이벤트를 호출합니다.
		private void InvokePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	
		// [함수] 프로퍼티의 값을 변경하면서, 프로퍼티 변경 이벤트를 호출합니다.
		private void SetField<T>(ref T field, T value, string propertyName)
		{
			// 값이 실제로 변경되었을 때만 이벤트를 호출합니다.
			if (!EqualityComparer<T>.Default.Equals(field, value))
			{
				field = value; // 프로퍼티의 값을 변경합니다.
				InvokePropertyChanged(propertyName); // 이벤트를 호출합니다.
			}
		}
		
		#endregion 프로퍼티 변경 이벤트
		
		// [함수] 모든 데이터를 원복합니다.
		internal void Reset()
		{
			_currentHealthPoint = MaxHealthPoint; // 현재 체력을 최대 체력으로 전부 회복합니다.
		}
		
		// [함수] 체력을 회복합니다. 최대 체력을 초과하여 회복할 수 없습니다.
		internal void HealHealthPoint(int heal) => CurrentHealthPoint = Mathf.Min(CurrentHealthPoint + heal, MaxHealthPoint);

		// [함수] 체력을 잃습니다. 0 미만으로 잃을 수 없습니다.
		internal void DamageHealthPoint(int damage) => CurrentHealthPoint = Mathf.Max(CurrentHealthPoint - damage, 0);
		
		#endregion 함수(메서드)
	}
}
