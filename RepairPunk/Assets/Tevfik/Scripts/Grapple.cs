using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public Transform cam;
    public RaycastHit hit;
    private bool attached = false;
    public float step;
    public float speed;
    public float momentum;
    public Vector3 velo;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(cam.position, cam.forward, out hit))
            {
                attached = true;

            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            attached = false;
            velo = GetComponentInChildren<CharacterController>().velocity;
            velo = transform.forward * momentum;
        }
        if (attached)
        {
            momentum += speed * Time.deltaTime;
            step = momentum * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, hit.point, step);
        }
    }
}
