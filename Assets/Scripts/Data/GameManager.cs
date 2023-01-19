using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int turn;

    public int nrofPlayers;

    public GameObject opponentShield, p1shield, p2shield;
    public DraggableClickable localShieldReference;
    public List<GameObject> players = new List<GameObject>();
    public ServerCanvas serverCanvas;
    public PlayerData localPlayer;
    public NetworkBehaviour server;


    private void Awake()
    {
        instance = this;
    }
    private void SetOpponentShieldReference(PlayerData pdata)
    {
        if (pdata.playerIndex == 0)
            opponentShield = p2shield;
        else if (pdata.playerIndex == 1)
            opponentShield = p1shield;
    }
    public void ResolveTurn()
    {

    }
    public void OnPlayerJoin(PlayerData joinedData)
    {
        nrofPlayers++;
        joinedData.playerIndex = nrofPlayers - 1;
        players.Add(joinedData.gameObject);

        if (joinedData.client.IsOwner)
        {
            localPlayer = joinedData;
            
        }


        //aids
        if(players.Count == 2)
        {
            p1shield = players[0];
            p2shield = players[1];

            SetOpponentShieldReference(players[0].GetComponent<PlayerData>());
            SetOpponentShieldReference(players[1].GetComponent<PlayerData>());
        }
    }
}
