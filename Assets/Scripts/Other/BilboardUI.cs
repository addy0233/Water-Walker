using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardUI : MonoBehaviour
{
    Transform cam;

    private void Start()
    {
        cam = PlayerManager.instance.playerCamera.transform;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}
