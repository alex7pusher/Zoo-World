using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UniRx;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Events
{
    [UsedImplicitly]
    public sealed class ZooEventBus : IZooEventBus, IDisposable
    {
        private readonly Dictionary<Type, object> _subjects = new Dictionary<Type, object>();

        public IObservable<TEvent> Receive<TEvent>()
        {
            return GetSubject<TEvent>();
        }

        public void Publish<TEvent>(TEvent eventData)
        {
            GetSubject<TEvent>().OnNext(eventData);
        }

        public void Dispose()
        {
            foreach (object subject in _subjects.Values)
            {
                if (subject is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            _subjects.Clear();
        }

        private ISubject<TEvent> GetSubject<TEvent>()
        {
            Type eventType = typeof(TEvent);
            if (_subjects.TryGetValue(eventType, out object existingSubject))
            {
                return (ISubject<TEvent>)existingSubject;
            }

            Subject<TEvent> subject = new Subject<TEvent>();
            _subjects.Add(eventType, subject);
            return subject;
        }
    }
}
