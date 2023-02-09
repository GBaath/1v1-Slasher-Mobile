using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int turn;


    public GameObject opponentShield,p1shield, p2shield;
    public OpponentSword opponentSword;
    public SlashFollow localSword;
    public DraggableClickable localShieldReference;
    public List<GameObject> players = new List<GameObject>();
    public ServerCanvas serverCanvas;
    public PlayerData localPlayer,otherPlayer;
    public Server server;

    public Slider localHpSlider, opponentHpSlider;

    [SerializeField] ClickableObject turnButton;

    [SerializeField] public GameObject mainCanvas, waitingText;

    [HideInInspector] public string localName;

    
    public string opName;


    [SerializeField] GameObject winScreen, loseScreen, drawScreen;


    private void Awake()
    {
        instance = this;

        Application.targetFrameRate = 60;
    }
    public void FinishTurn()
    {
        localPlayer.finishedTurn.Value = true;
        server.CheckEndTurnServerRpc();
    }
    public void OnPlayerJoin(PlayerData joinedData)
    {
        joinedData.playerIndex = System.Convert.ToInt16(joinedData.GetComponent<NetworkObject>().OwnerClientId);
        players.Add(joinedData.gameObject);
        server.nrOfPlayers = players.Count;

        if (joinedData.client.IsOwner)
        {
            localPlayer = joinedData;

            
        }
        if (players.Count > 1)
        {
            otherPlayer = players[1].GetComponent<PlayerData>();
            localPlayer = players[0].GetComponent<PlayerData>();
        }

        FirebaseSyncing.instance.OnStart();

        //aids
        if (players.Count == 2)
        {
            p1shield = players[0];
            p2shield = players[1];



            //other p reference set
            //if (localPlayer.playerIndex == 0)
            //    otherPlayer = players[1].GetComponent<PlayerData>();
            //else
            //    otherPlayer = players[0].GetComponent<PlayerData>();

            mainCanvas.SetActive(true);
            waitingText.SetActive(false);


            //set endturn button clickable when slash end gets a value
            localPlayer.slashEnd.OnValueChanged += (_old, _new) =>
            {
                turnButton.SetClickable(true);
            };
            //otherPlayer.slashEnd.OnValueChanged += (_old, _new) =>
            //{
            //    turnButton.SetClickable(true);
            //};
            otherPlayer.finishedTurn.OnValueChanged += (_old, _new) =>
            {
                server.CheckEndTurnServerRpc();
            };


            //updates opponenthpslider
            otherPlayer.hp.OnValueChanged += (_old, _new) =>
            {
                opponentHpSlider.value = _new;
            };


            FirebaseSyncing.instance.SetPlayerName(joinedData.playerIndex, localName);
            localHpSlider.GetComponentInChildren<TextMeshProUGUI>().text = localName;

            Invoke(nameof(GetName), 2f);
        }
    }
    private void GetName()
    {
        FirebaseSyncing.instance.GetPlayerName(otherPlayer.playerIndex,false);

    }
    public void SetLocalPlayerName(string name)
    {
        localName = name;
    }
    public void GameWin()
    {
        if (!loseScreen.activeSelf)
            winScreen.SetActive(true);
        else
            drawScreen.SetActive(true);
    }
    public void GameLose()
    {
        if (!winScreen.activeSelf)
            loseScreen.SetActive(true);
        else
            drawScreen.SetActive(true);
    }
}
