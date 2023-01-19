using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class OpponentTranslator : MonoBehaviour
{
    RectTransform localShieldRepresentation;

    private void Start()
    {
        localShieldRepresentation = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))    
        {
            UpdateRelativePosition(GameManager.instance.localPlayer.GetComponent<PlayerClient>().relativeShieldPosition);
            Debug.Log(GameManager.instance.localPlayer.GetComponent<PlayerClient>().relativeShieldPosition.Value);
        }
            //UpdatePosition(Camera.main.ScreenToWorldPoint(GameManager.instance.opponentShield.GetComponent<RectTransform>().position));

    }
    //get data from network and update local transform
    public void UpdatePosition(Vector2 opponentCanvasPosition)
    {
        //todo screen to world point
        transform.position = new Vector2(localShieldRepresentation.position.x * -1, localShieldRepresentation.position.y);
    }
    public void UpdateRelativePosition(NetworkVariable<Vector2> relativeVector)
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.scaledPixelWidth * relativeVector.Value.x, Camera.main.scaledPixelHeight * relativeVector.Value.y)); //set opshield  to the relative screen point of opponentpos
    }
}
