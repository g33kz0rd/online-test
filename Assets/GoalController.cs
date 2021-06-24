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
        VariableTrackerController.TrackVariable("General", gameObject, "Score", $"Score is: {Score}");
    }
}
