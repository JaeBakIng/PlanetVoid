using UnityEngine;

public class WaveObPlanet : MonoBehaviour
{
    public float speed = 3f;
    public float waveAmplitude = 1f;
    public float waveFrequency = 2f;
    public float lifeTime = 10f;


    private Vector2 currVel = new Vector2();
    private Rigidbody2D rb;




    private Vector2 startPosition;
    private Vector2 moveDirection;
    private float timeElapsed;

    private bool fromLeft = true; // 외부에서 지정

    public void SetDirectionFromLeft(bool isFromLeft)
    {
        fromLeft = isFromLeft;
    }

    public void Spawn()
    {
        Camera cam = Camera.main;

        float camHeight = cam.orthographicSize;
        float camWidth = cam.aspect * camHeight;

        float y = Random.Range(-camHeight + 1f, camHeight - 1f);
        float x = fromLeft ? -camWidth - 1f : camWidth + 1f;

        startPosition = new Vector2(x, y);
        moveDirection = fromLeft ? Vector2.right : Vector2.left;

        transform.position = startPosition;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        Vector2 waveOffset = Vector2.Perpendicular(moveDirection) * Mathf.Sin(timeElapsed * waveFrequency) * waveAmplitude;
        Vector2 forward = moveDirection * speed * timeElapsed;

        transform.position = startPosition + forward + waveOffset;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("sun"))
        {

            // 충돌한 벽의 법선 벡터를 가져옴
            Vector2 normal = collision.contacts[0].normal;

            // 입사 속도 벡터 (현재 속도)
            Vector2 incomingVelocity = currVel;

            // 법선 벡터를 사용하여 반사 벡터를 계산
            Vector2 reflectedVelocity = incomingVelocity - 2 * Vector2.Dot(incomingVelocity, normal) * normal;



            // 반사된 속도 적용 (반대 방향으로 튕겨나가게 설정)
            rb.linearVelocity = reflectedVelocity;

            currVel = rb.linearVelocity;

        }
    }


}
