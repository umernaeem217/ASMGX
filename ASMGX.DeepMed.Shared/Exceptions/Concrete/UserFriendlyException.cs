using ASMGX.DeepMed.Shared.Exceptions.Interfaces;
using System.Runtime.Serialization;

namespace ASMGX.DeepMed.Shared.Exceptions.Concrete
{
    public class UserFriendlyException : Exception, IException
    {
        public UserFriendlyException()
        {
        }

        public UserFriendlyException(string? message) : base(message)
        {
        }

        public UserFriendlyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserFriendlyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
