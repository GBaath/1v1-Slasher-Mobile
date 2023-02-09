using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
using Firebase.Auth;
using TMPro;

public class FirebaseSyncing : MonoBehaviour
{
    public static FirebaseSyncing instance;
    private void Awake()
    {
        instance = this;
    }

    public string index0Name, index1Name;

    FirebaseDatabase db;
    FirebaseAuth auth;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SetPlayerName(GameManager.instance.localPlayer.playerIndex,GameManager.instance.localName);
        }
    }
    public void SetPlayerName(int playerIndex, string name)
    {

        var db = FirebaseDatabase.DefaultInstance;
        var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        db.RootReference.Child("pIndex" + playerIndex.ToString()).SetValueAsync(name).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);
            else
                Debug.Log("DataTestWrite: Complete");
        });
    }
    public void GetPlayerName(int playerIndex, bool writeToSlider)
    {
        FirebaseDatabase.DefaultInstance.RootReference.Child("pIndex" + playerIndex.ToString()).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            //Log if we get any errors from the opperation
            if (task.Exception != null)
                Debug.LogError(task.Exception);

            db = FirebaseDatabase.DefaultInstance;
            if (writeToSlider)
            {
                DataSnapshot data = task.Result;
                GameManager.instance.opponentHpSlider.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = (data.Value.ToString());
                GameManager.instance.opName = (data.Value.ToString());
            }
        });
        
    }
    public void SetWinningPlayerName(string name)
    {

        var db = FirebaseDatabase.DefaultInstance;
        var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        db.RootReference.Child("lastWinner").SetValueAsync(name).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);
            else
                Debug.Log("DataTestWrite: Complete");
        });
    }
    public void GetWinningPlayerName(TextMeshProUGUI textRef)
    {

        var db = FirebaseDatabase.DefaultInstance;
        //var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        db.RootReference.Child("lastWinner").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);
            else
            {
                DataSnapshot data = task.Result;
                textRef.text = (data.Value.ToString());
            }
        });
    }
    public void AnonymousSignIn()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task => {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
            }
        });
    }
    private void Login()
    {
        auth.SignInWithCustomTokenAsync(GameManager.instance.localPlayer.playerIndex.ToString()).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
            }
        });
    }
    public void OnStart()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogError(task.Exception);

            auth = FirebaseAuth.DefaultInstance;
            AnonymousSignIn();
            //Login();
        });

    }
}
