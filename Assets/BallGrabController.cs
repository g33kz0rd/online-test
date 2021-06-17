using UnityEngine;

public class BallGrabController : MonoBehaviour
{
    private GameObject ball;
    private Rigidbody ballRigidBody;

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballRigidBody = ball.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!Input.GetKey(KeyCode.E))
            return;

        if (Vector3.Distance(ball.transform.position, transform.position) > .5)
            return;

        ball.transform.parent = transform;
        ballRigidBody.useGravity = false;
        ballRigidBody.isKinematic = true;
    }

    private void OnGUI()
    {
        GUI.TextField(new Rect(0, 0, 100, 20), $"Distance is {Vector3.Distance(ball.transform.position, transform.position)}");
    }
}
