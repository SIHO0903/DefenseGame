using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private void Start()
    {

        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        float timer =0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 1.2f)
            {
                SceneMove.Instance.LoadScene("Prepare");
                break;
            }
            yield return null;
        }
    }
}
