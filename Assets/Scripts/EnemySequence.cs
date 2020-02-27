using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypes
{
    Normal,
    Fast,
    Tanky,

}
public class EnemyWave
{
    public float Time;
    public EnemyTypes[] Types;
}
[CreateAssetMenu(fileName = "EnemySequence", menuName = "SomethingSpecific/EnemySequence", order = 1)]
public class EnemySequence : ScriptableObject
{
    public int Waves;
    public EnemyWave Enemy;
}
