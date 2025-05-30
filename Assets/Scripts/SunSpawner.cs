using System;
using System.Collections;
using UnityEngine;

public class SunSpawner : MonoBehaviour
{
    public GameObject sunPrefab; // ������ �� ������
    public float spawnInterval = 10f; // ���� �����Ǵ� ���� (��)

    


    private Coroutine spawnCoroutine;

    private void Awake()
    {
        if (gameObject.name.Contains("Clone"))
        { 
            Destroy(this); // �� ��ũ��Ʈ�� ����
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
            Debug.Log("�ڷ�ƾ ���� ����!");
        }
    }



    public IEnumerator SpawnSun()
    {

        Debug.Log("�ڷ�ƾ ����");
        // ���� ������ �ƴ϶� �� ���� ����ǵ��� ����
        while (true)
        {

            // ���� �ð� ���
            
          
            yield return new WaitForSeconds(spawnInterval);
            // �� ����
            Spawn();
        }
    }

    private void Spawn()
    {
        // ���� ������ ��ġ (���⼭�� ������ ��ġ���� �ణ ����)
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);

        // ���� ����
        Instantiate(sunPrefab, spawnPosition, Quaternion.identity);
    }
    
    
}
