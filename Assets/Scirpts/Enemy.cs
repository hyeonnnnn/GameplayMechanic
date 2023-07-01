using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    private Rigidbody enemyRB;
    private GameObject player; 

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player"); // 플레이어에 대한 레퍼런스 확보
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized; // 플레이어 위치 - 적 위치
                                                                                             // .normalized : 항상 같은 속도 (플레이어가 멀어짐에 따라 더욱 큰 값을 곱하는 것 방지)
        enemyRB.AddForce(lookDirection * speed); // 적이 플레이어를 향해 이동
        
        if(transform.position.y < -10) // 떨어지면 오브젝트 파괴
        {
            Destroy(gameObject);
        }
    }
}
