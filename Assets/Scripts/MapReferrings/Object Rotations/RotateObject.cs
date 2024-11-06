using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    public float objectSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 25 * objectSpeed * Time.deltaTime);
    }
}
