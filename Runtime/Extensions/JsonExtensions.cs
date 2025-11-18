using Newtonsoft.Json;
using System;
using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.Extensions
{
    public static class JsonExtensions
    {
        public static T DeserializeOrDefault<T>(this object data) where T : new()
        {
            // If empty data, return the default generic value.
            if (data == null)
            {
                return new T();
            }

            // If already de-serialized, return cast value
            if (data is T existing)
            {
                return existing;
            }

            // Try to de-serialize data.
            try
            {
                return JsonConvert.DeserializeObject<T>(data.ToString());
            }
            catch (JsonReaderException ex)
            {
                Debug.LogError($"{nameof(JsonExtensions)}: Invalid JSON format: {ex.Message}");
            }
            catch (JsonSerializationException ex)
            {
                Debug.LogError($"{nameof(JsonExtensions)}: Serialization error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameof(JsonExtensions)}: Unexpected error: {ex}");
            }

            return new T();
        }
    }
}