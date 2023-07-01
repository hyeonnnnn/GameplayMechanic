using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9.0f; // 적이 8각형 밖에 생성될 일X
    public int enemyCount;
    public int waveNum = 1; // 웨이브당 생성되는 적의 수

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNum); // 초기에 적 생성
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation); // 초기에 파워업 생성
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length; // 해당 스크립트를 가지고 있는 오브젝트를 씬에서 모두 찾음
                                                        // .Length 사용하여 배열을 정수로
        if(enemyCount == 0) // 적을 다 처치하면
        {
            waveNum++;
            SpawnEnemyWave(waveNum); // 웨이브가 늘어날수록 적의 수 += 1
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation); // 파워업 생성
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn) // n개의 적 생성
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosz = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosz);

        return randomPos; // 무작위 위치값 반환
    }
}
