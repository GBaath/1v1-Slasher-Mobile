using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int turn;

    public int nrofPlayers;


    private void Awake()
    {
        instance = this;
    }

    public void ResolveTurn()
    {

    }
    public void OnPlayerJoin(PlayerData joinedData)
    {
        joinedData.playerIndex = nrofPlayers - 1;
    }
}
