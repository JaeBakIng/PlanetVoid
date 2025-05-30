using System;
using System.Collections;
using UnityEngine;

public class SunSpawner : MonoBehaviour
{
    public GameObject sunPrefab; // 생성할 공 프리팹
    public float spawnInterval = 10f; // 공이 생성되는 간격 (초)

    


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

    private void Spawn()
    {
        // 공을 생성할 위치 (여기서는 스폰어 위치에서 약간 위쪽)
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);

        // 공을 생성
        Instantiate(sunPrefab, spawnPosition, Quaternion.identity);
    }
    
    
}
