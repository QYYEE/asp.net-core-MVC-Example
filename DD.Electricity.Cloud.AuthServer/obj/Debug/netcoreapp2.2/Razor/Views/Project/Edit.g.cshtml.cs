#pragma checksum "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "abf4dbfc57cdf9cfbe6040cf6b6024660563e125"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Project_Edit), @"mvc.1.0.view", @"/Views/Project/Edit.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Project/Edit.cshtml", typeof(AspNetCore.Views_Project_Edit))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\_ViewImports.cshtml"
using IdentityServer4.Quickstart.UI;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"abf4dbfc57cdf9cfbe6040cf6b6024660563e125", @"/Views/Project/Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"57b49bb18fbe88f2fb7e20eb130e64338d7f2c37", @"/Views/_ViewImports.cshtml")]
    public class Views_Project_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DD.Electricity.Cloud.AuthServer.Models.Project.EditProjectViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml"
  
    ViewData["Title"] = "Edit";

#line default
#line hidden
#line 5 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml"
  
    ViewBag.Title = "Edit";

#line default
#line hidden
            BeginContext(152, 15, true);
            WriteLiteral("<h2>Edit</h2>\r\n");
            EndContext();
#line 9 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml"
 using (Html.BeginForm())
{
    

#line default
#line hidden
            BeginContext(202, 23, false);
#line 11 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
            EndContext();
            BeginContext(227, 47, true);
            WriteLiteral("<div class=\"form-horizontal\">\r\n    <hr />\r\n    ");
            EndContext();
            BeginContext(275, 28, false);
#line 14 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml"
Write(Html.ValidationSummary(true));

#line default
#line hidden
            EndContext();
            BeginContext(303, 6, true);
            WriteLiteral("\r\n    ");
            EndContext();
            BeginContext(310, 33, false);
#line 15 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml"
Write(Html.HiddenFor(model => model.Id));

#line default
#line hidden
            EndContext();
            BeginContext(343, 42, true);
            WriteLiteral("\r\n\r\n    <div class=\"form-group\">\r\n        ");
            EndContext();
            BeginContext(386, 84, false);
#line 18 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml"
   Write(Html.LabelFor(model => model.ProjectName, new { @class = "control-label col-md-2" }));

#line default
#line hidden
            EndContext();
            BeginContext(470, 47, true);
            WriteLiteral("\r\n        <div class=\"col-md-10\">\r\n            ");
            EndContext();
            BeginContext(518, 42, false);
#line 20 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml"
       Write(Html.EditorFor(model => model.ProjectName));

#line default
#line hidden
            EndContext();
            BeginContext(560, 14, true);
            WriteLiteral("\r\n            ");
            EndContext();
            BeginContext(575, 53, false);
#line 21 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml"
       Write(Html.ValidationMessageFor(model => model.ProjectName));

#line default
#line hidden
            EndContext();
            BeginContext(628, 68, true);
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <div class=\"form-group\">\r\n        ");
            EndContext();
            BeginContext(697, 80, false);
#line 25 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml"
   Write(Html.LabelFor(model => model.GroupId, new { @class = "control-label col-md-2" }));

#line default
#line hidden
            EndContext();
            BeginContext(777, 47, true);
            WriteLiteral("\r\n        <div class=\"col-md-10\">\r\n            ");
            EndContext();
            BeginContext(824, 81, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "abf4dbfc57cdf9cfbe6040cf6b6024660563e1256733", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
#line 27 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.GroupId);

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 27 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = Model.Items;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(905, 14, true);
            WriteLiteral("\r\n            ");
            EndContext();
            BeginContext(920, 49, false);
#line 28 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml"
       Write(Html.ValidationMessageFor(model => model.GroupId));

#line default
#line hidden
            EndContext();
            BeginContext(969, 30, true);
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n");
            EndContext();
            BeginContext(1045, 187, true);
            WriteLiteral("    <div class=\"form-group\">\r\n        <div class=\"col-md-offset-2 col-md-10\">\r\n            <input type=\"submit\" value=\"更改\" class=\"btn btn-default\" />\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            EndContext();
#line 38 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml"
}

#line default
#line hidden
            BeginContext(1235, 11, true);
            WriteLiteral("<div>\r\n    ");
            EndContext();
            BeginContext(1247, 40, false);
#line 40 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Project\Edit.cshtml"
Write(Html.ActionLink("Back to List", "Index"));

#line default
#line hidden
            EndContext();
            BeginContext(1287, 10, true);
            WriteLiteral("\r\n</div>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DD.Electricity.Cloud.AuthServer.Models.Project.EditProjectViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591