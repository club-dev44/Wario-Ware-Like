using System;

namespace Core
{
    public class GamesGenerationException : Exception
    {
        public GamesGenerationException(string message) : base(message) { }

        public GamesGenerationException(string message, Exception innerException) : base(message, innerException) { }
        
    }
}