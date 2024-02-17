using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    private float _noteSpeed = GManager.instance.noteSpeed;

    private bool start;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            start = true;
        }

        if (start)
        {
            var transform1 = transform;
            transform1.position -= transform1.forward * (Time.deltaTime * _noteSpeed);
        }
    }
}
