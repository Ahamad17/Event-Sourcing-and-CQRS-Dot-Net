using System;

namespace CQRS.Core.Events
{
    public abstract class BaseEvent
    {
        public Guid Id { get; set; }
        protected BaseEvent(string type)
        {
            this.Type = type;
        }
        public int Version { get; set; }
        public string Type { get; set; }
    }
}
