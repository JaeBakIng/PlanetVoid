using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public float initialSpeed = 5f; // 공의 초기 속도
    private Vector2 currVel = new Vector2();
    private Rigidbody2D rb;
    

    void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;

        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // 공의 초기 속도 설정 (2D에서 x, y 속도를 설정)
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            rb.linearVelocity = randomDir * initialSpeed;

            currVel = rb.linearVelocity;
        }
        else
        {
            Debug.LogError("Rigidbody2D 컴포넌트를 찾을 수 없습니다.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("wall") || collision.collider.CompareTag("sun") || collision.collider.CompareTag("WaveOb") || collision.collider.CompareTag("Meteor"))
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
