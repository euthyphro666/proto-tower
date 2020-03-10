using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Defines all the enemy types
/// </summary>
public enum EnemyType
{
    Normal,
    Fast,
    Tanky,
}
[Serializable]
public class EnemyTypePrefabs : UnitySerializedDictionary<EnemyType, GameObject> { }
public abstract class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField, HideInInspector]
    private List<TKey> keyData = new List<TKey>();

    [SerializeField, HideInInspector]
    private List<TValue> valueData = new List<TValue>();

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        this.Clear();
        for (int i = 0; i < this.keyData.Count && i < this.valueData.Count; i++)
        {
            this[this.keyData[i]] = this.valueData[i];
        }
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        this.keyData.Clear();
        this.valueData.Clear();

        foreach (var item in this)
        {
            this.keyData.Add(item.Key);
            this.valueData.Add(item.Value);
        }
    }
}

[CreateAssetMenu(fileName = "EnemySequence", menuName = "SomethingSpecific/EnemySequence", order = 1)]
public class EnemySequence : ScriptableObject
{


    /// <summary>
    /// Defines a single wave of enemies
    /// </summary>
    [Serializable]
    public class EnemyWave
    {
        /// <summary>
        /// Defines a group of enemies in a wave
        /// </summary>
        [Serializable]
        public class EnemyGroup
        {
            /// <summary>
            /// Number of enemies.
            /// </summary>
            [InfoBox("The number of enemies in this group.")]
            public int Count;
            /// <summary>
            /// The type of enemy.
            /// </summary>
            [InfoBox("The type of enemy this group contains.")]
            public EnemyType Type;
        }

        /// <summary>
        /// The amount of time the enemies should take to be deployed.
        /// </summary>
        public float Time;

        /// <summary>
        /// The groups that make up the enemy.
        /// </summary>
        [FoldoutGroup("Enemy Groups")]
        public EnemyGroup[] Groups;
    }

    /// <summary>
    /// The waves the sequence is composed of.
    /// </summary>
    [FoldoutGroup("Enemy Waves")]
    public EnemyWave[] Waves;
}
