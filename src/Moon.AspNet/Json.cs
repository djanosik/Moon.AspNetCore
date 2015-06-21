﻿using System.IO;
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
        /// <param name="quoteNames">A value indicating whether names should be quoted.</param>
        public static HtmlString Serialize(object value, bool quoteNames = false)
        {
            Requires.NotNull(value, nameof(value));

            using (var writer = new StringWriter())
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                jsonWriter.QuoteName = quoteNames;

                var serializer = new JsonSerializer();
                serializer.Serialize(jsonWriter, value);

                return new HtmlString(writer.ToString());
            }
        }
    }
}