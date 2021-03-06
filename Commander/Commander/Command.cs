﻿using AggregateRoot = Commander.Common.AggregateRoot;

namespace Commander
{
    public abstract class Command<TRequest, TModel> : CommandCommon<TRequest, TModel>,
        ICommand<TRequest, TModel> where TModel : AggregateRoot
    {
        public virtual void Init()
        {
        }

        public virtual TModel Handle() => Model;
    }

    public abstract class EventHandler<TRequest, TModel> : CommandCommon<TRequest, TModel>,
        ICommand<TRequest, TModel> where TModel : AggregateRoot
    {
        public virtual void Init()
        {
        }

        public virtual TModel Handle() => Model;
    }
}