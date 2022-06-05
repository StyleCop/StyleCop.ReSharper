// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StyleCopDescriptor.cs" company="http://stylecop.codeplex.com">
//   MS-PL
// </copyright>
// <summary>
//   Defines the StyleCopDescriptor type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StyleCop.ReSharper.CodeCleanup
{
    using JetBrains.ReSharper.Feature.Services.CodeCleanup;
    using System.ComponentModel;

    [DisplayName("Fix StyleCop violations")]
    [DefaultValue(true)]
    public class FixViolationsDescriptor : CodeCleanupBoolOptionDescriptor
    {
        public FixViolationsDescriptor()
            : base("FixViolations")
        {
        }
    }

    [DisplayName("Create XML doc stubs")]
    [DefaultValue(false)]
    public class CreateXmlDocStubsDescriptor : CodeCleanupBoolOptionDescriptor
    {
        public CreateXmlDocStubsDescriptor()
            : base("CreateXmlDocStubs")
        {
        }
    }
}