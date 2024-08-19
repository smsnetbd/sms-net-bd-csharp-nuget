namespace SMS_Net_BD.Exceptions
{
    /// <summary>
    /// SMS Exception
    /// </summary>
    internal class SmsException : Exception
    {
        public SmsException(string message) : base(message) { }

        public SmsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
