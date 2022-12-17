using System;
using System.Collections.Generic;

namespace Day14.Tools
{
    public static class EventAggregator
    {
        private static readonly IDictionary<Type, IList<ISubscriber>> _subscriptions = new Dictionary<Type, IList<ISubscriber>>();

        public static void Subscribe<TEventType>( object subscriberInstance, Action<TEventType> handler ) where TEventType : IEvent
        {
            if ( !_subscriptions.ContainsKey( typeof(TEventType) ) )
            {
                _subscriptions.Add(typeof(TEventType), new List<ISubscriber>());
            }
            
            Subscriber<TEventType> subscriber = new Subscriber<TEventType>( subscriberInstance, handler );
            
            _subscriptions[typeof(TEventType)].Add(subscriber);

        }

        public static void Publish<TEventType>( TEventType eventToPublish ) where TEventType : IEvent
        {
            if ( !_subscriptions.ContainsKey( typeof(TEventType) ) )
            {
                return;
            }

            foreach ( ISubscriber subscriber in _subscriptions[typeof(TEventType)] )
            {
                (subscriber as Subscriber<TEventType>)?.Handler?.Invoke(eventToPublish);
            }
        }
    }
}