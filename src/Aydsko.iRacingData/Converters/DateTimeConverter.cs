﻿// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Converters;

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader,
                                  Type typeToConvert,
                                  JsonSerializerOptions options)
    {
        var dateString = reader.GetString()?.Trim();

        if (dateString is null or { Length: 0 })
        {
            return default;
        }

        var dateValue = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        return dateValue;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Check is being done, diagnostic requires an update.")]
    public override void Write(Utf8JsonWriter writer!!,
                               DateTime value,
                               JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
    }
}