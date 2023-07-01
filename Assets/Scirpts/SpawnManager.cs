using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9.0f; // ���� 8���� �ۿ� ������ ��X
    public int enemyCount;
    public int waveNum = 1; // ���̺�� �����Ǵ� ���� ��

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNum); // �ʱ⿡ �� ����
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation); // �ʱ⿡ �Ŀ��� ����
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length; // �ش� ��ũ��Ʈ�� ������ �ִ� ������Ʈ�� ������ ��� ã��
                                                        // .Length ����Ͽ� �迭�� ������
        if(enemyCount == 0) // ���� �� óġ�ϸ�
        {
            waveNum++;
            SpawnEnemyWave(waveNum); // ���̺갡 �þ���� ���� �� += 1
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation); // �Ŀ��� ����
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn) // n���� �� ����
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

        return randomPos; // ������ ��ġ�� ��ȯ
    }
}
