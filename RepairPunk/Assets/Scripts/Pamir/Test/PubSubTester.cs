using Game_Events;
using UnityEngine;
using Utilities.EventManager;
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

            GameEventManager.OnGameStartEvent += OnGameStartEventHandler;
            GameEventManager.OnGameEndEvent += OnGameEndEventHandler;
        }

        private void OnDisable()
        {
            PublisherSubscriber.Unsubscribe(gameStartEventSubscription);
            PublisherSubscriber.Unsubscribe(gameEndEventSubscription);
            
            GameEventManager.OnGameStartEvent -= OnGameStartEventHandler;
            GameEventManager.OnGameEndEvent -= OnGameEndEventHandler;
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

        private void OnGameStartEventHandler()
        {
            rewindableObjectGroup.ReleaseObjects();
        }

        private void OnGameEndEventHandler()
        {
            rewindableObjectGroup.RecallObjects();
        }
    }
}
