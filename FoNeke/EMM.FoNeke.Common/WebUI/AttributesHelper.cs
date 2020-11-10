using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Web.Mvc;

namespace EMM.FoNeke.Common.WebUI
{
    public class AttributesHelper<TModel, TProperty>
    {
        private ResourceManager _resourceManager;

        public AttributesHelper(CitUIHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes)
        {
            Helper = helper;
            Metadata = GetModelMetadata(expression);
            FieldFullName = GeNameFormExpression(expression);
            FieldId = GeNameFormExpression(expression).Replace(".", "_");

            SetHtmlAttributes(htmlAttributes);
        }

        public AttributesHelper(object htmlAttributes)
        {
            SetHtmlAttributes(htmlAttributes);
        }

        #region InternalMethods

        public void SetHtmlAttributes(object htmlAttributes)
        {
            ChampInvalidMessage = ResourceManager.GetString("ChampInvalidMessage");
            Yes = ResourceManager.GetString("Yes");
            No = ResourceManager.GetString("No");
            Title = GetValueFromHtmlAttribute(htmlAttributes, "Title");
            if (!string.IsNullOrWhiteSpace(Title))
            {
                if (!Title.Equals("Empty"))
                {
                    Title = ResourceManager.GetString(Title);
                    if (string.IsNullOrWhiteSpace(Title))
                    {
                        Title = GetValueFromHtmlAttribute(htmlAttributes, "Title");
                    }
                }
                else
                {
                    Title = "";
                }
            }
            else
            {
                if (Metadata != null)
                {
                    Title = Metadata.DisplayName;
                    if (!string.IsNullOrWhiteSpace(Title))
                    {
                        Title = ResourceManager.GetString(Title);
                        if (string.IsNullOrWhiteSpace(Title))
                        {
                            Title = Metadata.DisplayName;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(Title))
                    {
                        Title = Metadata.PropertyName;
                        if (!string.IsNullOrWhiteSpace(Title))
                        {
                            Title = ResourceManager.GetString(Title);
                            if (string.IsNullOrWhiteSpace(Title))
                            {
                                Title = Metadata.PropertyName;
                            }
                        }
                    }
                }
            }
            if (htmlAttributes == null)
                return;

            Required = GetValueFromHtmlAttribute(htmlAttributes, "Required");
            EntityId = GetValueFromHtmlAttribute(htmlAttributes, "EntityId");
            Type = GetValueFromHtmlAttribute(htmlAttributes, "Type").ToLower();
            Pattern = GetValueFromHtmlAttribute(htmlAttributes, "Pattern");
            ReadOnly = GetValueFromHtmlAttribute(htmlAttributes, "ReadOnly");
            LblHelpTxt = GetValueFromHtmlAttribute(htmlAttributes, "LblHelpTxt");

            string disabledValue = GetValueFromHtmlAttribute(htmlAttributes, "Disabled");
            if (!string.IsNullOrWhiteSpace(disabledValue))
            {
                Disabled = string.Format("Disabled='{0}'", disabledValue);
            }
            //Asterix Required sur le title
            if (!string.IsNullOrWhiteSpace(Required))
            {
                Title = string.Format("{0} <span class='required'>*</span>", Title);
            }
        }

        internal string GetValueFromHtmlAttribute(object htmlAttributes, string name)
        {
            if (htmlAttributes != null && htmlAttributes.GetType().GetProperty(name) != null)
                return (string) htmlAttributes.GetType().GetProperty(name).GetValue(htmlAttributes, null);
            return "";
        }

        internal ModelMetadata GetModelMetadata(Expression<Func<TModel, TProperty>> expression)
        {
            return ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>(Helper.ViewData));
        }

        internal string GetDateFromValue(string value)
        {
            DateTime date;
            return DateTime.TryParse(value, out date)
                ? string.Format("value: new Date({0}, {1}, {2})", date.Year, date.Month, date.Day)
                : "";
        }

        internal string GetDateFromValue()
        {
            return GetDateFromValue(Metadata.SimpleDisplayText);
        }

        internal string GeNameFormExpression(Expression<Func<TModel, TProperty>> expression)
        {
            return expression.Body.ToString()
                .Substring(expression.Body.ToString().IndexOf('.') + 1, expression.Body.ToString().Length - 2);
        }

        #endregion

        public string Title { get; set; }
        public string Yes { get; set; }
        public string No { get; set; }
        public string Required { get; set; }
        public string EntityId { get; set; }
        public string Disabled { get; set; }


        public string ReadOnly { get; set; }
        public string FieldFullName { get; set; }
        public string FieldId { get; set; }

        public string LblHelpTxt { get; set; }
        public string ChampInvalidMessage { get; set; }


        /// <summary>
        ///     A regex string for the field's html5 validation
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        ///     set this to with the folowing html 5 input types
        ///     color
        ///     date
        ///     datetime
        ///     datetime-local
        ///     month
        ///     search
        ///     tel
        ///     time
        ///     week
        /// </summary>
        public string Type { get; set; }

        private ResourceManager ResourceManager
        {
            get
            {
                if (_resourceManager == null)
                {
                    Assembly localisationAssembly = Assembly.Load("Cit.IBattuta.Resources");
                    _resourceManager = new ResourceManager("Cit.IBattuta.Resources.Strings", localisationAssembly);
                }
                return _resourceManager;
            }
        }


        public CitUIHtmlHelper<TModel> Helper { get; set; }
        public ModelMetadata Metadata { get; set; }
    }
}