using UnityEngine;

public class RocketController : MonoBehaviour, ITouchable
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider boxCollider;
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
        ball.rb.velocity = ball.direction.normalized * Mathf.Max(speed, 0f);
        //turning the ball in a chosen direction
        var angle = Quaternion.AngleAxis(90f, ball.direction.normalized);
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
            if (raycastHit.point.x >= leftCorner.x && raycastHit.point.x <= rightCorner.x)
            {
                rb.position = Vector3.Lerp(transform.position, new Vector3(raycastHit.point.x, rb.position.y, rb.position.z), 0.9f);
            }
            else if (raycastHit.point.x <= leftCorner.x)
            {
                rb.position = Vector3.Lerp(transform.position, new Vector3(leftCorner.x, rb.position.y, rb.position.z), 0.9f);
            }

            else if (raycastHit.point.x >= rightCorner.x)
            {
                rb.position = Vector3.Lerp(transform.position, new Vector3(rightCorner.x, rb.position.y, rb.position.z), 0.9f);
            }

        }
    }

}
