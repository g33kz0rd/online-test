using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using System;
using UnityEngine;

public class PlayerSyncer : NetworkBehaviour
{
    private NetworkObject networkObject;

    public override void NetworkStart()
    {
        networkObject = GetComponent<NetworkObject>();
    }

    void Update()
    {
        if (networkObject.IsOwner)
            InformServerRpc(transform.position);
    }

    [ServerRpc]
    private void InformServerRpc(Vector3 position)
    {
        InformClientRpc(position);
    }

    [ClientRpc]
    private void InformClientRpc(Vector3 position)
    {
        if (networkObject.IsOwner)
            return;

        transform.position = position;
    }
}
