using System.Collections.Generic;
using System.ComponentModel;

internal class MonsterBattleData : INotifyPropertyChanged
{
	// [변수] 체력(HP)
	private int _currentHealthPoint; // 현재 체력
	internal int CurrentHealthPoint
	{
		get => _currentHealthPoint;
		private set => SetField(ref _currentHealthPoint, value, nameof(CurrentHealthPoint));
	}
	internal int MaxHealthPoint { get; init; } // 최대 체력
	
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
}
