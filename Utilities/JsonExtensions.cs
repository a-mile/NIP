using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace NIP.Utilities
{
	public static class JsonExtensions
	{
		public static string ToCamelCaseName<TModel, TProp>(
			this Expression<Func<TModel, TProp>> property)
		{			
			var pascalCaseName = ExpressionHelper.GetExpressionText(property);
		
			var camelCaseName = ConvertFullNameToCamelCase(pascalCaseName);
			return camelCaseName;
		}
		
		private static string ConvertFullNameToCamelCase(string pascalCaseName)
		{
			var parts = pascalCaseName.Split('.')
				.Select(ConvertToCamelCase);

			return string.Join(".", parts);
		}

		private static string ConvertToCamelCase(string s)
		{
			if (string.IsNullOrEmpty(s))
				return s;
			if (!char.IsUpper(s[0]))
				return s;
			char[] chars = s.ToCharArray();
			for (int i = 0; i < chars.Length; i++)
			{
				bool hasNext = (i + 1 < chars.Length);
				if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
					break;
				chars[i] = char.ToLowerInvariant(chars[i]);
			}
			return new string(chars);
		}
	}
}