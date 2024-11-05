using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public void FollowThePlayer(float playerPosX)
    {
        transform.position = new Vector3(playerPosX,transform.position.y,transform.position.z);
    }
}
