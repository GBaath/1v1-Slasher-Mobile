using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCanvas : MonoBehaviour
{
    public void UpdateShieldReferencePosition(int shieldIndex, Vector2 newPos)
    {
        switch (shieldIndex)
        {
            case 0:
                GameManager.instance.p1shield.transform.position = Camera.main.ScreenToWorldPoint(newPos); //RelativeScreenPos(newPos.x / Camera.main.scaledPixelWidth, newPos.y / Camera.main.scaledPixelHeight)/50;//
                break;
            case 1:
                GameManager.instance.p2shield.transform.position = Camera.main.ScreenToWorldPoint(newPos); //RelativeScreenPos(newPos.x / Camera.main.scaledPixelWidth, newPos.y / Camera.main.scaledPixelHeight)/50;//
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
