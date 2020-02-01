using System;

namespace Utilities.Publisher_Subscriber_System {
    public class Subscription<TMessage> : IDisposable
    {
        public Action<TMessage> PublisherAction { get; private set; }
        //private readonly PublisherSubscriber publisherSubscriber;
        private bool isDisposed;

        public Subscription(Action<TMessage> publisherAction/*, PublisherSubscriber publisherSubscriber*/)
        {
            PublisherAction = publisherAction;
            //this.publisherSubscriber = publisherSubscriber;
        }
        
        ~Subscription()
        {
            if (!isDisposed)
            {
                Dispose();
            }    
        }
        public void Dispose()
        {
            //publisherSubscriber.Unsubscribe(this);
            PublisherSubscriber.Unsubscribe(this);
            isDisposed = true;
        }
    }
}