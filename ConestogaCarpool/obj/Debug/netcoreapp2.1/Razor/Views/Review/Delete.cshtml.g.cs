#pragma checksum "C:\Users\Lisda\Documents\School\Fall 2019\MS Enterprice\GroupFive_Iteration3\ConestogaCarpool\ConestogaCarpool\Views\Review\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0123c975a75e3d6bdac8c07249d98b4b510af321"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Review_Delete), @"mvc.1.0.view", @"/Views/Review/Delete.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Review/Delete.cshtml", typeof(AspNetCore.Views_Review_Delete))]
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
#line 1 "C:\Users\Lisda\Documents\School\Fall 2019\MS Enterprice\GroupFive_Iteration3\ConestogaCarpool\ConestogaCarpool\Views\_ViewImports.cshtml"
using ConestogaCarpool;

#line default
#line hidden
#line 2 "C:\Users\Lisda\Documents\School\Fall 2019\MS Enterprice\GroupFive_Iteration3\ConestogaCarpool\ConestogaCarpool\Views\_ViewImports.cshtml"
using ConestogaCarpool.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0123c975a75e3d6bdac8c07249d98b4b510af321", @"/Views/Review/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1bcb33f09722f6e897bb3047affcb0f70986625b", @"/Views/_ViewImports.cshtml")]
    public class Views_Review_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ConestogaCarpool.Models.Review>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(39, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\Lisda\Documents\School\Fall 2019\MS Enterprice\GroupFive_Iteration3\ConestogaCarpool\ConestogaCarpool\Views\Review\Delete.cshtml"
  
    ViewData["Title"] = "Delete";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(130, 252, true);
            WriteLiteral("<div class=\"allBodyLeft\">\r\n    <div class=\"transparentBack\">\r\n        <h2>Delete Review</h2>\r\n\r\n        <h3>Are you sure you want to delete this review?</h3>\r\n        <div>\r\n            <hr />\r\n            <label class=\"detailLabels\">\r\n                ");
            EndContext();
            BeginContext(383, 42, false);
#line 15 "C:\Users\Lisda\Documents\School\Fall 2019\MS Enterprice\GroupFive_Iteration3\ConestogaCarpool\ConestogaCarpool\Views\Review\Delete.cshtml"
           Write(Html.DisplayNameFor(model => model.Rating));

#line default
#line hidden
            EndContext();
            BeginContext(425, 60, true);
            WriteLiteral(":\r\n            </label>\r\n            <label id=\"textLabels\">");
            EndContext();
            BeginContext(486, 38, false);
#line 17 "C:\Users\Lisda\Documents\School\Fall 2019\MS Enterprice\GroupFive_Iteration3\ConestogaCarpool\ConestogaCarpool\Views\Review\Delete.cshtml"
                              Write(Html.DisplayFor(model => model.Rating));

#line default
#line hidden
            EndContext();
            BeginContext(524, 96, true);
            WriteLiteral("</label>\r\n            <br /><br />\r\n\r\n            <label class=\"detailLabels\">\r\n                ");
            EndContext();
            BeginContext(621, 43, false);
#line 21 "C:\Users\Lisda\Documents\School\Fall 2019\MS Enterprice\GroupFive_Iteration3\ConestogaCarpool\ConestogaCarpool\Views\Review\Delete.cshtml"
           Write(Html.DisplayNameFor(model => model.Comment));

#line default
#line hidden
            EndContext();
            BeginContext(664, 60, true);
            WriteLiteral(":\r\n            </label>\r\n            <label id=\"textLabels\">");
            EndContext();
            BeginContext(725, 39, false);
#line 23 "C:\Users\Lisda\Documents\School\Fall 2019\MS Enterprice\GroupFive_Iteration3\ConestogaCarpool\ConestogaCarpool\Views\Review\Delete.cshtml"
                              Write(Html.DisplayFor(model => model.Comment));

#line default
#line hidden
            EndContext();
            BeginContext(764, 96, true);
            WriteLiteral("</label>\r\n            <br /><br />\r\n\r\n            <label class=\"detailLabels\">\r\n                ");
            EndContext();
            BeginContext(861, 42, false);
#line 27 "C:\Users\Lisda\Documents\School\Fall 2019\MS Enterprice\GroupFive_Iteration3\ConestogaCarpool\ConestogaCarpool\Views\Review\Delete.cshtml"
           Write(Html.DisplayNameFor(model => model.Driver));

#line default
#line hidden
            EndContext();
            BeginContext(903, 60, true);
            WriteLiteral(":\r\n            </label>\r\n            <label id=\"textLabels\">");
            EndContext();
            BeginContext(964, 52, false);
#line 29 "C:\Users\Lisda\Documents\School\Fall 2019\MS Enterprice\GroupFive_Iteration3\ConestogaCarpool\ConestogaCarpool\Views\Review\Delete.cshtml"
                              Write(Html.DisplayFor(model => model.Driver.User.Username));

#line default
#line hidden
            EndContext();
            BeginContext(1016, 96, true);
            WriteLiteral("</label>\r\n            <br /><br />\r\n\r\n            <label class=\"detailLabels\">\r\n                ");
            EndContext();
            BeginContext(1113, 45, false);
#line 33 "C:\Users\Lisda\Documents\School\Fall 2019\MS Enterprice\GroupFive_Iteration3\ConestogaCarpool\ConestogaCarpool\Views\Review\Delete.cshtml"
           Write(Html.DisplayNameFor(model => model.Passenger));

#line default
#line hidden
            EndContext();
            BeginContext(1158, 60, true);
            WriteLiteral(":\r\n            </label>\r\n            <label id=\"textLabels\">");
            EndContext();
            BeginContext(1219, 47, false);
#line 35 "C:\Users\Lisda\Documents\School\Fall 2019\MS Enterprice\GroupFive_Iteration3\ConestogaCarpool\ConestogaCarpool\Views\Review\Delete.cshtml"
                              Write(Html.DisplayFor(model => model.Passenger.Email));

#line default
#line hidden
            EndContext();
            BeginContext(1266, 96, true);
            WriteLiteral("</label>\r\n            <br /><br />\r\n\r\n            <label class=\"detailLabels\">\r\n                ");
            EndContext();
            BeginContext(1363, 40, false);
#line 39 "C:\Users\Lisda\Documents\School\Fall 2019\MS Enterprice\GroupFive_Iteration3\ConestogaCarpool\ConestogaCarpool\Views\Review\Delete.cshtml"
           Write(Html.DisplayNameFor(model => model.Ride));

#line default
#line hidden
            EndContext();
            BeginContext(1403, 60, true);
            WriteLiteral(":\r\n            </label>\r\n            <label id=\"textLabels\">");
            EndContext();
            BeginContext(1464, 43, false);
#line 41 "C:\Users\Lisda\Documents\School\Fall 2019\MS Enterprice\GroupFive_Iteration3\ConestogaCarpool\ConestogaCarpool\Views\Review\Delete.cshtml"
                              Write(Html.DisplayFor(model => model.Ride.RideId));

#line default
#line hidden
            EndContext();
            BeginContext(1507, 60, true);
            WriteLiteral("</label>\r\n            <br /><br />\r\n        </div>\r\n        ");
            EndContext();
            BeginContext(1567, 169, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f92229ebdfa149c587071b2dbc5c5d05", async() => {
                BeginContext(1593, 14, true);
                WriteLiteral("\r\n            ");
                EndContext();
                BeginContext(1607, 42, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "a5ab12c2ed67420eaa31a4b5c43cb5c3", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 45 "C:\Users\Lisda\Documents\School\Fall 2019\MS Enterprice\GroupFive_Iteration3\ConestogaCarpool\ConestogaCarpool\Views\Review\Delete.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ReviewId);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1649, 80, true);
                WriteLiteral("\r\n            <input type=\"submit\" value=\"Delete\" id=\"btnAllShared\" />\r\n        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1736, 22, true);
            WriteLiteral("\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ConestogaCarpool.Models.Review> Html { get; private set; }
    }
}
#pragma warning restore 1591
