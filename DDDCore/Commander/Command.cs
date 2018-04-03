﻿using Commander.Domain;

namespace Commander
{
    public abstract class Command<TRequest, TModel> : CommandCommon<TRequest, TModel>,
        ICommand<TRequest, TModel> where TModel : IAggregateRoot
    {
        public virtual void Init()
        {
        }

        public abstract TModel Handle();        
    }

    public abstract class EventHandler<TRequest, TModel> : CommandCommon<TRequest, TModel>,
        ICommand<TRequest, TModel> where TModel : IAggregateRoot
    {
        public virtual void Init()
        {
        }

        public abstract TModel Handle();
    }
}