#pragma checksum "C:\Users\richa\Source\Repos\rjharris01\ISAD251REF\ISAD251REF\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "39d5e3bad67790ed9b105e756505f4bbde13cdfd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#nullable restore
#line 1 "C:\Users\richa\Source\Repos\rjharris01\ISAD251REF\ISAD251REF\Views\_ViewImports.cshtml"
using ISAD251REF;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\richa\Source\Repos\rjharris01\ISAD251REF\ISAD251REF\Views\_ViewImports.cshtml"
using ISAD251REF.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"39d5e3bad67790ed9b105e756505f4bbde13cdfd", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0896b02b7752381ad53d7c54acc5ce5771c5cdda", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\richa\Source\Repos\rjharris01\ISAD251REF\ISAD251REF\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center mt-5 px-5\">\r\n    <div class=\"column\">\r\n        <center>\r\n\r\n            <div class=\"mb-2 col-lg-2\">\r\n\r\n                <input type=\"button\" class=\"btn btn-primary btn-lg btn-block\" value=\"Parent\"");
            BeginWriteAttribute("onclick", " onclick=\"", 265, "\"", 325, 4);
            WriteAttributeValue("", 275, "window.location=", 275, 16, true);
            WriteAttributeValue(" ", 291, "\'", 292, 2, true);
#nullable restore
#line 11 "C:\Users\richa\Source\Repos\rjharris01\ISAD251REF\ISAD251REF\Views\Home\Index.cshtml"
WriteAttributeValue("", 293, Url.Action( "Index", "Parent"), 293, 31, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 324, "\'", 324, 1, true);
            EndWriteAttribute();
            WriteLiteral(" />\r\n            \r\n            </div>\r\n\r\n\r\n\r\n            <div class=\"mt-2 col-lg-2\">\r\n\r\n                <input type=\"button\" class=\"btn btn-secondary btn-lg btn-block\" value=\"Child\"");
            BeginWriteAttribute("onclick", " onclick=\"", 507, "\"", 566, 4);
            WriteAttributeValue("", 517, "window.location=", 517, 16, true);
            WriteAttributeValue(" ", 533, "\'", 534, 2, true);
#nullable restore
#line 19 "C:\Users\richa\Source\Repos\rjharris01\ISAD251REF\ISAD251REF\Views\Home\Index.cshtml"
WriteAttributeValue("", 535, Url.Action( "Index", "Child"), 535, 30, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 565, "\'", 565, 1, true);
            EndWriteAttribute();
            WriteLiteral(" />\r\n\r\n            </div>\r\n  \r\n     \r\n        </center>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
