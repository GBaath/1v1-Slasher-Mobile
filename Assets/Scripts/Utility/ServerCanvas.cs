using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ServerCanvas : MonoBehaviour
{
    public void UpdateShieldReferencePosition(int shieldIndex, Vector2 newPos)
    {
        switch (shieldIndex)
        {
            case 0:
                GameManager.instance.p1shield.transform.position = (newPos);
                GameManager.instance.localPlayer.client.relativeShieldPosition.Value = RelativeScreenPos(newPos.x / Camera.main.scaledPixelWidth, newPos.y / Camera.main.scaledPixelHeight);
                //GameManager.instance.players[1].GetComponent<PlayerClient>().relativeShieldPosition = 
                //  new NetworkVariable<Vector2>(RelativeScreenPos(newPos.x / Camera.main.scaledPixelWidth, newPos.y / Camera.main.scaledPixelHeight));
                break;
            case 1:
                GameManager.instance.p2shield.transform.position =(newPos);
                GameManager.instance.localPlayer.client.relativeShieldPosition.Value = RelativeScreenPos(newPos.x / Camera.main.scaledPixelWidth, newPos.y / Camera.main.scaledPixelHeight);
                //GameManager.instance.players[0].GetComponent<PlayerClient>().relativeShieldPosition = 
                //    new NetworkVariable<Vector2>(RelativeScreenPos(newPos.x / Camera.main.scaledPixelWidth, newPos.y / Camera.main.scaledPixelHeight));
                break;

            default:
                break;
        }
    }
    public Vector2 RelativeScreenPos(float xPercentage, float yPercentage)
    {
        Vector2 newPos;
        Camera cam = Camera.main;
        newPos = new Vector2(xPercentage * cam.scaledPixelWidth, yPercentage * cam.scaledPixelHeight);


        return newPos;
    }
}
