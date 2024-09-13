using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 발키리의 기본 데이터 중에서, 전투에 사용하는 데이터만을 모아 관리하는 클래스입니다.
	/// </summary>
	internal class ValkyrieBattleData : INotifyPropertyChanged
	{
		// [변수] 플레이어의 컨트롤러로, 코루틴 등 MonoBehaviour의 속성을 사용하기 위한 참조입니다.
		private readonly PlayerControllerBase _controller;
		
		#region 변수(필드)
		
		// [변수] 체력(HP)
		private int _currentHealthPoint; // 현재 체력
		public int CurrentHealthPoint
		{
			get => _currentHealthPoint;
			private set => SetField(ref _currentHealthPoint, value, nameof(CurrentHealthPoint));
		}
		public int MaxHealthPoint { get; init; } // 최대 체력

		// [변수] 기력(SP)
		private int _currentSkillPoint; // 현재 기력
		public int CurrentSkillPoint
		{
			get => _currentSkillPoint;
			private set => SetField(ref _currentSkillPoint, value, nameof(CurrentSkillPoint));
		}
		public int MaxSkillPoint { get; init; } // 최대 기력

		// [변수] 필살기(Ultimate)
		public int UltimateCost { get; init; } // 기력 소모량
		private float _currentUltimateCoolTime; // 남은 재사용 대기 시간
		public float CurrentUltimateCoolTime
		{
			get => _currentUltimateCoolTime;
			private set => SetField(ref _currentUltimateCoolTime, value, nameof(CurrentUltimateCoolTime));
		}
		public int MaxUltimateCoolTime { get; init; } // 기본 재사용 대기 시간

		#endregion 변수(필드)
		
		#region 함수(메서드)
		
		// [생성자] 컨트롤러의 참조를 받아 저장합니다.
		public ValkyrieBattleData(PlayerControllerBase controller)
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
		public void HealHealthPoint(int heal) => CurrentHealthPoint = Mathf.Min(CurrentHealthPoint + heal, MaxHealthPoint); // 고정값
		public void HealHealthPoint(float percentage) => HealHealthPoint(Mathf.FloorToInt(MaxHealthPoint * percentage)); // 비율값

		// [함수] 체력을 잃습니다. 0 미만으로 잃을 수 없습니다.
		public void DamageHealthPoint(int damage) => CurrentHealthPoint = Mathf.Max(CurrentHealthPoint - damage, 0); // 고정값
		public void DamageHealthPoint(float percentage) => DamageHealthPoint(Mathf.FloorToInt(MaxHealthPoint * percentage)); // 회복값

		// [함수] 기력을 회복합니다. 최대 기력을 초과하여 회복할 수 없습니다.
		public void ChargeSkillPoint(int sp) => CurrentSkillPoint = Mathf.Min(CurrentSkillPoint + sp, MaxSkillPoint); // 고정값
		public void ChargeSkillPoint(float percentage) => ChargeSkillPoint(Mathf.FloorToInt(MaxSkillPoint * percentage)); // 비율값

		// [함수] 필살기를 발동합니다. 현재 기력이 소모량 이상이고, 재사용 대기 시간이 아니어야만 발동할 수 있습니다.
		public bool UseUltimate()
		{
			bool usable = _currentSkillPoint >= UltimateCost && _currentUltimateCoolTime <= 0.0f;
			
			if (usable)
			{
				CurrentSkillPoint -= UltimateCost; // 필살기의 기력 소모량만큼 기력을 소모하고,
				
				// 필살기의 재사용 대기 시간을 적용합니다.
				CurrentUltimateCoolTime = MaxUltimateCoolTime;
				_controller.StartCoroutine(StartUltimateCoolTime());
				
				return true;
			}
			return false;
		}

		// [코루틴] 필살기의 재사용 대기 시간을 적용합니다.
		private IEnumerator StartUltimateCoolTime()
		{
			while (_currentUltimateCoolTime > 0.0f) // 재사용 대기 시간이 0초 이하가 아니라면,
			{
				CurrentUltimateCoolTime -= Time.deltaTime; // 시간이 흐른 만큼 재사용 대기 시간을 감소시킵니다.
				yield return null;
			}

			CurrentUltimateCoolTime = 0.0f; // 0초 이하가 되었다면, 정확히 0초로 설정하고 코루틴을 종료합니다.
		}
		
		#endregion 함수(메서드)
	}
}
