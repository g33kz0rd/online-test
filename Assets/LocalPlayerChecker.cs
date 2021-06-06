using MLAPI;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerChecker : MonoBehaviour
{
    private NetworkObject networkObject;
    public List<GameObject> objectsToRemove;
    public List<Component> componentsToRemove;

    // Start is called before the first frame update
    void Start()
    {
        networkObject = GetComponent<NetworkObject>();
        if (!networkObject.IsLocalPlayer)
        {
            foreach (var objToRm in objectsToRemove)
                Destroy(objToRm);
            foreach (var compToRm in componentsToRemove)
                Destroy(compToRm);
        }

        Destroy(this);
    }
}
