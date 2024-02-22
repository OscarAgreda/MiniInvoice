using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace DDDInvoicingCleanL.SharedKernel
{
    public sealed class JsonSerializerSettingsSingleton
    {
        private static readonly JsonSerializerSettings _instance = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            NullValueHandling   = NullValueHandling.Include,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
        };
        private JsonSerializerSettingsSingleton() { }
        public static JsonSerializerSettings Instance => _instance;
    }
}
