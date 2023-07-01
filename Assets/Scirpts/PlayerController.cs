using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private GameObject focalPoint;
    private float poweupStrength = 15.0f;
    public float speed = 5.0f;
    public bool hasPowerup = false;
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point"); // 게임 오브젝트가 씬에 있기 때문에 GameObject.Find() 사용
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput); // 플레이어가 카메라가 바라보는 방향으로 이동

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0); // 인디케이터가 플레이어를 따라가도록
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup")) // Powerup과 충돌 시
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true); // 비활성화 -> 활성화
            Destroy(other.gameObject);
            StartCoroutine(PowerupcountdownRoutine()); // 코루틴 시작
        }
    }

    IEnumerator PowerupcountdownRoutine()
    {
        yield return new WaitForSeconds(7); // 7초간 기다렸다가 특정 동작 수행
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false); // 활성화 -> 비활성화
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position; // 적이 플레이어로부터 튕겨 나갈 방향 계산
                                                                                                   // 적 위치 - 플레이어 위치
            enemyRigidbody.AddForce(awayFromPlayer * poweupStrength, ForceMode.Impulse);

            Debug.Log("Collided with: " + collision.gameObject.name + " with powerup set to " + hasPowerup);

        }
    }
}
