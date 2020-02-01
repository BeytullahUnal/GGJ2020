using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.Publisher_Subscriber_System {
    public static class PublisherSubscriber
    {
        private static Dictionary<Type, IList> Subscriber;
        private static bool IsInitialized;

        private static void Initialize()
        {
            if (IsInitialized) return;
            IsInitialized = true;
            Subscriber = new Dictionary<Type, IList>();
        }

        public static void Publish<TMessageType>(TMessageType messageType)
        {
            Initialize();
            
            var type = typeof(TMessageType);

            if (!Subscriber.ContainsKey(type)) return;
            var actionList = new List<Subscription<TMessageType>>(Subscriber[type].Cast<Subscription<TMessageType>>());

            foreach (var action in actionList)
            {
                action.PublisherAction(messageType);
            }
        }

        public static Subscription<TMessageType> Subscribe<TMessageType>(Action<TMessageType> action)
        {
            Initialize();
            
            var type = typeof(TMessageType);
            var actionDetail = new Subscription<TMessageType>(action/*, this*/);

            if (!Subscriber.TryGetValue(type, out var actionList))
            {
                actionList = new List<Subscription<TMessageType>> {actionDetail};
                Subscriber.Add(type, actionList);
            }
            else
            {
                actionList.Add(actionDetail);
            }

            return actionDetail;
        }

        public static void Unsubscribe<TMessageType>(Subscription<TMessageType> subscription)
        {
            Initialize();
            
            var type = typeof(TMessageType);
            if (Subscriber.ContainsKey(type))
            {
                Subscriber[type].Remove(subscription);
            }
        }
    }
}
