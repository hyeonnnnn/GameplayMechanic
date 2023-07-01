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
        focalPoint = GameObject.Find("Focal Point"); // ���� ������Ʈ�� ���� �ֱ� ������ GameObject.Find() ���
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput); // �÷��̾ ī�޶� �ٶ󺸴� �������� �̵�

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0); // �ε������Ͱ� �÷��̾ ���󰡵���
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup")) // Powerup�� �浹 ��
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true); // ��Ȱ��ȭ -> Ȱ��ȭ
            Destroy(other.gameObject);
            StartCoroutine(PowerupcountdownRoutine()); // �ڷ�ƾ ����
        }
    }

    IEnumerator PowerupcountdownRoutine()
    {
        yield return new WaitForSeconds(7); // 7�ʰ� ��ٷȴٰ� Ư�� ���� ����
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false); // Ȱ��ȭ -> ��Ȱ��ȭ
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position; // ���� �÷��̾�κ��� ƨ�� ���� ���� ���
                                                                                                   // �� ��ġ - �÷��̾� ��ġ
            enemyRigidbody.AddForce(awayFromPlayer * poweupStrength, ForceMode.Impulse);

            Debug.Log("Collided with: " + collision.gameObject.name + " with powerup set to " + hasPowerup);

        }
    }
}
