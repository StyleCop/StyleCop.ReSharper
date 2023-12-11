using System.Diagnostics;
using JetBrains.Application;
using JetBrains.Application.Settings;
using JetBrains.Application.Settings.WellKnownRootKeys;
using JetBrains.Lifetimes;
using JetBrains.Util;

namespace StyleCop.ReSharper.ShellComponents;

[ShellComponent]
public class DeprecationMessage
{
    public DeprecationMessage(
        Lifetime lifetime,
        ISettingsStore settingsStore)
    {
        var settingsStoreLive = settingsStore.BindToContextLive(lifetime, ContextRange.ApplicationWide);
        var messageShown =
            settingsStoreLive.GetValueProperty(lifetime, (StyleCopDeprecationSettings x) => x.MessageShown);

        if (messageShown.Value)
            return;

        messageShown.Value = true;

        MessageBox.ShowExclamation(
            caption: "StyleCop",
            text: @"The original StyleCop project has been discontinued. If you keep using this plugin, it may cause:
- Failures on new language syntax starting with C# 7
- Performance degradation

Please take a look at the README for further information.");
        Process.Start(new ProcessStartInfo
            { FileName = "https://github.com/StyleCop/StyleCop.ReSharper#readme", UseShellExecute = true });
    }
}

[SettingsKey(
    typeof(EnvironmentSettings),
    Description: "StyleCop Deprecation Settings")]
public class StyleCopDeprecationSettings
{
    [SettingsEntry(DefaultValue: false, Description: "Message shown")]
    public bool MessageShown;
}
