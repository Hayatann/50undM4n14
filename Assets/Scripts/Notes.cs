using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    private int _noteSpeed = 8;
    // Update is called once per frame
    void Update()
    {
        var transform1 = transform;
        transform1.position -= transform1.forward * (Time.deltaTime * _noteSpeed);
    }
}
