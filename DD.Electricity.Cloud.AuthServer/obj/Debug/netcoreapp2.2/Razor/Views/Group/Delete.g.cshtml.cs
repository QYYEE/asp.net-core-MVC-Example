#pragma checksum "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Group\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "82e192deba625d660b6792a100c078c3c4f6c678"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Group_Delete), @"mvc.1.0.view", @"/Views/Group/Delete.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Group/Delete.cshtml", typeof(AspNetCore.Views_Group_Delete))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"82e192deba625d660b6792a100c078c3c4f6c678", @"/Views/Group/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"57b49bb18fbe88f2fb7e20eb130e64338d7f2c37", @"/Views/_ViewImports.cshtml")]
    public class Views_Group_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DD.Electricity.Cloud.Domain.Entities.Headquarter.Group>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Group\Delete.cshtml"
  
    ViewData["Title"] = "Delete";

#line default
#line hidden
            BeginContext(105, 148, true);
            WriteLiteral("<h1>Delete</h1>\r\n<h3>Are you sure you want to delte this?</h3>\r\n<div>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(254, 38, false);
#line 11 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Group\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Id));

#line default
#line hidden
            EndContext();
            BeginContext(292, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(354, 34, false);
#line 14 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Group\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Id));

#line default
#line hidden
            EndContext();
            BeginContext(388, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(449, 40, false);
#line 17 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Group\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(489, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(551, 36, false);
#line 20 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Group\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(587, 28, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n");
            EndContext();
#line 23 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Group\Delete.cshtml"
     using (Html.BeginForm())
    {
        

#line default
#line hidden
            BeginContext(662, 23, false);
#line 25 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Group\Delete.cshtml"
   Write(Html.AntiForgeryToken());

#line default
#line hidden
            EndContext();
            BeginContext(687, 132, true);
            WriteLiteral("        <div class=\"form-action no-color\">\r\n            <input type=\"submit\" value=\"Delete\" class=\"btn btn-default\" />\r\n            ");
            EndContext();
            BeginContext(820, 39, false);
#line 28 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Group\Delete.cshtml"
       Write(Html.ActionLink("Back to List","Index"));

#line default
#line hidden
            EndContext();
            BeginContext(859, 20, true);
            WriteLiteral("\r\n\r\n        </div>\r\n");
            EndContext();
#line 31 "E:\鼎鼎安全公司\DDCloudAPI\DD.Electricity.Cloud.AuthServer\Views\Group\Delete.cshtml"
    }

#line default
#line hidden
            BeginContext(886, 10, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DD.Electricity.Cloud.Domain.Entities.Headquarter.Group> Html { get; private set; }
    }
}
#pragma warning restore 1591