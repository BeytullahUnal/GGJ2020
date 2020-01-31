using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindController : MonoBehaviour
{
    public List<Rewind> rewindObjects;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            foreach(Rewind r in rewindObjects)
            {
                r.InitiateRewindRecording();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (Rewind r in rewindObjects)
            {
                r.RewindObject();
            }
        }
    }
}
