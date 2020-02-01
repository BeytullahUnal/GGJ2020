using Game_Events;
using UnityEngine;
using Utilities.Publisher_Subscriber_System;

namespace Pamir.Test {
    public class PubSubTester : MonoBehaviour
    {
        [SerializeField] private RewindableObjectGroup rewindableObjectGroup;
        
        private Subscription<GameEventType> gameStartEventSubscription;
        private Subscription<GameEventType> gameEndEventSubscription;

        private void OnEnable()
        {
            gameStartEventSubscription = PublisherSubscriber.Subscribe<GameEventType>(GameStartEventHandler);
            gameEndEventSubscription = PublisherSubscriber.Subscribe<GameEventType>(GameEndEventHandler);
        }

        private void OnDisable()
        {
            PublisherSubscriber.Unsubscribe(gameStartEventSubscription);
            PublisherSubscriber.Unsubscribe(gameEndEventSubscription);
        }

        private void GameStartEventHandler(GameEventType gameEventType)
        {
            if (gameEventType == GameEventType.GameStart)
            {
                rewindableObjectGroup.ReleaseObjects();
            }
        }

        private void GameEndEventHandler(GameEventType gameEventType)
        {
            if (gameEventType == GameEventType.GameEnd)
            {
                rewindableObjectGroup.RecallObjects();
            }
        }
    }
}
