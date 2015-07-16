using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Threading.Tasks;

namespace Glimpse.Agent
{ 
    public class DefaultAgentBroker : IAgentBroker
    {
        private readonly ISubject<object> _subject;

        // TODO: Review if we care about unifying which thread message is published on
        //       and which thread it is recieved on. If so need to use IScheduler.

        public DefaultAgentBroker(IChannelSender channelSender)
        {
            _subject = new BehaviorSubject<object>(null);

            // TODO: This probably shouldn't be here but don't want to setup 
            //       more infrasture atm. Deciding whether it should be users 
            //       code which instantiates listners or if we provider infrasturcture
            //       which starts them up and triggers the subscription. 
            ListenAll().Subscribe(async msg => await channelSender.PublishMessage(msg));
        }

        public IObservable<T> Listen<T>() 
        {
            return ListenIncludeLatest<T>().Skip(1);
        }

        public IObservable<T> ListenIncludeLatest<T>() 
        {
            return _subject
                .Where(msg => typeof(T).GetTypeInfo().IsAssignableFrom(msg.GetType().GetTypeInfo()))
                .Select(msg => (T)msg);
        }
        public IObservable<object> ListenAll()
        {
            return ListenAllIncludeLatest().Skip(1);
        }

        public IObservable<object> ListenAllIncludeLatest()
        {
            return _subject;
        }

        public async Task SendMessage(object payload)
        {
            await Task.Run(() => _subject.OnNext(payload));
        }
    }
}