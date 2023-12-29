using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] private float speed = 3;

    [SerializeField] private int num = 0;

    private float _alfa = 0;
    private Renderer _rend;
    // Start is called before the first frame update
    void Start()
    {
        _rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(_rend.material.color.a <= 0))
        {
            var material = _rend.material;
            material.color =
                new Color(material.color.r, material.color.g, material.color.b, _alfa);
        }
        if (num == 1)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                ColorChange();
            }
        }
        if (num == 2)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ColorChange();
            }
        }
        if (num == 3)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                ColorChange();
            }
        }
        if (num == 4)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                ColorChange();
            }
        }

        _alfa -= Time.deltaTime * speed;
    }

    void ColorChange()
    {
        _alfa = 0.3f;
        var material = _rend.material;
        material.color = new Color(material.color.r, material.color.g, material.color.b,_alfa);
    }
}
