using System.Collections.Generic;
using UnityEngine;

namespace Pamir {
    [ExecuteAlways]
    public class RewindableObjectGroup : MonoBehaviour
    {
        public float recordTime;
        private float recordStartTime;

        [SerializeField] private List<RecordedRewindableObject> rewindableObjects;

        bool recordTransforms;
        bool rewindObjects;

        private void Awake()
        {
            rewindableObjects = new List<RecordedRewindableObject>();

            var children = transform.GetComponentsInChildren<RecordedRewindableObject>();

            foreach(var rewindObject in children)
            {
                rewindableObjects.Add(rewindObject);
            }
        }

        private void Update()
        {
            if (Time.time >= recordStartTime + recordTime && recordTransforms)
            {
                recordTransforms = false;
                StopObjectRecording();
            }

            foreach(var recordedRewindableObject in rewindableObjects)
            {
                recordedRewindableObject.CustomUpdate();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ReleaseObjects();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                RecallObjects();
            }
        }
    
        public void StopObjectRecording()
        {
            foreach (var recordedRewindableObject in rewindableObjects)
            {
                recordedRewindableObject.EndRewindRecording();
            }
        }

        public void ReleaseObjects()
        {
            recordStartTime = Time.time;
            recordTransforms = true;

            foreach(var recordedRewindableObject in rewindableObjects)
            {
                recordedRewindableObject.InitiateRewindRecording();
            }
        }

        public void RecallObjects()
        {
            foreach (var recordedRewindableObject in rewindableObjects)
            {
                recordedRewindableObject.RewindObject();
            }
        }

    }
}