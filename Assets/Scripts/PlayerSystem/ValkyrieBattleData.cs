using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 발키리의 기본 데이터 중에서, 전투에 사용하는 데이터만을 모아 관리하는 클래스입니다.
	/// </summary>
	public class ValkyrieBattleData : INotifyPropertyChanged
	{
		// 현재 체력
		private int _currentHealthPoint;
		public int CurrentHealthPoint
		{
			get => _currentHealthPoint;
			private set => SetField(ref _currentHealthPoint, value, nameof(CurrentHealthPoint));
		}

		// 최대 체력
		private int MaxHealthPoint { get; init; }

		// 현재 기력
		private int _currentSkillPoint;
		public int CurrentSkillPoint
		{
			get => _currentSkillPoint;
			private set => SetField(ref _currentSkillPoint, value, nameof(CurrentSkillPoint));
		}

		// 최대 기력
		private int MaxSkillPoint { get; init; }

		public int AnimationCount = 0;
		
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
		
		// 체력을 회복하는 함수; 최대 체력을 초과하여 회복할 수 없다.
		public void HealHealthPoint(int heal) => CurrentHealthPoint = Mathf.Min(CurrentHealthPoint + heal, MaxHealthPoint); // 고정값
		public void HealHealthPoint(float percentage) => HealHealthPoint(Mathf.FloorToInt(MaxHealthPoint * percentage)); // 비율값

		// 체력을 잃는 함수; 0 미만으로 잃을 수 없다.
		public void DamageHealthPoint(int damage) => CurrentHealthPoint = Mathf.Max(CurrentHealthPoint - damage, 0); // 고정값
		public void DamageHealthPoint(float percentage) => DamageHealthPoint(Mathf.FloorToInt(MaxHealthPoint * percentage)); // 회복값

		// 기력을 회복하는 함수; 최대 기력을 초과하여 회복할 수 없다.
		public void HealSkillPoint(int sp) => CurrentSkillPoint = Mathf.Min(CurrentSkillPoint + sp, MaxSkillPoint); // 고정값
		public void HealSkillPoint(float percentage) => HealSkillPoint(Mathf.FloorToInt(MaxSkillPoint * percentage)); // 비율값

		// 기력을 소모하는 함수; 현재 기력이 소모량 이상이어야 소모할 수 있다.
		public bool ConsumeSkillPoint(int sp)
		{
			if (CurrentSkillPoint >= sp)
			{
				CurrentSkillPoint -= sp;
				return true;
			}
			return false;
		}
	}
}
