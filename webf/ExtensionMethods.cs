

using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;


namespace webf
{
    public static class ExtensionMethods
    {
        public static int pageNum = 1;
        public static MvcHtmlString NavigatorUserHelper(this AjaxHelper html,int elementsCount,int currPage,string actionName,string controllerName)
        {
            int pageRows = 5;
            int rows = 10;

            int pageAmount = framesAmount(elementsCount, pageRows);
            int frameAmount = framesAmount(pageAmount, rows);

            TagBuilder divtag = new TagBuilder("div");
            divtag.MergeAttribute("id", "PageDiv");

            TagBuilder ulTag = new TagBuilder("ul");
            ulTag.MergeAttribute("id", "pagination-digg");

            TagBuilder liTag = new TagBuilder("li");
            liTag.MergeAttribute("class", "previous");
            
            TagBuilder innerDiv = new TagBuilder("div");
            innerDiv.MergeAttribute("id","innerPage");

            TagBuilder _nextBtn = new TagBuilder("a");
            _nextBtn.MergeAttributes(new Dictionary<string,string> { { "id", "nextblock" }, { "href", "" } });
            _nextBtn.SetInnerText(">>>");

            TagBuilder _prevBtn = new TagBuilder("a");
            _prevBtn.MergeAttributes(new Dictionary<string, string> { { "id", "prevblock" }, { "href", "" } });
            _prevBtn.SetInnerText("<<<");

            
            liTag.InnerHtml =   _prevBtn.ToString(TagRenderMode.Normal) + 
                                innerDiv.ToString(TagRenderMode.Normal) +
                                _nextBtn.ToString(TagRenderMode.Normal);

            ulTag.InnerHtml = liTag.ToString(TagRenderMode.Normal);
            divtag.InnerHtml = ulTag.ToString(TagRenderMode.Normal);

            return new MvcHtmlString(operationalPartScript(controllerName, actionName) + 
                                        pageNsvScript(pageAmount, frameAmount) + 
                                        divtag.ToString(TagRenderMode.Normal));
        }

        private static string pageNsvScript(int pageAmount,int frameAmount)
        {
            TagBuilder script = new TagBuilder("script");
            script.MergeAttribute("type", "text/javascript");

            StringBuilder javaScript = new StringBuilder("");

            
            
            javaScript.Append("$(document).ready(function () {");
            javaScript.Append("var per = 1;");
            javaScript.Append(string.Format("var pageBoxes = createPage({0}, per);",pageAmount));
            javaScript.Append("var prevBlock = $('#PageDiv #prevblock');");
            javaScript.Append("var nextBlock = $('#PageDiv #nextblock');");
            javaScript.Append("nextBlock.click(function (e) {");
            javaScript.Append("$('#innerPage li').each(function () {");
            javaScript.Append("        $(this).remove();");
            javaScript.Append("    });");
            javaScript.Append("    e.preventDefault();");
                
            javaScript.Append(string.Format("    if(per !=  {0})",frameAmount));
            javaScript.Append("        per++;");
                
            javaScript.Append(string.Format("    pageBoxes = createPage({0}, per);",pageAmount));
            javaScript.Append(string.Format("    initBoxes(per, {0}, '#PageDiv #innerPage', pageBoxes,prevBlock,nextBlock);",frameAmount));
                
            javaScript.Append("});");
            javaScript.Append("prevBlock.click(function (e) {");
            javaScript.Append("    $('#innerPage li').each(function () {");
            javaScript.Append("        $(this).remove();");
            javaScript.Append("    });");
            javaScript.Append("    e.preventDefault();");
                
            javaScript.Append("    if(per !=  1)");
            javaScript.Append("        per--;");
                
            javaScript.Append(string.Format("    pageBoxes = createPage({0}, per);",pageAmount));
            javaScript.Append(string.Format("    initBoxes(per, {0}, '#PageDiv #innerPage', pageBoxes,prevBlock,nextBlock);",frameAmount));
            javaScript.Append("});");

            javaScript.Append(string.Format("initBoxes(per, {0}, '#PageDiv #innerPage', pageBoxes,prevBlock,nextBlock);",frameAmount));

            javaScript.Append("});");
            script.InnerHtml = javaScript.ToString();
                return script.ToString(TagRenderMode.Normal);
        }

        private static string operationalPartScript(string actionName, string controllerName)
        {
            TagBuilder script = new TagBuilder("script");
            script.MergeAttribute("type", "text/javascript");

            StringBuilder javaScript = new StringBuilder("");

            javaScript.Append("function clickMe(pagenum) {");
            javaScript.Append(string.Format("    $('#ajaxDiv').load('/{0}/{1}'", actionName, controllerName) + ", { Page: pagenum });");
            javaScript.Append("}");

            script.InnerHtml = javaScript.ToString();
            return script.ToString(TagRenderMode.Normal);
        }

        private static int framesAmount(int lenghOfdata, int _frameLenght)
        {
            int operationValue = 0;

            if (lenghOfdata < _frameLenght) lenghOfdata = _frameLenght;
            else
            {
                operationValue = ((lenghOfdata / _frameLenght) * _frameLenght);
                lenghOfdata = operationValue < lenghOfdata ?
                    (operationValue + _frameLenght) : lenghOfdata;
            }
            return lenghOfdata / _frameLenght;
        }
    }
}