using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera mainCamera;

    Vector3 basePos;
    Vector3 castlePos;

    float curTime;
    float lerpTime =1f;
    float mouseMoveSpeed = 2f;
    float cameraXPos;

    private void Awake()
    {
        mainCamera = Camera.main;
        basePos = new Vector3(-36, 0,-10);
        castlePos = new Vector3(0, 0,-10);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mainCamera.transform.Translate(-Input.GetAxis("Mouse X") * mouseMoveSpeed, 0,0);
            mainCamera.transform.position = new Vector3(Mathf.Clamp(mainCamera.transform.position.x, 0, 41),0,-10);
        }

    }


    public void TransitionBaseNCastle(bool moveToCastle)
    {
        curTime = 0;
        StartCoroutine(CameraMoveCoroutine(moveToCastle));

    }
    IEnumerator CameraMoveCoroutine(bool moveToCastle)
    {
        Vector3 startPos = mainCamera.transform.position;
        Vector3 endPos = moveToCastle ? castlePos : basePos;

        while (true)
        {
            curTime += Time.deltaTime;

            if (curTime >= lerpTime)
            {
                curTime = lerpTime;
                break;
            }
            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, curTime / lerpTime);
            yield return null;
        }

        mainCamera.transform.position = endPos;
    }
}
