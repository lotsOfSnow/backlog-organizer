﻿namespace BacklogOrganizer.Shared.Core.Exceptions;
public class CustomException : Exception
{
    protected CustomException()
    {
    }

    protected CustomException(string? message) : base(message)
    {
    }

    protected CustomException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
