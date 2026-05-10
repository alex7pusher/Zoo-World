using System;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Events
{
    public interface IZooEventBus
    {
        IObservable<TEvent> Receive<TEvent>();
        void Publish<TEvent>(TEvent eventData);
    }
}
