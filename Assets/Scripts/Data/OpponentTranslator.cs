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
            UpdatePosition(GameManager.instance.otherPlayer.transform.position);
        }
        try
        {

        }
        catch { }
            //UpdatePosition(Camera.main.ScreenToWorldPoint(GameManager.instance.opponentShield.GetComponent<RectTransform>().position));

    }
    //get data from network and update local transform
    public void UpdatePosition(Vector2 opponentCanvasPosition)
    {
        //todo screen to world point
        transform.position = new Vector3(opponentCanvasPosition.x * -1, opponentCanvasPosition.y ,Camera.main.nearClipPlane); //y + playersprite y offset (difference between opponent sprite and self sprite)
    }
    public void UpdateRelativePosition(Vector2 relativeVector)
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.scaledPixelWidth * relativeVector.x, Camera.main.scaledPixelHeight * relativeVector.y)); //set opshield  to the relative screen point of opponentpos
    }
}
