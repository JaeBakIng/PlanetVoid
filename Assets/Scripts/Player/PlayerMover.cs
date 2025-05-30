using System;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{

    [SerializeField] private SunSpawner spawner;

    public float speed = 5f; // 이동 속도


    GameObject GameObject;

    private Rigidbody2D characterRb;
    private Collider2D characterCl;




    private void Start()
    {
        characterRb = GetComponent<Rigidbody2D>();
        characterCl = GetComponent<Collider2D>();



    }

    void LateUpdate()
    {
        float moveX = Input.GetAxis("Horizontal"); // 좌우 이동 (A, D 또는 ← →)
        float moveY = Input.GetAxis("Vertical"); // 상하 이동 (W, S 또는 ↑ ↓)

        Vector3 move = new Vector3(moveX, moveY, 0) * speed;
        //transform.Translate(move);

        characterRb.linearVelocity = move;


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("sun"))
        {

            GameObject spawnerObj = GameObject.Find("Sun");

            if (spawnerObj != null)
            {
                // SunSpawner 스크립트 컴포넌트 가져오기
                SunSpawner spawner = spawnerObj.GetComponent<SunSpawner>();
                if (spawner != null)
                {
                    spawner.StopSpawning(); // StopSpawning() 호출
                    Debug.Log("공 생성 멈춤");
                }
                else
                {
                    Debug.LogError("SunSpawner 컴포넌트를 찾지 못했습니다.");
                }
            }
            else
            {
                Debug.LogError("SunSpawner 오브젝트를 찾지 못했습니다.");
            }

            characterRb.gravityScale = 2f;






            /// sun 오브젝트에 중력 발동
            Debug.Log("적 공에 부딪힘! 중력 발동");
            GameObject[] allBalls = GameObject.FindGameObjectsWithTag("sun");
            foreach (GameObject ball in allBalls)
            {
                Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
                Collider2D cl = ball.GetComponent<Collider2D>();
                if (rb != null)
                {
                    rb.gravityScale = 2f; // 떨어지게 만듦
                    cl.isTrigger = true;
                }
            }

            characterRb.linearVelocity = Vector2.zero;
            characterRb.gravityScale = 2f;
            characterCl.isTrigger = true;
            this.enabled = false;  // 움직임 스크립트 비활성화

        }

        
    }

    
}
