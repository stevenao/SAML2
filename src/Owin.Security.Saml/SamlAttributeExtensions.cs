using System.Collections.Generic;
using System.Linq;
using SAML2.Schema.Core;
using System;
using System.Security.Claims;

namespace Owin.Security.Saml
{
    internal static class SamlAttributeExtensions
    {
        public static IEnumerable<Claim> ToClaims(this SamlAttribute value, string issuer)
        {
            if (value == null) throw new ArgumentNullException("value");
			return ( value.AttributeValue ?? Enumerable.Empty<string>() ).Select( attr => new Claim( value.Name, attr, value.NameFormat, issuer ) );
        }
    }
}
