﻿namespace GymApp.Database.Entities;

using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BodyType
{
    Ectomorph,
    Mesomorph,
    Endomorph,
}
