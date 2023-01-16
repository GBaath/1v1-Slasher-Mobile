using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkingUI : MonoBehaviour
{
    [SerializeField] private Button serverBtn,hostBtn,clientBtn;

    private void Awake()
    {
        serverBtn.onClick.AddListener(()=> { NetworkManager.Singleton.StartServer(); });
        hostBtn.onClick.AddListener(() => { NetworkManager.Singleton.StartHost(); });
        clientBtn.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); });
    }
}
