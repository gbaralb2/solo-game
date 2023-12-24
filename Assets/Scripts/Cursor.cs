using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] RectTransform rt;

    void Update()
    {
        Vector3 cursorPos = Input.mousePosition;
        rt.position = cursorPos;
    }
}
