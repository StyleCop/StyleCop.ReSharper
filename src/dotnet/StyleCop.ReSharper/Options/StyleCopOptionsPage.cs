// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StyleCopOptionsPage.cs" company="http://stylecop.codeplex.com">
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
//   Defines the StyleCopOptionsPage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Platform.VisualStudio.Protocol.Common.Shell;

namespace StyleCop.ReSharper.Options
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Reflection;

    using JetBrains.Application.Components;
    using JetBrains.Application.Settings;
    using JetBrains.Application.UI.Commands;
    using JetBrains.Application.UI.Options;
    using JetBrains.Application.UI.Options.OptionsDialog;
    using JetBrains.Application.UI.Options.OptionsDialog.SimpleOptions;
    using JetBrains.Application.UI.Options.OptionsDialog.SimpleOptions.ViewModel;
    using JetBrains.DataFlow;
    using JetBrains.Lifetimes;
    using JetBrains.ProjectModel;
    using JetBrains.ReSharper.Feature.Services.Daemon;
    using JetBrains.ReSharper.Resources.Shell;
    using JetBrains.UI.Controls;
    using JetBrains.UI.RichText;
    using JetBrains.Util;
    using JetBrains.VsIntegration.Shell;

    using StyleCop.ReSharper.Resources;
    using StyleCop.ReSharper.ShellComponents;

    /// <summary>
    /// Options page to allow the plugins options to be set from within the ReSharper Options window.
    /// </summary>
    [OptionsPage(PageId, "StyleCop", typeof(StyleCopThemedIcons.Logo), ParentId = "Tools")]
    public class StyleCopOptionsPage : CustomSimpleOptionsPage
    {

        /// <summary>
        /// The unique name of this options page.
        /// </summary>
        private const string PageId = "StyleCopOptionsPage";

        private readonly bool originalEnablePlugins;
        private readonly string originalPluginsPath;

        /// <summary>
        /// Initializes a new instance of the StyleCopOptionsPage class.
        /// </summary>
        /// <param name="lifetime">
        /// The lifetime of the options page.
        /// </param>
        /// <param name="settingsSmartContext">
        /// Our settings context. 
        /// </param>
        /// <param name="container">
        /// The component container
        /// </param>
        /// <param name="commonFileDialog">
        /// The common file dialog
        /// </param>
        public StyleCopOptionsPage(
            Lifetime lifetime,
            OptionsSettingsSmartContext settingsSmartContext,
            IComponentContainer container,
            WindowsCommonFileDialog commonFileDialog)
            : base(lifetime, settingsSmartContext)
        {
            IContextBoundSettingsStoreLive settingsContext =
                this.OptionsSettingsSmartContext.StoreOptionsTransactionContext;
            this.originalEnablePlugins =
                settingsContext.GetValue((StyleCopOptionsSettingsKey options) => options.PluginsEnabled);
            this.originalPluginsPath =
                settingsContext.GetValue((StyleCopOptionsSettingsKey options) => options.PluginsPath);

            this.AddHeader("Version");

            Assembly assembly = typeof(StyleCopEnvironment).Assembly;
            string styleCopFileVersion = GetFileVersionInfo(assembly);
            this.AddText(string.Format("StyleCop.dll {0} ({1})", assembly.GetName().Version, styleCopFileVersion));

            assembly = this.GetType().Assembly;
            string ourFileVersion = GetFileVersionInfo(assembly);
            this.AddText(string.Format("StyleCop.ReSharper.dll {0} ({1})", assembly.GetName().Version, ourFileVersion));

            this.AddHeader("Options");

            // Note that we have to check to see if the lifetime is terminated before accessing the
            // settings context because WPF will continue to call our CanExecute until a garbage collection
            // breaks the weak reference that WPF holds on command
            this.AddButton(
                "Reset code style options",
                new DelegateCommand(
                    () => CodeStyleOptions.CodeStyleOptionsReset(settingsSmartContext),
                    () => !lifetime.IsTerminated && !CodeStyleOptions.CodeStyleOptionsValid(settingsSmartContext)));

            this.AddHeader("Analysis Performance");
            if (DoesHostSupportRoslynAnalzyers(container))
            {
                this.AddText(
                    "Note: Analysis is automatically disabled if the project references the StyleCop.Analyzers NuGet package.");
            }

            this.AddBoolOption(
                (StyleCopOptionsSettingsKey options) => options.AnalysisEnabled,
                "Run StyleCop as you type");
            BoolOptionViewModel nonUserFiles =
                this.AddBoolOption(
                    (StyleCopOptionsSettingsKey options) => options.AnalyzeReadOnlyFiles,
                    "Analyze non-user files (not recommended)");
            this.AddBinding(
                nonUserFiles,
                BindingStyle.IsEnabledProperty,
                (StyleCopOptionsSettingsKey options) => options.AnalysisEnabled,
                x => x);

            this.AddHeader("Headers");
            this.AddBoolOption(
                (StyleCopOptionsSettingsKey options) => options.InsertTextIntoDocumentation,
                "Insert text into documentation and file headers");
            this.AddBoolOption(
                (StyleCopOptionsSettingsKey options) => options.UseSingleLineDeclarationComments,
                "Use single lines for declaration headers");
            this.AddBoolOption(
                (StyleCopOptionsSettingsKey options) => options.InsertToDoText,
                "Insert TODO into headers");
            this.AddIntOption(
                (StyleCopOptionsSettingsKey options) => options.DashesCountInFileHeader,
                "Number of dashes in file header text:");

            this.AddHeader("StyleCop Plugins");
            this.AddBoolOption(
                (StyleCopOptionsSettingsKey options) => options.PluginsEnabled,
                "Enable StyleCop plugins");
            this.AddText("Location of StyleCop plugins:");
            Property<FileSystemPath> pluginsPath = this.SetupPluginsPathProperty(lifetime);
            PathChooserViewModel fileChooser = this.AddFolderChooserOption(
                pluginsPath,
                "Location of StyleCop plugins",
                FileSystemPath.Empty,
                commonFileDialog);
            fileChooser.IsEnabledProperty.SetValue(true);
            this.AddBinding(
                fileChooser,
                BindingStyle.IsEnabledProperty,
                (StyleCopOptionsSettingsKey options) => options.PluginsEnabled,
                x => x);

            this.AddHeader("Misc");
            this.AddBoolOption(
                (StyleCopOptionsSettingsKey options) => options.UseExcludeFromStyleCopSetting,
                "Use ExcludeFromStyleCop setting in csproj files");
            this.AddStringOption(
                (StyleCopOptionsSettingsKey options) => options.SuppressStyleCopAttributeJustificationText,
                "Justification for SuppressMessage attribute:");

            // TODO: Add "update file header style" that used to be in code cleanup
            this.FinishPage();
        }

        /// <summary>
        /// Called when the OK button is pressed
        /// </summary>
        /// <returns>True to continue, false to prevent the dialog closing</returns>
        public override bool OnOk()
        {
            var settingsContext = this.OptionsSettingsSmartContext.StoreOptionsTransactionContext;
            bool newEnablePlugins = settingsContext.GetValue(
                (StyleCopOptionsSettingsKey options) => options.PluginsEnabled);
            string newPluginsPath = settingsContext.GetValue(
                (StyleCopOptionsSettingsKey options) => options.PluginsPath);
            if (newEnablePlugins != this.originalEnablePlugins || newPluginsPath != this.originalPluginsPath)
            {
                var solutionsManager = Shell.Instance.TryGetComponent<SolutionsManager>();
                if (solutionsManager != null && solutionsManager.Solution != null)
                {
                    solutionsManager.Solution.GetComponent<StyleCopApiPool>().Reset();
                    solutionsManager.Solution.GetComponent<IDaemon>().Invalidate();
                }
            }

            return base.OnOk();
        }

        private static string GetFileVersionInfo(Assembly assembly)
        {
            if (assembly.Location != null)
            {
                return FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
            }

            return String.Empty;
        }

        private static bool DoesHostSupportRoslynAnalzyers(IComponentContainer container)
        {
            bool hostSupportsRoslynAnalzyers = false;

            // There's probably a nicer way of optionally testing for this, but this works for now
            var vsEnvironmentInformation = container.TryGetComponent<IVsEnvironmentInformation>();
            if (vsEnvironmentInformation != null)
            {
                hostSupportsRoslynAnalzyers = vsEnvironmentInformation.VsVersion2 >= new Version2(14);
            }

            return hostSupportsRoslynAnalzyers;
        }

        private Property<FileSystemPath> SetupPluginsPathProperty(Lifetime lifetime)
        {
            var pluginsPath = new Property<FileSystemPath>(lifetime, "StyleCopOptionsPage::PluginsPath");
            var currentPath = FileSystemPath.Parse(this.originalPluginsPath);
            pluginsPath.SetValue(currentPath);
            pluginsPath.Change.Advise(
                lifetime,
                args =>
                    {
                        if (!args.HasNew || args.New == null)
                        {
                            return;
                        }

                        this.OptionsSettingsSmartContext.StoreOptionsTransactionContext.SetValue(
                            (StyleCopOptionsSettingsKey options) => options.PluginsPath,
                            args.New.FullPath);
                    });
            return pluginsPath;
        }
    }
}
