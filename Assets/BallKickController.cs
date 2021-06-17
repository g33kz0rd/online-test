using UnityEngine;

public class BallKickController : MonoBehaviour
{
    private GameObject ball;
    private Rigidbody ballRigidBody;

    public float kickForce = 10;

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballRigidBody = ball.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (ball.transform.parent != transform)
            return;

        if (!Input.GetKey(KeyCode.F))
            return;

        ball.transform.parent = null;
        ballRigidBody.useGravity = true;
        ballRigidBody.isKinematic = false;
        ballRigidBody.AddForce(transform.forward * kickForce + ballRigidBody.transform.up * kickForce * .1f);
    }

    private void OnGUI()
    {
        GUI.TextField(new Rect(0, 0, 100, 20), $"Distance is {Vector3.Distance(ball.transform.position, transform.position)}");
    }
}
