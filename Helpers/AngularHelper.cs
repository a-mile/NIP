using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace NIP.Helpers
{
	public static class AngularHelperExtension
	{
		public static AngularHelper<TModel> Angular<TModel>(this IHtmlHelper<TModel> helper)
		{
			return new AngularHelper<TModel>(helper);
		}
	}

	public class AngularHelper<TModel>
	{
		private readonly IHtmlHelper<TModel> _htmlHelper;

		public AngularHelper(IHtmlHelper<TModel> helper)
		{
			_htmlHelper = helper;
		}

		public AngularModelHelper<TModel> ModelFor(string expressionPrefix)
		{
			return new AngularModelHelper<TModel>(_htmlHelper, expressionPrefix);
		}		

		public IHtmlContent ListGroupForModel(string expressionPrefix)
		{
			var modelHelper = ModelFor(expressionPrefix);

			var formGroupForMethodGeneric = typeof(AngularModelHelper<TModel>)
				.GetMethod("ListGroupFor");

			var wrapperTag = new TagBuilder("ul");
			wrapperTag.AddCssClass("list-group");

			foreach (var prop in typeof(TModel)
				.GetProperties(BindingFlags.Public | BindingFlags.Instance))
			{
				if (prop.GetCustomAttributes().OfType<HiddenInputAttribute>().Any()) continue;				

				var formGroupForProp = formGroupForMethodGeneric
					.MakeGenericMethod(prop.PropertyType);

				var propertyLambda = MakeLambda(prop);

				var formGroupTag = (HtmlString)formGroupForProp.Invoke(modelHelper, 
					new[] { propertyLambda });

				wrapperTag.InnerHtml.AppendHtml(formGroupTag);
			}

			return wrapperTag;
		}

		private object MakeLambda(PropertyInfo prop)
		{
			var parameter = Expression.Parameter(typeof(TModel), "x");
			var property = Expression.Property(parameter, prop);
			var funcType = typeof(Func<,>).MakeGenericType(typeof(TModel), prop.PropertyType);
			
			return Expression.Lambda(funcType, property, parameter);
		}				
	}
}