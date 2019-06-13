# StyleCop for ReSharper

StyleCop analyzes C# source code to enforce a set of style and consistency rules. StyleCop for ReSharper is an extension for ReSharper that will run StyleCop analysis as you type, display the violations inline in the editor and provide quick fixes for some violations. It will also configure ReSharper to use default settings that match StyleCop rules.

[![Gitter](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/StyleCop/StyleCop?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

## About this repo

The code in this repo is the ReSharper extension taken from the [GitHub StyleCop](https://github.com/StyleCop/StyleCop) repo, which is itself an unofficial fork of the StyleCop project hosted at [CodePlex](http://stylecop.codeplex.com).

The CodePlex project was maintained by Andy Reeves for several years, and was on [indefinite hiatus from March 2014](https://twitter.com/stylecopdev/status/448202371798433792) until recently, when @csdahlberg took over and restarted development.

The GitHub fork and the CodePlex fork have been independently developed, although the changes don't appear to overlap. The GitHub repo concentrated on updating the ReSharper extension, while the CodePlex repo has been making changes to StyleCop itself. Hopefully, these changes can be reconciled (it's more complicated than just a pull request - for example, the CodePlex repo is still on Mercurial).

In the meantime, this repo exists to decouple the ReSharper extension from the rest of the StyleCop source tree. It can be updated to use the latest released version of StyleCop without having to merge the source code.

At some point in the future, the changes in here can be merged back into the main StyleCop repo (whether that lives on CodePlex or StyleCop) and this repo will be deleted. Alternatively, the ReSharper code will be removed from the main repo.

However, this extension should be considered the "official" StyleCop ReSharper extension.

## Installing the extension

Please install the extension via ReSharper's Extension Manager.
