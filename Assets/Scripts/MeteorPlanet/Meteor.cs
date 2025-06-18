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

        // �밢�� ����: x�� ���� (-0.5 ~ 0.5), y�� �׻� -1 (�Ʒ�)
        Vector2 direction = new Vector2(Random.Range(-0.5f, 0.5f), -1f).normalized;

        rb.linearVelocity = direction * fallSpeed;

        Destroy(gameObject, lifeTime); // �ʹ� ���� ������� �ʵ���
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
