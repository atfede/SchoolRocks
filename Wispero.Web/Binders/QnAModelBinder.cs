
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wispero.Web.Models;

namespace Wispero.Web.Binders
{
    public class QnAModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return BindQnAModel(controllerContext.HttpContext.Request.Form, bindingContext.ModelState);
        }

        public static object BindQnAModel(NameValueCollection values, ModelStateDictionary modelState)
        {
            //TODO: Implement model binder for QuestionAndAnswerModel

            try
            {
                string question = values.Get("txtQuestion");
                string tags = values.Get("txtTags");
                string answer = values.Get("txtAnswer");

                return new QuestionAndAnswerModel
                {
                    Question = question,
                    Answer = answer,
                    Tags = tags
                };
            }
              catch (Exception)
            {
                modelState.AddModelError("Error", "Data incomplete");
                return null;
            }
            
        }
    }
}