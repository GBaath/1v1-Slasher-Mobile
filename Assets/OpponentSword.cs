using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSword : MonoBehaviour
{
    private LineRenderer line;
    public Vector3 start, end;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        //Invoke("OnStart",1);

        GameManager.instance.otherPlayer.slashStart.OnValueChanged += (_old, _new) =>
        {
            if (!GameManager.instance.otherPlayer.finishedTurn.Value)
            {
                start = _new;
                start = new Vector3(-start.x, start.y, start.z); //magic mumber from difference between player shadow and opponent input
            }
        };
        GameManager.instance.otherPlayer.slashEnd.OnValueChanged += (_old, _new) =>
        {
            if (!GameManager.instance.otherPlayer.finishedTurn.Value)
            {
                end = _new;
                end = new Vector3(-end.x, end.y, end.z);
            }
        };
    }
    public void OnStart()
    {
        //Draw(new Vector3(-100, 100, 0), new Vector3(100, -100), 5);
    }
    public void Draw()
    {
        start += Vector3.forward * Camera.main.nearClipPlane;
        end += Vector3.forward * Camera.main.nearClipPlane;
        StartCoroutine(ShowSlash(start, end, 1));
    }
    private IEnumerator ShowSlash(Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        line.SetPosition(0, startPosition);
        line.SetPosition(1, targetPosition);
        float time = 0;
        while (time < duration)
        {
            //transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            line.startColor = Color.Lerp(Color.red, Color.clear, time/duration);
            line.endColor = Color.Lerp(Color.red, Color.clear, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        line.startColor = Color.clear;
        line.endColor = Color.clear;
    }
}
