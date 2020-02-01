using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineTry : MonoBehaviour
{
    Shader mat;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Renderer>().material.color = Color.red;
        mat = GetComponent<Renderer>().material.shader = Shader.Find("Outline Toolkit/Outline");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
