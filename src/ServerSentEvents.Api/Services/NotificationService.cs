using ServerSentEvents.Core.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace ServerSentEvents.Api.Services
{
    public class NotificationService: INotificationService
    {
        private readonly BehaviorSubject<List<Order>> _orders = new BehaviorSubject<List<Order>>(null);

        public void Subscribe(Action<List<Order>> onNext)
        {
            _orders.Subscribe(onNext);
        }

        public void OnNext(List<Order> value)
        {
            _orders.OnNext(value);
        }

    }
}
