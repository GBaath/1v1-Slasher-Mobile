using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mobileStart : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    // Start is called before the first frame update
    void Start()
    {
        FirebaseSyncing.instance.AnonymousSignIn();
        FirebaseSyncing.instance.GetWinningPlayerName(nameText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
