using Game_Events;
using UnityEngine;
using Utilities.Publisher_Subscriber_System;

namespace Pamir.Manager {
    public class DummyGameManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                PublisherSubscriber.Publish(GameEventType.GameStart);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                PublisherSubscriber.Publish(GameEventType.GameEnd);
            }
        }
    }
}
