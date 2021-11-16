using UnityEngine;

public class BallController : MonoBehaviour
{
    public float acceleration;
    public Vector3 direction;
    public float maxVelocity;
    public Vector3 lastVelocity;
    public Rigidbody rb;
    public bool ready = false;


    float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BallMovement();
    }


    private void Update()
    {
        if (!ready && Input.GetMouseButtonDown(0))
        {
            StartMoveBall();
        }
    }

    private void StartMoveBall()
    {
        direction = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), 0, 1);
        ready = true;
    }

    void BallMovement()
    {
        if (!ready)
            return;
        rb.AddForce(direction.normalized * acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        lastVelocity = rb.velocity;

        var angle = Quaternion.AngleAxis(lastVelocity.magnitude, Vector3.down);
        transform.rotation *= angle;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ITouchable touchable = collision.gameObject.GetComponent<ITouchable>();

        if (touchable == null)
            return;

        touchable.Touch(this, collision);

    }
}
