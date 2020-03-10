using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemySequence Sequence;
    public EnemyTypePrefabs EnemyPrefabs;

    private bool doneSpawning;
    private float currentTime;
    private float waitTime;
    private int currentWave;
    private int currentGroup;
    private int currentEnemy;

    public void Start()
    {
        waitTime = 1f;
    }

    public void Update()
    {
        if (doneSpawning) return;

        currentTime += Time.deltaTime;
        if (currentTime > waitTime)
        {
            currentTime -= waitTime;
            if (TryGetNextEnemy(out var type))
            {
                SpawnEnemy(type);
            }
            else
            {
                doneSpawning = true;
            }
        }
    }

    private bool TryGetNextEnemy(out EnemyType type)
    {
        var wave = Sequence.Waves[currentWave];
        var group = wave.Groups[currentGroup];
        type = group.Type;
        if (currentEnemy < group.Count)
        {
            currentEnemy += 1;
            return true;
        }
        else
        {
            // Reset enemy counter and get next group if it exists
            currentEnemy = 0;
            currentGroup += 1;
            if (currentGroup < wave.Groups.Length)
            {
                group = wave.Groups[currentGroup];
                type = group.Type;
                currentEnemy += 1;
                return true;
            }
            else
            {
                // Otherwise reset group counter and get next wave if it exists
                currentGroup = 0;
                currentWave += 1;
                if (currentWave < Sequence.Waves.Length)
                {
                    wave = Sequence.Waves[currentWave];
                    group = wave.Groups[currentGroup];
                    type = group.Type;
                    currentEnemy += 1;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    private void SpawnEnemy(EnemyType type)
    {
        var prefab = EnemyPrefabs[type];
        var spawn = GameObject.FindGameObjectWithTag("Spawn");
        Instantiate(prefab, spawn.transform.position, Quaternion.identity);
    }


}
