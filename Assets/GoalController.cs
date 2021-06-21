using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class GoalController : NetworkBehaviour
{
    public int Score { get; set; }
    private BallController ballController;

    private void FixedUpdate()
    {
        if (ballController != null)
            return;

        var ball = GameObject.FindGameObjectWithTag("Ball");

        if (ball == null)
            return;

        ballController = ball.GetComponent<BallController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ball")
            return;

        Debug.Log("colliderrrrr");

        GoalServerRpc();

    }

    [ServerRpc]
    private void GoalServerRpc()
    {
        GoalClientRpc();
    }

    [ClientRpc]
    private void GoalClientRpc()
    {
        Score++;
        ballController.Reset();
    }

    private void OnGUI()
    {
        GUI.TextField(new Rect(360, 20, 100, 20), $"Score is: {Score}");
    }
}
