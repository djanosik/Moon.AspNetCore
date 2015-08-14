using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Newtonsoft.Json;

namespace Moon.AspNet
{
    /// <summary>
    /// The helper used to serialize objects as JSON (resp. JavaScript objects).
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// Serializes the given object as JSON (resp. JavaScrpt object).
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="settings">The JSON serializer settings.</param>
        public static HtmlString Serialize(object value, JsonSerializerSettings settings = null)
        {
            Requires.NotNull(value, nameof(value));

            settings = settings ?? new JsonSerializerSettings();
            var json = new JsonHelper(new JsonOutputFormatter { SerializerSettings = settings });
            return json.Serialize(value);
        }
    }
}