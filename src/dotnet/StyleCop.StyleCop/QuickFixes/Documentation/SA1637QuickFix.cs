﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SA1637QuickFix.cs" company="http://stylecop.codeplex.com">
//   MS-PL
// </copyright>
// <license>
//   This source code is subject to terms and conditions of the Microsoft 
//   Public License. A copy of the license can be found in the License.html 
//   file at the root of this distribution. If you cannot locate the  
//   Microsoft Public License, please send an email to dlr@microsoft.com. 
//   By using this source code in any fashion, you are agreeing to be bound 
//   by the terms of the Microsoft Public License. You must not remove this 
//   notice, or any other, from this software.
// </license>
// <summary>
//   QuickFix - SA1637: FileHeaderMustContainFileName.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StyleCop.ReSharper.QuickFixes.Documentation
{
    using JetBrains.ReSharper.Feature.Services.Bulbs;
    using JetBrains.ReSharper.Feature.Services.QuickFixes;
    using StyleCop.ReSharper.BulbItems.Documentation;
    using StyleCop.ReSharper.QuickFixes.Framework;
    using StyleCop.ReSharper.Violations;
    using System.Collections.Generic;

    /// <summary>
    /// QuickFix - SA1637: FileHeaderMustContainFileName.
    /// </summary>
    //// [ShowQuickFix]
    [QuickFix]
    public class SA1637QuickFix : StyleCopQuickFixBase
    {
        /// <summary>
        /// Initializes a new instance of the SA1637QuickFix class that can 
        /// handle <see cref="StyleCopHighlighting"/>.
        /// </summary>
        /// <param name="highlight">
        /// <see cref="StyleCopHighlighting"/>that has been detected.
        /// </param>
        public SA1637QuickFix(StyleCopHighlighting highlight)
            : base(highlight)
        {
        }

        /// <summary>
        /// Initializes the QuickFix with all the available BulbItems that can fix the current
        /// StyleCop Violation.
        /// </summary>
        protected override void InitialiseBulbItems()
        {
            this.BulbItems = new List<IBulbAction>
                                 {
                                     new SA1637FileHeaderMustContainFileNameBulbItem
                                         {
                                             Description =
                                                 "Insert filename into header : "
                                                 + this.Highlighting.ToolTip
                                         }
                                 };
        }
    }
}