using System;
using System.Collections;
using UnityEngine;

public class SunSpawner : MonoBehaviour , EnemyInterface
{
    public GameObject sunPrefab; // 생성할 공 프리팹
    public float spawnInterval = 10f; // 공이 생성되는 간격 (초)
    public float spawnRadius = 5f; // 스폰 반경
    public float collisionCheckRadius = 0.5f; // 겹침 확인용 반경
    public LayerMask collisionLayerMask;     // 충돌 체크할 레이어


    private Coroutine spawnCoroutine;

    private void Awake()
    {
        if (gameObject.name.Contains("Clone"))
        { 
            Destroy(this); // 이 스크립트만 제거
            return;
        }
    }


    private void Start()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnSun());
        }
    }

    public void StopSpawning()
    {   
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
            Debug.Log("코루틴 정지 성공!");
        }
    }



    public IEnumerator SpawnSun()
    {

        Debug.Log("코루틴 시작");
        // 무한 루프가 아니라 한 번만 실행되도록 설정
        while (true)
        {
            // 일정 시간 대기
            yield return new WaitForSeconds(spawnInterval);
            // 공 생성
            Spawn();
        }
    }

    public void Spawn()
    {
        //// 중심에서 랜덤 방향 + 거리 (원형 범위 내)
        int maxAttempts = 30;
        for (int i = 0; i < maxAttempts; i++)
        {
            // 진짜 2D 전용 XY 평면에서 랜덤 위치
            Vector2 offset = UnityEngine.Random.insideUnitCircle * spawnRadius;
            Vector2 spawnPos = (Vector2)transform.position + offset;

            // 주변에 다른 충돌체가 없다면 생성
            if (!Physics2D.OverlapCircle(spawnPos, collisionCheckRadius, collisionLayerMask))
            {
                Instantiate(sunPrefab, spawnPos, Quaternion.identity);
                Debug.Log($"🌞 Sun 생성됨 at {spawnPos}");
                return;
            }
        }
        Debug.LogWarning("⚠️ 적절한 위치를 찾지 못해 Sun 생성 실패");
    }
}
