# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## vNext
- Updated to ReSharper 2018.3

## 2018.2.0
- Updated to ReSharper 2018.2

## 2018.1.2
- Fixed removal of parentheses for conditional expressions in interpolated strings. (StyleCop/StyleCop#175)

## 2018.1.1
- Updated to ReSharper 2018.1.1

## 2018.1.0
- Updated to ReSharper 2018.1

## 2017.3.0
- Updated to ReSharper 2017.3
- Updated to StyleCop 5.0.0 (StyleCop/StyleCop#123)
- Fix cleanup for rule 1100 (StyleCop/StyleCop#153)

## 2017.2.0
- Updated to ReSharper 2017.2

## 2017.1.1
- Fixed broken Code Cleanup (StyleCop/StyleCop#110)

## 2017.1.0
- Updated to ReSharper 2017.1

## 2016.3.2
- Fix some Cleanup rules that modify single line comments, should not apply to file headers (#1)
- Fix Cleanup bug that would insert blank line before comment if it was first line after a switch label (#1)
Thanks @SanjayGuntur for the PR!

## 2016.3.1
- Fix crash if the file doesn't have a header (StyleCop/StyleCop#78)

## 2016.3.0
- Updated to ReSharper 2016.3

## 2016.2.1
- Remove NUnit file layout pattern, since it doesn't match StyleCop guidelines

## 2016.2.0
- Updated to ReSharper 2016.2 (StyleCop/StyleCop#55)

## 2016.1.4
- Create SuppressMessage attribute parameters more reliably
- Works properly if StyleCop 4.7.49 is already loaded in Visual Studio

## 2016.1.3
- Better version number reporting

## 2016.1.2
- Updated to StyleCop 4.7.54 (StyleCop/StyleCop#52)

## 2016.1.1
- Suppress message quick fix available again (StyleCop/StyleCop#59)
- Fix duplicate warning tooltips (StyleCop/StyleCop#34)
- Fix insert header documentation quick fix (StyleCop/StyleCop#57)
- Disable inspection warnings with ReSharper comments (StyleCop/StyleCop#60)
- Don't add trailing space after last comma on line (StyleCop/StyleCop#25)
- Display StyleCop parse errors
- Display StyleCop.dll version in plugin options page

## Previous releases
- Support for ReSharper 2016.1 (StyleCop/StyleCop#47)
- Fix to allow filtering by StyleCop issues in code inspection window (StyleCop/StyleCop#37)
- Fix error messages in installer (StyleCop/StyleCop#32)
- Fix to stop code cleanup opening unedited files (StyleCop/StyleCop#27)
- Automatically disable analysis if StyleCop.Analyzers is referenced in VS2015 (StyleCop/StyleCop#20)
- Supports loading custom StyleCop addins. Specify location in settings (StyleCop/StyleCop#18)
- Fix duplication of constructor summary XML doc element in Code Cleanup (StyleCop/StyleCop#17)
- StyleCop issues now grouped under "StyleCop" in Inspection Results (StyleCop/StyleCop#16)
- Separate Code Cleanup option to create XML doc stubs (StyleCop/StyleCop#15)
- Not honouring settings when adjusting file header in Code Cleanup (StyleCop/StyleCop#14)