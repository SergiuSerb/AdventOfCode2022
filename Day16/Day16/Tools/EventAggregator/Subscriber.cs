using System;

namespace Day16.Tools.EventAggregator
{
    public class Subscriber<TEventType> : ISubscriber where TEventType : IEvent
    {
        public object SubscriberInstance { get; }
        
        public Action<TEventType> Handler { get; }

        public Subscriber( object subscriberInstance, Action<TEventType> handler )
        {
            SubscriberInstance = subscriberInstance;
            Handler = handler;
        }

    }
}