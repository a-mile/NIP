using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using NIP.Utilities;

namespace NIP.Helpers
{
    public class AngularModelHelper<TModel>
    {
        protected readonly IHtmlHelper Helper;
        private readonly string _expressionPrefix;

        public AngularModelHelper(IHtmlHelper helper, string expressionPrefix)
        {
            Helper = helper;
            _expressionPrefix = expressionPrefix;
        }

        public HtmlString ExpressionFor<TProp>(Expression<Func<TModel, TProp>> property)
        {
            var expressionText = ExpressionForInternal(property);
            return new HtmlString(expressionText);
        }

        private string ExpressionForInternal<TProp>(Expression<Func<TModel, TProp>> property)
        {
            var camelCaseName = property.ToCamelCaseName();

            var expression = !string.IsNullOrEmpty(_expressionPrefix)
                ? _expressionPrefix + "." + camelCaseName
                : camelCaseName;

            return expression;
        }

        public HtmlString BindingFor<TProp>(Expression<Func<TModel, TProp>> property)
        {
            return new HtmlString("{{" + ExpressionForInternal(property) + "}}");
        }


        public HtmlString ListGroupFor<TProp>(Expression<Func<TModel, TProp>> property)
        {						
			var modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(property,(ViewDataDictionary<TModel>)Helper.ViewData,Helper.MetadataProvider);	
			var metadata = modelExplorer.Metadata;		
            var name = ExpressionHelper.GetExpressionText(property);
            var expression = ExpressionForInternal(property);

            return new HtmlString("<li class=\"list-group-item\">" +
                                "<label>" + (metadata.DisplayName ?? name) + "</label>" +
                                "<div>" + "{{" + expression + "}}" + "</div>"
                                + "</li>");
										
        }
    }


}