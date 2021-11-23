using UnityEngine;

public class RocketController : MonoBehaviour, ITouchable
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private float rocketSpeed;

    private Vector3 leftCorner
    {
        get
        {
            return leftCornerItem.position + Vector3.right * leftOffSet;
        }
    }
    private Vector3 rightCorner
    {
        get
        {
            return rightCornerItem.position + Vector3.left * rightOffSet;
        }
    }
    [SerializeField] private Transform leftCornerItem;
    [SerializeField] private Transform rightCornerItem;
    [SerializeField] private float leftOffSet;
    [SerializeField] private float rightOffSet;

    public void Touch(BallController ball, Collision collision)
    {
        var speed = ball.lastVelocity.magnitude;
        ball.direction = collision.GetContact(0).point - boxCollider.bounds.center;
        int angleSign = ball.direction.x > 0 ? 1 : -1;
        var reflection = Vector3.Lerp(Vector3.forward, Vector3.right, Mathf.Abs(ball.direction.x));

        reflection.x *= angleSign;
        ball.direction = reflection;
        ball.rb.velocity = reflection.normalized * Mathf.Max(speed, 0f);

        //turning the ball in a chosen direction
        var angle = Quaternion.AngleAxis(90f, ball.direction);
        ball.transform.rotation = angle;
        playerStats.AddScore();

    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RocketMove();
    }
    void RocketMove()
    {
        var mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layer = 3;
        if (Physics.Raycast(mousePosition, out RaycastHit raycastHit, Mathf.Infinity, ~layer))
        {
            float toMouse = raycastHit.point.x - transform.position.x;
            rb.velocity = new Vector3(Mathf.Lerp(0, toMouse, 0.9f), 0, 0) * rocketSpeed;
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, rocketSpeed);
        }
    }

}
