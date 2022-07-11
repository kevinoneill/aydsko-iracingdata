﻿// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Runtime.Serialization;

namespace Aydsko.iRacingData.Exceptions;

[Serializable]
public class iRacingUnauthorizedResponseException : iRacingDataClientException
{
    public static iRacingUnauthorizedResponseException Create()
    {
        return new("Requested unauthorized.");
    }

    public iRacingUnauthorizedResponseException()
    { }

    public iRacingUnauthorizedResponseException(string message)
        : base(message)
    { }

    public iRacingUnauthorizedResponseException(string message, Exception innerException)
        : base(message, innerException)
    { }

    protected iRacingUnauthorizedResponseException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    { }
}
