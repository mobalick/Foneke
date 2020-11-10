using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using EMM.FoNeke.Common.BaseObjects;
using EMM.FoNeke.Common.Entities;
using EMM.FoNeke.Common.Extentions;

namespace EMM.FoNeke.Common.WebUI
{
    public class CitUIHtmlHelper<TModel> : HtmlHelper<TModel>
    {
        public CitUIHtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
            : base(viewContext, viewDataContainer)
        {
        }

        public CitUIHtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer,
            RouteCollection routeCollection)
            : base(viewContext, viewDataContainer, routeCollection)
        {
        }


        public MvcHtmlString TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes = null)
        {
            var helper = new AttributesHelper<TModel, TProperty>(this, expression, htmlAttributes);

            return new MvcHtmlString(string.Format(@"<div class='form-group'>
                                                            <label class='col-sm-4 control-label no-padding-right' for='{0}'>{2}&nbsp;:</label>
                                                            <div class='col-sm-8'>
                                                                <input type='text' id='{0}' placeholder='Username' value='{1}' class='col-xs-12 col-sm-12' {3} />
                                                            </div>
                                                        </div>",
                helper.FieldId,
                helper.Metadata.SimpleDisplayText,
                helper.Title,
                helper.Required));
        }


        public MvcHtmlString DropDownFor<TProperty>(Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes = null, List<SelectListItem> datasource = null)
        {
            var helper = new AttributesHelper<TModel, TProperty>(this, expression, htmlAttributes);
            var data = new StringBuilder("<option value=''>&nbsp;</option>");
            if (datasource != null)
            {
                foreach (SelectListItem currentItem in datasource)
                {
                    data.AppendLine(string.Format("<option value='{0}' >{1}</option>", currentItem.Value,
                        currentItem.Text));
                }
            }


            return new MvcHtmlString(string.Format(@"<div class='form-group'>
                                                        <label class='col-sm-4 control-label no-padding-right' for='{0}'>{1}&nbsp;:</label>
                                                        <div class='col-sm-8'>
                                                            <select class='form-control col-xs-12 col-sm-12' id='{0}' data-placeholder='Choose a Country...'>
                                                                {2}
                                                            </select>
                                                        </div>
                                                    </div>", helper.FieldId, helper.Title, data));
        }

        public MvcHtmlString DatePickerFor<TProperty>(Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes = null)
        {
            var helper = new AttributesHelper<TModel, TProperty>(this, expression, htmlAttributes);

            return new MvcHtmlString(string.Format(@"<div class='form-group'>
                                                        <label for='{0}' class='col-sm-4 control-label no-padding-right'>{2}&nbsp;:</label>
                                                        <div class='col-sm-8 input-group input-group-sm'>
                                                            <input type='text' class='form-control date-picker' id='{0}' value='{1}' data-date-format='{4}' {3}/>
                                                            <span class='input-group-addon'>
                                                                <i class='icon-calendar bigger-110'></i>
                                                            </span>
                                                        </div>
                                                    </div>",
                helper.FieldId,
                helper.Metadata.SimpleDisplayText,
                helper.Title,
                helper.Required,
                CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern));
        }

        public MvcHtmlString DateRangePickerFor(Expression<Func<TModel, DateRange>> expression,
            object htmlAttributes = null)
        {
            var helper = new AttributesHelper<TModel, DateRange>(this, expression, htmlAttributes);

            return new MvcHtmlString(string.Format(@"<div class='form-group'>
                                                        <label for='{0}' class='col-sm-4 control-label no-padding-right'>{2}&nbsp;:</label>
                                                        <div class='col-sm-8 input-group input-group-sm'>
                                                            <input type='text' class='form-control date-range-picker' id='{0}FromToString' value='{1}' data-date-format='{4}' {3}/>
                                                            <span class='input-group-addon'>
                                                                <i class='icon-calendar bigger-110'></i>
                                                            </span>
                                                        </div>
                                                    </div>",
                helper.FieldId,
                helper.Metadata.SimpleDisplayText,
                helper.Title,
                helper.Required,
                CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern));
        }


        public string GetFieldTemplateName(ModelMetadata metadata)
        {
            if (metadata.ModelType.IsSubclassOf(typeof (BaseEntity)))
            {
                return "Cartouche";
            }
            if (metadata.ModelType.IsAssignableFrom(typeof (DateTime)) ||
                metadata.ModelType.IsAssignableFrom(typeof (DateTime?)))
            {
                return "Date";
            }

            if (metadata.ModelType.IsAssignableFrom(typeof (bool)) ||
                metadata.ModelType.IsAssignableFrom(typeof (bool?)))
            {
                return "Bool";
            }

            if (metadata.ModelType.IsAssignableFrom(typeof (int)) || metadata.ModelType.IsAssignableFrom(typeof (int?)) ||
                metadata.ModelType.IsAssignableFrom(typeof (decimal)) ||
                metadata.ModelType.IsAssignableFrom(typeof (decimal?)) ||
                metadata.ModelType.IsAssignableFrom(typeof (float)) ||
                metadata.ModelType.IsAssignableFrom(typeof (float?)))
            {
                return "Number";
            }

            if (metadata.ModelType.IsEnum)
            {
                return "Enum";
            }

            return "String";
        }


        //public Column GridColumnFor<TProperty>(Expression<Func<TModel, TProperty>> expression,
        //                                      object htmlAttributes = null)
        //{
        //    var helper = new AttributesHelper<TModel, TProperty>(this, expression, htmlAttributes);
        //    var column = new Column(helper.Metadata.PropertyName);
        //    if (helper.Metadata.ModelType.IsSubclassOf(typeof(BaseEntity)))
        //    {
        //        column.SetCustomFormatter("GridDisplayBaseEntity");
        //    }
        //    if (helper.Metadata.ModelType.IsAssignableFrom(typeof(DateTime)) || helper.Metadata.ModelType.IsAssignableFrom(typeof(DateTime?)))
        //    {
        //        column.SetFormatter(Formatters.Date);
        //    }
        //    return column;
        //}


//CTRL + M + H


        public MvcHtmlString TextAreaFor<TProperty>(Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes = null)
        {
            var helper = new AttributesHelper<TModel, TProperty>(this, expression, htmlAttributes);
            return new MvcHtmlString(string.Format(@"<div class='form_row'>
                                                        <label class='field_name align_right' for='{0}'>
                                                            <div class='TitreLabel'>{2}</div>
                                                            <div class='sTitreLabel'>{5}</div>
                                                        </label>
                                                        <div class='field'>
                                                            <textarea id='{0}' name='{4}' rows='5' class='span4' value='{1}' {3}></textarea>
                                                        </div>
                                                    </div>", helper.FieldId,
                helper.Metadata.SimpleDisplayText,
                helper.Title,
                helper.Required,
                helper.FieldFullName,
                helper.LblHelpTxt));
        }

        public MvcHtmlString DropDownListFor<TProperty, TDataSource, TDataSourceTextProperty, TDataSourceValueProperty>(
            Expression<Func<TModel, TProperty>> expression,
            List<TDataSource> datasource,
            Expression<Func<TDataSource, TDataSourceTextProperty>> textProperty,
            Expression<Func<TDataSource, TDataSourceValueProperty>> valueProperty,
            object htmlAttributes = null)
        {
            return BuildSelect(false, expression, datasource, textProperty, valueProperty, htmlAttributes);
        }

        private MvcHtmlString BuildSelect<TProperty, TDataSource, TDataSourceTextProperty, TDataSourceValueProperty>(
            bool multiple,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<TDataSource> datasource,
            Expression<Func<TDataSource, TDataSourceTextProperty>> textProperty,
            Expression<Func<TDataSource, TDataSourceValueProperty>> valueProperty,
            object htmlAttributes = null)
        {
            var helper = new AttributesHelper<TModel, TProperty>(this, expression, htmlAttributes);
            string multipleStr = multiple ? " multiple: true," : "maximumSelectionSize:1,";
            string multipleInputStr = "";
            string multipleChangeScript = string.Format(@"if(e.added){{
                                          $('[name=""{0}['+e.added.index+'].Id""]').val(e.added.id); 
                                          $('[name=""{0}['+e.added.index+'].Name""]').val(e.added.text); 
                                       }}else if(e.removed){{
                                          $('[name=""{0}['+e.removed.index+'].Id""]').val('');
                                          $('[name=""{0}['+e.removed.index+'].Name""]').val(''); 
                                        }}", helper.FieldFullName);

            #region DataSource

            var data = new List<string>();
            Func<TDataSource, TDataSourceValueProperty> valueFunc = valueProperty.Compile();
            Func<TDataSource, TDataSourceTextProperty> textFunc = textProperty.Compile();
            string requiredScript = string.Format("$('#{0}-control-group :input').attr('required','required')",
                helper.FieldId);
            string requiredScriptOnChange = string.Format(@"if(e.added.text)
                                                           {{$('#{0}-control-group :input').removeAttr('required'); }}
                                                       else{{$('#{0}-control-group :input').attr('required','required'); }}",
                helper.FieldId);
            if (datasource != null)
            {
                int i = 0;
                foreach (TDataSource currentItem in datasource)
                {
                    TDataSourceValueProperty valueMember = valueFunc(currentItem);
                    TDataSourceTextProperty textMember = textFunc(currentItem);

                    multipleInputStr +=
                        string.Format("<input type='hidden'  name='{0}[{1}].Id'  />" +
                                      "<input type='hidden'  name='{0}[{1}].Name'  />",
                            helper.FieldFullName, i);

                    data.Add(string.Format(" {{ id: '{0}', text: '{1}',index:'{2}' }}", valueMember, textMember, i));
                    i++;
                }
            }
            string js = string.Format(@"<script>
                                        $('#{0}Id').select2({{
                                                  width: 'resolve',
                                                  {4}
                                                  placeholder: 'Selectionner',
                                                  
                                                  allowClear: true,
                                                  data: [
                                                      {1}
                                                  ]
                                              }});
                                        $('#{0}Id').on('select2-removed',function(e){{
                                                                   {3}
                                                                }});

                                        $('#{0}Id').on('change',function(e){{
                                                                   {2}
                                                                   if(e.added)
                                                                       $('#{0}Name').val(e.added.text);
                                                                   if(e.removed)
                                                                       $('#{0}Name').val();
                                                                   {5}
                                                                }});
                                        {3}
                                    </script>", helper.FieldId, string.Join(",", data)
                , string.IsNullOrWhiteSpace(helper.Required) ? "" : requiredScriptOnChange
                , string.IsNullOrWhiteSpace(helper.Required) ? "" : requiredScript, multipleStr,
                multiple ? multipleChangeScript : "");

            #endregion DataSource

            dynamic id = helper.Metadata.Model == null ? "" : ((dynamic) helper.Metadata.Model).Key;
            dynamic name = helper.Metadata.Model == null ? "" : ((dynamic) helper.Metadata.Model).Value;

            return new MvcHtmlString(string.Format(@"<div class='control-group' id='{0}-control-group' >
                                                            <label class='control-label' for='{0}Id'>{3}</label>
                                                            <div class='controls'>
                                                                <input type='hidden' class='span12' id='{0}Id' name='{5}.Id' value='{1}' {4}/>
                                                                <input type='hidden' class='span12' id='{0}Name' name='{5}.Name' value='{6}' {4}/>
                                                                {7}
                                                            </div>
                                                        </div> 
                                                     {2}", helper.FieldId,
                id, js,
                helper.Title,
                helper.Required,
                helper.FieldFullName, name, multiple ? multipleInputStr : ""));
        }

        public MvcHtmlString SelectListFor<TProperty, TDataSource, TDataSourceTextProperty, TDataSourceValueProperty>(
            Expression<Func<TModel, TProperty>> expression,
            List<TDataSource> datasource,
            Expression<Func<TDataSource, TDataSourceTextProperty>> textProperty,
            Expression<Func<TDataSource, TDataSourceValueProperty>> valueProperty,
            object htmlAttributes = null)
        {
            return BuildSelect(true, expression, datasource, textProperty, valueProperty, htmlAttributes);
        }


        public MvcHtmlString LabelFor<TProperty>(Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes = null)
        {
            var attributesHelper = new AttributesHelper<TModel, TProperty>(this, expression, htmlAttributes);
            string pt = ":";
            if (string.IsNullOrWhiteSpace(attributesHelper.Title))
            {
                pt = "";
            }
            if (attributesHelper.Metadata.ModelType == typeof (bool) ||
                attributesHelper.Metadata.ModelType == typeof (bool?))
            {
                attributesHelper.Metadata.SimpleDisplayText = attributesHelper.Metadata.SimpleDisplayText.ToLocalized();
            }

            if (string.IsNullOrWhiteSpace(attributesHelper.Metadata.SimpleDisplayText))
            {
                attributesHelper.Metadata.SimpleDisplayText = "EmptyValue".ToLocalized();
            }


            return
                new MvcHtmlString(string.Format(@"<b>{0} {4} </b> <span data-label-id='{2}' name='{3}'>{1}</span>",
                    attributesHelper.Title,
                    attributesHelper.Metadata.SimpleDisplayText, attributesHelper.FieldId,
                    attributesHelper.FieldFullName, pt));
        }

        /// <summary>
        ///     TODO SetValues For Radios And CheckBoxes and implementation of html5 required attribute
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public MvcHtmlString RadioButtonFor<TProperty>(Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes = null)
        {
            var helper = new AttributesHelper<TModel, TProperty>(this, expression, htmlAttributes);

            string nullValue = Nullable.GetUnderlyingType(helper.Metadata.ModelType) != null
                ? string.Format(
                    "<label class='radio inline'><input id='{0}' name='{1}' type='radio' value='null'> n/c</label>",
                    helper.FieldId,
                    helper.FieldFullName)
                : "";

            return new MvcHtmlString(string.Format(@"<div class='control-group'>
                                                            <label class='control-label' for='{0}'>{2} : </label>
                                                            <div class='controls'>
                                                                <label class='radio inline'><input id='{0}' name='{4}' type='radio'  {3} value='true'> Oui</label>
                                                                <label class='radio inline'><input id='{0}' name='{4}' type='radio' value='false'> Non</label>
                                                                {5}
                                                            </div>
                                                        </div>", helper.FieldId,
                helper.Metadata.SimpleDisplayText,
                helper.Title,
                helper.Required,
                helper.FieldFullName, nullValue));
        }


        public GridBuilder<TProperty> CITKendoGridFor<TProperty>(Expression<Func<TModel, List<TProperty>>> expression,
            object htmlAttributes = null) where TProperty : BaseEntity
        {
            var name = expression.Body.ToString().Split('.').Last();


             //var helper = new AttributesHelper<TModel, TProperty>(this, expression, htmlAttributes);
            var grid = this.Kendo().Grid<TProperty>()
                .Name(string.Format("grid{0}", name))
                .Columns(columns => columns.Template(t => t.Id)
                    .ClientTemplate(string.Format("<input type='checkbox' data-ui-id='#=data.Id#' class='grid-select-row' data-grid-entity-name='{0}' />",name))
                    .HeaderTemplate(string.Format(@"<input type='checkbox' id='grid-select-all' name='grid-select-all' onclick='checkAll(this)' data-grid-entity-name='{0}'/> 
                                                    <input type='hidden' id='{0}SelectedIds' name='{0}SelectedIds' data-val='true' data-val-required='Le champ Start est requis.' />
                                                    <div class='red col-sm-8'>
                                                        <span class='field-validation-error' data-valmsg-for='{0}SelectedIds' data-valmsg-replace='true'><span for='{0}SelectedIds' class=''></span></span>
                                                    </div>", name))
                    .Width(10))
                .Pageable(pageable => pageable.ButtonCount(5))
                .Selectable(selectable => selectable
                    .Mode(GridSelectionMode.Multiple)
                    .Type(GridSelectionType.Row))
                .Events(e => e.DataBound("gridDataBound"))
                .Navigatable()
                .DataSource(dataSource => dataSource
                    .Ajax()
                    .PageSize(5)
                    //.Read(read => read.Action("GetStudentsByTeacher", "Notification"))
                );

            return grid;
        }


        
    }
}