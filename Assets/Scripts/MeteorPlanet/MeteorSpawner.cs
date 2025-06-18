using System.Collections;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour , EnemyInterface
{
    [Header("스폰 설정")]
    public GameObject meteorPrefab;
    public float spawnInterval = 3f;
    public float minAngle = -30f; // 대각선 낙하 각도 범위
    public float maxAngle = 30f;

    private Coroutine spawnCoroutine;

    private void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Spawn();
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
            Debug.Log("🌊 Meteor 코루틴 정지됨!");
        }
    }




    public void Spawn()
    {
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize;
        float camWidth = cam.aspect * camHeight;

        float x = Random.Range(-camWidth + 1f, camWidth - 1f);
        float y = cam.transform.position.y + camHeight + 1f;

        Vector2 spawnPos = new Vector2(x, y);
        GameObject meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);

        // 대각선 방향 랜덤 설정
        float angle = Random.Range(minAngle, maxAngle);
        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.down;

        // Rigidbody에 힘 적용
        Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.linearVelocity = direction.normalized * 5f;

        Debug.Log($"☄️ Meteor 생성 at {spawnPos}, angle: {angle}°");
    }





}
