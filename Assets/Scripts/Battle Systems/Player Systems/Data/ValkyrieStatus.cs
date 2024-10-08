using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 발키리의 기본 데이터 중에서, 전투에 사용하는 데이터만을 모아 관리하는 클래스입니다.
	/// </summary>
	internal class PlayerStatus : INotifyPropertyChanged
	{
		// [변수] 플레이어의 컨트롤러로, 코루틴 등 MonoBehaviour의 속성을 사용하기 위한 참조입니다.
		private readonly PlayerStatusController _controller;
		
		#region 변수(필드)
		
		// [변수] 체력(HP)
		private int _currentHealthPoint; // 현재 체력
		internal int CurrentHealthPoint
		{
			get => _currentHealthPoint;
			private set => SetField(ref _currentHealthPoint, value, nameof(CurrentHealthPoint));
		}
		internal int MaxHealthPoint { get; init; } // 최대 체력

		// [변수] 기력(SP)
		private int _currentSkillPoint; // 현재 기력
		internal int CurrentSkillPoint
		{
			get => _currentSkillPoint;
			private set => SetField(ref _currentSkillPoint, value, nameof(CurrentSkillPoint));
		}
		internal int MaxSkillPoint { get; init; } // 최대 기력
		
		// [변수] 회피 스킬(Evade Skill)
		private float _currentEvadeSkillCoolTime; // 남은 재사용 대기 시간
		internal float CurrentEvadeSkillCoolTime
		{
			get => _currentEvadeSkillCoolTime;
			private set => SetField(ref _currentEvadeSkillCoolTime, value, nameof(CurrentEvadeSkillCoolTime));
		}
		internal float MaxEvadeSkillCoolTime { get; init; } // 기본 재사용 대기 시간

		// [변수] 필살기(Ultimate)
		internal int UltimateCost { get; init; } // 기력 소모량
		private float _currentUltimateCoolTime; // 남은 재사용 대기 시간
		internal float CurrentUltimateCoolTime
		{
			get => _currentUltimateCoolTime;
			private set => SetField(ref _currentUltimateCoolTime, value, nameof(CurrentUltimateCoolTime));
		}
		internal float MaxUltimateCoolTime { get; init; } // 기본 재사용 대기 시간

		#endregion 변수(필드)
		
		#region 함수(메서드)
		
		// [생성자] 컨트롤러의 참조를 받아 저장합니다.
		internal PlayerStatus(PlayerStatusController controller)
		{
			_controller = controller;
		}

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
		
		// [함수] 체력을 회복합니다. 최대 체력을 초과하여 회복할 수 없습니다.
		internal void HealHealthPoint(int heal) => CurrentHealthPoint = Mathf.Min(CurrentHealthPoint + heal, MaxHealthPoint); // 고정값
		internal void HealHealthPoint(float percentage) => HealHealthPoint(Mathf.FloorToInt(MaxHealthPoint * percentage)); // 비율값

		// [함수] 체력을 잃습니다. 0 미만으로 잃을 수 없습니다.
		internal void DamageHealthPoint(int damage) => CurrentHealthPoint = Mathf.Max(CurrentHealthPoint - damage, 0); // 고정값
		internal void DamageHealthPoint(float percentage) => DamageHealthPoint(Mathf.FloorToInt(MaxHealthPoint * percentage)); // 회복값

		// [함수] 기력을 회복합니다. 최대 기력을 초과하여 회복할 수 없습니다.
		internal void ChargeSkillPoint(int sp) => CurrentSkillPoint = Mathf.Min(CurrentSkillPoint + sp, MaxSkillPoint); // 고정값
		internal void ChargeSkillPoint(float percentage) => ChargeSkillPoint(Mathf.FloorToInt(MaxSkillPoint * percentage)); // 비율값

		// [함수] 회피 스킬을 발동합니다.
		internal bool ActivateEvadeSkill()
		{
			bool usable = _currentEvadeSkillCoolTime <= 0.0f; // 재사용 대기 시간이 아니어야만 발동할 수 있습니다.

			if (usable)
			{
				_controller.StartCoroutine(StartEvadeSkillCoolTime()); // 재사용 대기 시간을 적용하는 코루틴을 시작합니다.
			}
			
			return usable;
		}
		
		// [함수] 필살기를 발동합니다. 
		internal bool ActivateUltimate()
		{
			bool usable = _currentSkillPoint >= UltimateCost && _currentUltimateCoolTime <= 0.0f;
			
			// 현재 기력이 소모량 이상이고, 재사용 대기 시간이 아니어야만 발동할 수 있습니다.
			if (usable)
			{
				_controller.StartCoroutine(StartUltimateCoolTime()); // 재사용 대기 시간을 적용하는 코루틴을 시작합니다.
			}
			
			return usable;
		}

		// [코루틴] 회피 스킬의 재사용 대기 시간을 적용합니다.
		private IEnumerator StartEvadeSkillCoolTime()
		{
			CurrentUltimateCoolTime = MaxUltimateCoolTime; // 재사용 대기 시간을 적용합니다.
			
			while (CurrentEvadeSkillCoolTime > 0.0f) // 재사용 대기 시간이 0초 이하가 될 때까지,
			{
				CurrentEvadeSkillCoolTime -= Time.deltaTime; // 시간이 흐른 만큼 재사용 대기 시간을 감소시킵니다.
				yield return null;
			}

			CurrentEvadeSkillCoolTime = 0.0f; // 0초 이하가 되었다면, 정확히 0초로 설정하고 코루틴을 종료합니다.
		}

		// [코루틴] 필살기의 재사용 대기 시간을 적용합니다.
		private IEnumerator StartUltimateCoolTime()
		{
			CurrentEvadeSkillCoolTime = MaxEvadeSkillCoolTime; // 재사용 대기 시간을 적용하고,
			CurrentSkillPoint -= UltimateCost; // 필살기의 기력 소모량만큼 기력을 소모합니다.
			
			while (CurrentUltimateCoolTime > 0.0f) // 재사용 대기 시간이 0초 이하가 될 때까지,
			{
				CurrentUltimateCoolTime -= Time.deltaTime; // 시간이 흐른 만큼 재사용 대기 시간을 감소시킵니다.
				yield return null;
			}

			CurrentUltimateCoolTime = 0.0f; // 0초 이하가 되었다면, 정확히 0초로 설정하고 코루틴을 종료합니다.
		}
		
		#endregion 함수(메서드)
	}
}
