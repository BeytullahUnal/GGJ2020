using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindableObject : MonoBehaviour
{
    public bool holdForever;
    public float holdDuration;
    bool finalFall;
    private float recallEndTime;
    bool rewinded;

    private List<Vector3> rewindPositions;
    private List<Quaternion> rewindRotations;

    private bool saveTransforms;
    private Rigidbody myRigidbody;
    private Transform myTransform;

    private bool doRewind;

    private int rewindPosIndex;
    private int rewindQuatIndex;

    private float hoverPosY;
    private bool onReleasePosition;

    private void Awake()
    {
        rewindPositions = new List<Vector3>();
        rewindRotations = new List<Quaternion>();

        myRigidbody = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();
    }

    public void CustomUpdate()
    {
      
        if(!saveTransforms && myRigidbody.isKinematic == true && !doRewind && !rewinded && onReleasePosition)
        {
            Vector3 hoverPos = new Vector3(myTransform.position.x, hoverPosY + .2f * Mathf.Sin(2 * Time.time), myTransform.position.z);
            myTransform.position = hoverPos;
        }

        if (finalFall)
            return;

        if (saveTransforms)
        {
            RecordTransform();
        }

        if (doRewind)
        {
            RewindToTransform();
        }

        if (Time.time >= holdDuration + recallEndTime && !finalFall && !holdForever && rewinded)
        {
            finalFall = true;
            myRigidbody.isKinematic = false;
        }
    }

    public void InitiateRewindRecording()
    {
        if (saveTransforms) return;

        if (finalFall) return;

        myRigidbody.velocity = Vector3.zero;
        
        RecordTransform();

        saveTransforms = true;

        myRigidbody.isKinematic = false;
        myRigidbody.AddForce(Random.insideUnitCircle.normalized * Random.Range(25, 100));
        myRigidbody.AddTorque(Random.insideUnitCircle.normalized * Random.Range(5, 20));
    }

    public void EndRewindRecording()
    {
        saveTransforms = false;
        myRigidbody.isKinematic = true;
        onReleasePosition = true;
        hoverPosY = transform.position.y;
    }


    public void RewindObject()
    {
        if (finalFall) return;

        EndRewindRecording();

        rewindPosIndex = rewindPositions.Count - 1;
        rewindQuatIndex = rewindRotations.Count - 1;

        doRewind = true;
    }

    public void EndRewinding()
    {
        rewindPositions.Clear();
        rewindRotations.Clear();
        rewinded = true;
        recallEndTime = Time.time;
        doRewind = false;
    }

    private void RecordTransform()
    {
        rewindPositions.Add(myTransform.position);
        rewindRotations.Add(myTransform.rotation);
    }

    private void RewindToTransform()
    {

        if (rewindPosIndex < 0 || rewindQuatIndex < 0)
        {
            EndRewinding();
            return;
        }

        myTransform.position = rewindPositions[rewindPosIndex];
        myTransform.rotation = rewindRotations[rewindQuatIndex];

        rewindPosIndex--;
        rewindQuatIndex--;

    }
}
