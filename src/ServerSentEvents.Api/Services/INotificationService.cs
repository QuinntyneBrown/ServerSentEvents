using ServerSentEvents.Core.Models;
using System;
using System.Collections.Generic;

namespace ServerSentEvents.Api.Services
{
    public interface INotificationService
    {
        void Subscribe(Action<List<Order>> onNext);

        void OnNext(List<Order> value);
    }
}