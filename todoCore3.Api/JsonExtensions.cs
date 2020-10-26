using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace todoCore3.Api
{
  public static class JsonExtensions
  {
    public static bool IsNullOrEmpty(this JToken token)
    {
      return (token == null) ||
          (token.Type == JTokenType.Array && !token.HasValues) ||
          (token.Type == JTokenType.Object && !token.HasValues) ||
          (token.Type == JTokenType.String && token.ToString() == string.Empty) ||
          (token.Type == JTokenType.Null);
    }

    public static string ToMetaValueString(this JToken token)
    {
      return token.IsNullOrEmpty() ? "" : token["value"].ToString();
    }
  }

  public class NullToEmptyStringResolver : DefaultContractResolver
  {
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
      return type.GetProperties()
          .Select(p =>
          {
            var jp = base.CreateProperty(p, memberSerialization);
            jp.ValueProvider = new NullToEmptyStringValueProvider(p);
            return jp;
          }).ToList();
    }
  }

  public class NullToEmptyStringValueProvider : IValueProvider
  {
    PropertyInfo _memberInfo;

    public NullToEmptyStringValueProvider(PropertyInfo memberInfo)
    {
      _memberInfo = memberInfo;
    }

    public object GetValue(object target)
    {
      object result = _memberInfo.GetValue(target);
      if (_memberInfo.PropertyType == typeof(string) && result == null) result = "";

      return result;
    }

    public void SetValue(object target, object value)
    {
      _memberInfo.SetValue(target, value);
    }
  }
}
