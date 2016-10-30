using System;
using System.Runtime.Serialization;

namespace Models.Repositories
{
    [Serializable]
    public class DuplicateTodoItemException : Exception
    {
        private Guid id;
        private string v;

        public DuplicateTodoItemException()
        {
        }

        public DuplicateTodoItemException(string message) : base(message)
        {
        }

        public DuplicateTodoItemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DuplicateTodoItemException(string v, Guid id)
        {
            this.v = v;
            this.id = id;
        }

        protected DuplicateTodoItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}