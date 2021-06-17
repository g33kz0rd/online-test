using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class AnimationSyncer : NetworkBehaviour
{
    private NetworkObject networkObject;
    private CharacterController characterController;
    [SerializeField]
    private Animator animator;

    public override void NetworkStart()
    {
        networkObject = GetComponent<NetworkObject>();

        if (!networkObject.IsOwner)
            return;

        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!networkObject.IsOwner)
            return;

        InformServerRpc(Vector3.Magnitude(characterController.velocity));
    }

    [ServerRpc]
    private void InformServerRpc(float speed)
    {
        InformClientRpc(speed);
    }

    [ClientRpc]
    private void InformClientRpc(float speed)
    {
        if (networkObject.IsOwner)
            return;
        animator.SetFloat("Speed", speed);
    }
}
