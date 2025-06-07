using UnityEngine;

public class WaveObPlanet : MonoBehaviour
{
    public float speed = 3f;
    public float waveAmplitude = 1f;
    public float waveFrequency = 2f;
    public float lifeTime = 10f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 currVel;
    private float timeElapsed;
    private bool fromLeft = true;

    private bool isStopped = false;

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

        transform.position = new Vector2(x, y);
        Destroy(gameObject, lifeTime);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = fromLeft ? Vector2.right : Vector2.left;
        currVel = moveDirection * speed;
        rb.linearVelocity = currVel;
    }

    private void FixedUpdate()
    {
        if (isStopped || rb == null) return;

        timeElapsed += Time.fixedDeltaTime;

        Vector2 waveOffset = Vector2.Perpendicular(moveDirection) * Mathf.Sin(timeElapsed * waveFrequency) * waveAmplitude;
        Vector2 velocity = moveDirection * speed + waveOffset;

        rb.linearVelocity = velocity;
        currVel = velocity;
    }

    public void ApplyGravityAndStop()
    {
        if (rb != null)
        {
            rb.gravityScale = 2f;
            
        }
        isStopped = true;
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
