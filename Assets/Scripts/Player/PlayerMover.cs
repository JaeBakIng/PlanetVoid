using System;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{

    [SerializeField] 
    private SunSpawner spawner;

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
        if (collision.gameObject.CompareTag("sun") || collision.gameObject.CompareTag("WaveOb") || collision.gameObject.CompareTag("Meteor"))
        {
            // 1. Spawner 중지
            GameObject.Find("Sun")?.GetComponent<SunSpawner>()?.StopSpawning();
            GameObject.Find("WaveSpawner")?.GetComponent<WaveObPlanetSpawner>()?.StopSpawning();
            GameObject.Find("MeteorSpawner")?.GetComponent<MeteorSpawner>()?.StopSpawning();


            // 2. 충돌한 WaveOb 본인에게도 중력 적용
            if (collision.gameObject.CompareTag("WaveOb"))
            {
                Rigidbody2D wrb = collision.gameObject.GetComponent<Rigidbody2D>();
                Collider2D wcl = collision.gameObject.GetComponent<Collider2D>();

                if (wrb != null)
                {
                    wrb.gravityScale = 2f;
                    wrb.linearVelocity = Vector2.zero;
                    wrb.angularVelocity = 0f;
                }

                if (wcl != null)
                    wcl.isTrigger = true;
            }

            // 3. 기존 sun + wave 오브젝트 중력 적용
            foreach (GameObject sun in GameObject.FindGameObjectsWithTag("sun"))
            {
                Rigidbody2D rb = sun.GetComponent<Rigidbody2D>();
                Collider2D cl = sun.GetComponent<Collider2D>();
                if (rb != null) rb.gravityScale = 2f;
                if (cl != null) cl.isTrigger = true;
            }


            foreach (GameObject wave in GameObject.FindGameObjectsWithTag("WaveOb"))
            {
                WaveObPlanet planetScript = wave.GetComponent<WaveObPlanet>();
                if (planetScript != null)
                {
                    planetScript.ApplyGravityAndStop();  // ✅ 내부에서 gravityScale = 2f + isStopped = true 처리
                }

                Rigidbody2D wrb = wave.GetComponent<Rigidbody2D>();
                Collider2D wcl = wave.GetComponent<Collider2D>();
                if (wrb != null) wrb.gravityScale = 2f;
                if (wcl != null) wcl.isTrigger = true;
            }

            foreach (GameObject meteor in GameObject.FindGameObjectsWithTag("Meteor"))
            {
                Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
                Collider2D cl = meteor.GetComponent<Collider2D>();
                if (rb != null) rb.gravityScale = 2f;
                if (cl != null) cl.isTrigger = true;
            }



            // 4. 플레이어 정지
            characterRb.linearVelocity = Vector2.zero;
            characterRb.gravityScale = 2f;
            characterCl.isTrigger = true;
            this.enabled = false;

            Debug.Log("💥 충돌: WaveOb 중력 적용 및 전체 정지 완료");
        }
    }

}
