using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int turn;

    public int nrofPlayers;

    public GameObject opponentShield,p1shield, p2shield;
    public OpponentSword opponentSword;
    public SlashFollow localSword;
    public DraggableClickable localShieldReference;
    public List<GameObject> players = new List<GameObject>();
    public ServerCanvas serverCanvas;
    public PlayerData localPlayer,otherPlayer;
    public Server server;

    [SerializeField] GameObject mainCanvas, waitingText;


    private void Awake()
    {
        instance = this;
    }
    public void FinishTurn()
    {
        server.CheckEndTurnServerRpc();
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
        if (players.Count == 2)
        {
            p1shield = players[0];
            p2shield = players[1];



            //other p reference set
            if (localPlayer.playerIndex == 0)
                otherPlayer = players[1].GetComponent<PlayerData>();
            else
                otherPlayer = players[0].GetComponent<PlayerData>();

            mainCanvas.SetActive(true);
            waitingText.SetActive(false);
        }
    }
}
