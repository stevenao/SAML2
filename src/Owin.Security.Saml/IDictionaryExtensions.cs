using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Owin.Security.Saml
{
    public static class IDictionaryExtensions
    {
        public static NameValueCollection ToNameValueCollection(this IDictionary<string, string> value)
        {
            if (value == null) throw new ArgumentNullException("value");
            var nvc = new NameValueCollection(value.Count);
            foreach (var item in value)
                nvc.Add(item.Key, item.Value);
            return nvc;
        }

        public static string ToDelimitedString(this IDictionary<string, string> value)
        {
            if (value == null) throw new ArgumentNullException("value");	
            return JsonSerialize( value );
        }

        public static IEnumerable<KeyValuePair<string,string>> FromDelimitedString(this string value)
        {
            if (value == null) throw new ArgumentNullException("value");
			IDictionary<string, string> dict = JsonDeserialize( value );
			return dict.ToList();
        }

		private static string JsonSerialize( IDictionary<string, string> value ) 
		{
			var ser = new DataContractJsonSerializer( typeof( IDictionary<string, string> ) );
			using( var ms = new MemoryStream() ) 
			{
				ser.WriteObject( ms, value );
				var jsonString = Encoding.UTF8.GetString( ms.ToArray() );
				return jsonString;
			}
		}

		private static IDictionary<string, string> JsonDeserialize(string value) 
		{
			var ser = new DataContractJsonSerializer( typeof( IDictionary<string, string> ) );
			using(var ms = new MemoryStream( Encoding.UTF8.GetBytes( value ) )) 
			{
				var obj = (IDictionary<string, string>) ser.ReadObject( ms );
				return obj;
			}
		}
    }
}
