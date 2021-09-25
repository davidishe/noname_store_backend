﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MyAppBack.Models.Payment
{
  public static class Serializer
  {
    public static T DeserializeObject<T>(string data) => JsonConvert.DeserializeObject<T>(data, SerializerSettings);

    public static string SerializeObject(object value) => value == null ? "" : JsonConvert.SerializeObject(value, SerializerSettings);

    private static readonly IContractResolver ContractResolver = new DefaultContractResolver()
    {
      NamingStrategy = new SnakeCaseNamingStrategy()
    };

    private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
    {
      ContractResolver = ContractResolver,
      Formatting = Formatting.None,
      NullValueHandling = NullValueHandling.Ignore,
    };
  }
}
