using MLAPI;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerChecker : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objectsToRemove;
    [SerializeField]
    private List<Component> componentsToRemove;

    // Start is called before the first frame update
    void Start()
    {
        var networkObject = GetComponent<NetworkObject>();
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
