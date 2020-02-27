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
        [FoldoutGroup("The enemy groups in this wave.")]
        public EnemyGroup[] Groups;
    }

    /// <summary>
    /// The waves the sequence is composed of.
    /// </summary>
    [FoldoutGroup("The enemy waves in this sequence.")]
    public EnemyWave[] Waves;
}
