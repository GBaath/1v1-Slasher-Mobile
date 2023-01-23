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
    public NetworkBehaviour server;


    private void Awake()
    {
        instance = this;
    }
    private void SetOpponentShieldReference(PlayerData pdata)
    {
        //if (pdata.playerIndex == 0)
        //    opponentShield = p2shield;
        //else if (pdata.playerIndex == 1)
        //    opponentShield = p1shield;
    }
    public void CheckResolveTurn()
    {
        localPlayer.finishedTurn.Value = true;
        if(localPlayer.finishedTurn.Value && otherPlayer.finishedTurn.Value)
        {
            ResolveTurn();
        }
    }
    public void ResolveTurn()
    {
        turn++;
        //update reference positions
        opponentShield.GetComponent<OpponentTranslator>().UpdatePosition(otherPlayer.transform.position);
        //check hits
        if (localSword.CheckHit())
        {
            //update hp
            otherPlayer.SetHp(otherPlayer.hp.Value - localPlayer.dmg.Value);
        }
        //show opponent slash
        opponentSword.Draw(otherPlayer.slashStart.Value, otherPlayer.slashEnd.Value, 1);
        
        //sync variables
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


            //other p reference set
            if (localPlayer.playerIndex == 0)
                otherPlayer = players[1].GetComponent<PlayerData>();
            else
                otherPlayer = players[0].GetComponent<PlayerData>();
        }
    }
}
