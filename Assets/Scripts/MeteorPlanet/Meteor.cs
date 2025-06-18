using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float lifeTime = 10f;


    private Vector2 currVel;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 대각선 방향: x는 랜덤 (-0.5 ~ 0.5), y는 항상 -1 (아래)
        Vector2 direction = new Vector2(Random.Range(-0.5f, 0.5f), -1f).normalized;

        rb.linearVelocity = direction * fallSpeed;

        Destroy(gameObject, lifeTime); // 너무 오래 살아있지 않도록
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("sun"))
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 reflectedVelocity = currVel - 2 * Vector2.Dot(currVel, normal) * normal;

            rb.linearVelocity = reflectedVelocity;
            currVel = reflectedVelocity;
        }
    }

}
