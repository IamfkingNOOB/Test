using System;
using MonsterSystem;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
	internal MonsterStatusController CurrentMonster;
	
	internal event Action<MonsterStatusController> MonsterTargeted;
}