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
    using System;
    using System.ComponentModel;

    using JetBrains.ReSharper.Feature.Services.CodeCleanup;

    [Category (CSharpCategory)]
    [DisplayName ("Fix StyleCop violations")]
    [DefaultValue (true)]
    public class FixViolationsDescriptor : CodeCleanupBoolOptionDescriptor
    {
        public FixViolationsDescriptor ()
            : base ("FixViolations")
        {
        }
    }

    [Category (CSharpCategory)]
    [DisplayName ("Create XML doc stubs")]
    [DefaultValue (false)]
    public class CreateXmlDocStubsDescriptor : CodeCleanupBoolOptionDescriptor
    {
        public CreateXmlDocStubsDescriptor ()
            : base ("CreateXmlDocStubs")
        {
        }
    }
}