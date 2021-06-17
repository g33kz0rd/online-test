using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class PlayerSyncer : NetworkBehaviour
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

        InformServerRpc(transform.position, transform.rotation, Vector3.Magnitude(characterController.velocity));
    }

    [ServerRpc]
    private void InformServerRpc(Vector3 position, Quaternion rotation, float speed)
    {
        InformClientRpc(position, rotation, speed);
    }

    [ClientRpc]
    private void InformClientRpc(Vector3 position, Quaternion rotation, float speed)
    {
        if (networkObject.IsOwner)
            return;

        transform.position = position;
        transform.rotation = rotation;
        animator.SetFloat("Speed", speed);
    }
}
