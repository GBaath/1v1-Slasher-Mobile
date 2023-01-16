using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerClient : NetworkBehaviour
{
    private void Start()
    {
        OnPlayerJoin();
    }
    public void OnPlayerJoin()
    {
        GameManager.instance.OnPlayerJoin(GetComponent<PlayerData>());
        transform.parent = GameManager.instance.serverCanvas.transform;
    }

}
