using System;

namespace FrameworkEx
{
    public static class Raise
    {
        public static TResult Exception<TException, TResult>(TException exception)
            where TException : Exception
        {
            throw exception;
        }
        public static TResult NotSupported<TResult>(string message)
        {
            return Exception<NotSupportedException, TResult>(new NotSupportedException(message));
        }
        public static TResult NotSupportedEnumValue<TEnum, TResult>(TEnum src)
        {
            return Exception<NotSupportedException, TResult>(new NotSupportedException(string.Format("{0}: Value of {1} is not supported", typeof(TEnum).Name, src.GetEnumName())));
        }
    }
}