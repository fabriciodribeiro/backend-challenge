using System;

namespace Challenge.Application.Common.ViewModel
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base() { }
    }
}
