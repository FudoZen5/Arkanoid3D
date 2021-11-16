using UnityEngine;

public class Wall : MonoBehaviour, ITouchable
{
    public void Touch(BallController ball, Collision collision)
    {
        var speed = ball.lastVelocity.magnitude;
        ball.direction = Vector3.Reflect(ball.lastVelocity, collision.GetContact(0).normal);
        ball.rb.velocity = ball.direction * Mathf.Max(speed, 0f);

        var angle = Quaternion.AngleAxis(90f, ball.direction.normalized);
        ball.transform.rotation = angle;
    }

}
