using System.Collections;
using UnityEngine;

public class WaveObPlanetSpawner : MonoBehaviour, EnemyInterface
{
    [Header("스폰 설정")]
    public GameObject waveObPlanetPrefab;
    public float spawnInterval = 4f;

    private Coroutine spawnCoroutine;
    private bool isClone = false;

    private void Awake()
    {
        if (gameObject.name.Contains("Clone"))
        {
            isClone = true;
        }
    }

    private void Start()
    {
        if (isClone) return;

        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
            Debug.Log("🌊 WaveObPlanet 코루틴 정지됨!");
        }
    }

    private IEnumerator SpawnRoutine()
    {
        Debug.Log("🌊 WaveObPlanet 코루틴 시작");

        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Spawn();
        }
    }

    public void Spawn()
    {
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize;
        float camWidth = cam.aspect * camHeight;

        bool fromLeft = Random.value < 0.5f;
        float x = fromLeft ? -camWidth - 1f : camWidth + 1f;
        float y = Random.Range(-camHeight + 1f, camHeight - 1f);

        Vector2 spawnPos = new Vector2(x, y);

        GameObject obj = Instantiate(waveObPlanetPrefab);
        if (obj.TryGetComponent(out WaveObPlanet wave))
        {
            wave.SetDirectionFromLeft(fromLeft);
            wave.Spawn(); // 인터페이스 호출
        }

        Debug.Log($"🌊 WaveObPlanet 생성 at {spawnPos}, 방향: {(fromLeft ? "→" : "←")}");
    }
}
