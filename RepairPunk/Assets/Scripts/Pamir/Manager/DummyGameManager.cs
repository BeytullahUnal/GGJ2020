using Game_Events;
using UnityEngine;
using Utilities.EventManager;
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

            if (Input.GetKeyDown(KeyCode.A))
            {
                GameEventManager.RaiseOnGameStartEvent();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                GameEventManager.RaiseOnGameEndEvent();
            }
        }
    }
}
