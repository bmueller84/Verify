<!--
GENERATED FILE - DO NOT EDIT
This file was generated by [MarkdownSnippets](https://github.com/SimonCropp/MarkdownSnippets).
Source File: /readme.source.md
To change this file edit the source file and then run MarkdownSnippets.
-->

# <img src='/src/icon.png' height='30px'> Verify

[![Build status](https://ci.appveyor.com/api/projects/status/dpqylic0be7s9vnm/branch/master?svg=true)](https://ci.appveyor.com/project/SimonCropp/Verify)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.Xunit.svg?label=Verify.Xunit)](https://www.nuget.org/packages/Verify.Xunit/)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.NUnit.svg?label=Verify.NUnit)](https://www.nuget.org/packages/Verify.NUnit/)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.MSTest.svg?label=Verify.MSTest)](https://www.nuget.org/packages/Verify.MSTest/)

Verify is a snapshot tool that simplifies the assertion of complex data models and documents.

Verify is called on the test result during the assertion phase. It serializes that result and stores it in a file that matches the test name. On the next test execution, the result is again serialized and compared to the existing file. The test will fail if the two snapshots do not match: either the change is unexpected, or the reference snapshot needs to be updated to the new result.

Support is available via a [Tidelift Subscription](https://tidelift.com/subscription/pkg/nuget-verify?utm_source=nuget-verify&utm_medium=referral&utm_campaign=enterprise).

<a href='https://dotnetfoundation.org' alt='Part of the .NET Foundation'><img src='/docs/dotNetFoundation.svg' height='30px'></a><br>
Part of the <a href='https://dotnetfoundation.org' alt=''>.NET Foundation</a>

<!-- toc -->
## Contents

  * [Usage](#usage)
    * [Class being tested](#class-being-tested)
    * [xUnit](#xunit)
    * [NUnit](#nunit)
    * [MSTest](#mstest)
    * [Initial Verification](#initial-verification)
    * [Subsequent Verification](#subsequent-verification)
  * [Received and Verified](#received-and-verified)
  * [Videos](#videos)
  * [Extensions](#extensions)
  * [More Documentation](#more-documentation)
  * [Alternatives](#alternatives)
  * [Security contact information](#security-contact-information)<!-- endtoc -->


## NuGet packages

 * https://nuget.org/packages/Verify.Xunit/
 * https://nuget.org/packages/Verify.NUnit/
 * https://nuget.org/packages/Verify.MSTest/


## Usage


### Class being tested

Given a class to be tested:

<!-- snippet: ClassBeingTested -->
<a id='snippet-classbeingtested'></a>
```cs
public static class ClassBeingTested
{
    public static Person FindPerson()
    {
        return new Person
        {
            Id = new Guid("ebced679-45d3-4653-8791-3d969c4a986c"),
            Title = Title.Mr,
            GivenNames = "John",
            FamilyName = "Smith",
            Spouse = "Jill",
            Children = new List<string>
            {
                "Sam",
                "Mary"
            },
            Address = new Address
            {
                Street = "4 Puddle Lane",
                Country = "USA"
            }
        };
    }
}
```
<sup><a href='/src/TargetLibrary/ClassBeingTested.cs#L4-L29' title='File snippet `classbeingtested` was extracted from'>snippet source</a> | <a href='#snippet-classbeingtested' title='Navigate to start of snippet `classbeingtested`'>anchor</a></sup>
<!-- endsnippet -->


### xUnit

Support for [xUnit](https://xunit.net/)

<!-- snippet: SampleTestXunit -->
<a id='snippet-sampletestxunit'></a>
```cs
using static VerifyXunit.Verifier;

[UsesVerify]
public class Sample
{
    [Fact]
    public Task Test()
    {
        var person = ClassBeingTested.FindPerson();
        return Verify(person);
    }
}
```
<sup><a href='/src/Verify.Xunit.Tests/Snippets/Sample.cs#L5-L18' title='File snippet `sampletestxunit` was extracted from'>snippet source</a> | <a href='#snippet-sampletestxunit' title='Navigate to start of snippet `sampletestxunit`'>anchor</a></sup>
<!-- endsnippet -->

 * [Verify Xunit Intro](https://www.youtube.com/watch?v=uGVogEltSkY)


### NUnit

Support for [NUnit](https://nunit.org/)

<!-- snippet: SampleTestNUnit -->
<a id='snippet-sampletestnunit'></a>
```cs
using static VerifyNUnit.Verifier;

[TestFixture]
public class Sample
{
    [Test]
    public Task Test()
    {
        var person = ClassBeingTested.FindPerson();
        return Verify(person);
    }
}
```
<sup><a href='/src/Verify.NUnit.Tests/Snippets/Sample.cs#L4-L17' title='File snippet `sampletestnunit` was extracted from'>snippet source</a> | <a href='#snippet-sampletestnunit' title='Navigate to start of snippet `sampletestnunit`'>anchor</a></sup>
<!-- endsnippet -->


### MSTest

Support for [MSTest](https://github.com/Microsoft/testfx-docs)

<!-- snippet: SampleTestMSTest -->
<a id='snippet-sampletestmstest'></a>
```cs
[TestClass]
public class Sample :
    VerifyBase
{
    [TestMethod]
    public Task Test ()
    {
        var person = ClassBeingTested.FindPerson();
        return Verify(person);
    }
}
```
<sup><a href='/src/Verify.MSTest.Tests/Snippets/Sample.cs#L5-L17' title='File snippet `sampletestmstest` was extracted from'>snippet source</a> | <a href='#snippet-sampletestmstest' title='Navigate to start of snippet `sampletestmstest`'>anchor</a></sup>
<!-- endsnippet -->


### Initial Verification

When the test is initially run will fail with:

```
First verification. Sample.Test.verified.txt not found.
Verification command has been copied to the clipboard.
```

The clipboard will contain the following:

> cmd /c move /Y "C:\Code\Sample\Sample.Test.received.txt" "C:\Code\Sample\Sample.Test.verified.txt"

See also: [Clipboard](/docs/clipboard.md)

If a [Diff Tool](https://github.com/VerifyTests/DiffEngine) is detected it will display the diff:

![InitialDiff](/docs/InitialDiff.png)

To verify the result:

 * Execute the command from the clipboard, or
 * Use the diff tool to accept the changes, or
 * Manually copy the text to the new file


#### Verified result

This will result in the `Sample.Test.verified.txt` being created:

<!-- snippet: Verify.Xunit.Tests/Snippets/Sample.Test.verified.txt -->
<a id='snippet-Verify.Xunit.Tests/Snippets/Sample.Test.verified.txt'></a>
```txt
{
  GivenNames: 'John',
  FamilyName: 'Smith',
  Spouse: 'Jill',
  Address: {
    Street: '4 Puddle Lane',
    Country: 'USA'
  },
  Children: [
    'Sam',
    'Mary'
  ],
  Id: Guid_1
}
```
<sup><a href='/src/Verify.Xunit.Tests/Snippets/Sample.Test.verified.txt#L1-L14' title='File snippet `Verify.Xunit.Tests/Snippets/Sample.Test.verified.txt` was extracted from'>snippet source</a> | <a href='#snippet-Verify.Xunit.Tests/Snippets/Sample.Test.verified.txt' title='Navigate to start of snippet `Verify.Xunit.Tests/Snippets/Sample.Test.verified.txt`'>anchor</a></sup>
<!-- endsnippet -->


### Subsequent Verification

If the implementation of `ClassBeingTested` changes:

<!-- snippet: ClassBeingTestedChanged -->
<a id='snippet-classbeingtestedchanged'></a>
```cs
public static class ClassBeingTested
{
    public static Person FindPerson()
    {
        return new Person
        {
            Id = new Guid("ebced679-45d3-4653-8791-3d969c4a986c"),
            Title = Title.Mr,
            // Middle name added
            GivenNames = "John James",
            FamilyName = "Smith",
            Spouse = "Jill",
            Children = new List<string>
            {
                "Sam",
                "Mary"
            },
            Address = new Address
            {
                // Address changed
                Street = "64 Barnett Street",
                Country = "USA"
            }
        };
    }
}
```
<sup><a href='/src/TargetLibrary/ClassBeingTestedChanged.cs#L6-L33' title='File snippet `classbeingtestedchanged` was extracted from'>snippet source</a> | <a href='#snippet-classbeingtestedchanged' title='Navigate to start of snippet `classbeingtestedchanged`'>anchor</a></sup>
<!-- endsnippet -->

And the test is re run it will fail with

```
Verification command has been copied to the clipboard.
Assert.Equal() Failure
                                  ↓ (pos 21)
Expected: ···\n  GivenNames: 'John',\n  FamilyName: 'Smith',\n  Spouse: 'Jill···
Actual:   ···\n  GivenNames: 'John James',\n  FamilyName: 'Smith',\n  Spouse:···
                                  ↑ (pos 21)
```

The clipboard will again contain the following:

> cmd /c move /Y "C:\Code\Sample\Sample.Test.received.txt" "C:\Code\Sample\Sample.Test.verified.txt"

See also: [Clipboard](/docs/clipboard.md)

#### The [Diff Tool](https://github.com/VerifyTests/DiffEngine) is will display the diff:

![SecondDiff](/docs/SecondDiff.png)

The same approach can be used to verify the results and the change to `Sample.Test.verified.txt` is committed to source control along with the change to `ClassBeingTested`.


## Received and Verified

 * **All `*.verified.*` files should be committed to source control.**
 * **All `*.received.*` files should be excluded from source control.**


## Videos

 * [Verify Xunit Intro](https://www.youtube.com/watch?v=uGVogEltSkY)


## Extensions

 * [Verify.AngleSharp.Diffing](https://github.com/VerifyTests/Verify.AngleSharp.Diffing): Comparison of html files via [AngleSharp.Diffing](https://github.com/AngleSharp/AngleSharp.Diffing).
 * [Verify.Aspose](https://github.com/VerifyTests/Verify.Aspose): Verification of documents (pdf, docx, xslx, and pptx) via Aspose.
 * [Verify.Blazor](https://github.com/VerifyTests/Verify.Blazor): Verification of [Blazor Component](https://docs.microsoft.com/en-us/aspnet/core/blazor/#components) via [bunit](https://bunit.egilhansen.com) or via raw Blazor rendering.
 * [Verify.EntityFramework](https://github.com/VerifyTests/Verify.EntityFramework): Verification of EntityFramework bits.
 * [Verify.ICSharpCode.Decompiler](https://github.com/VerifyTests/Verify.ICSharpCode.Decompiler): Comparison of assemblies and types via [ICSharpCode.Decompiler](https://github.com/icsharpcode/ILSpy/wiki/Getting-Started-With-ICSharpCode.Decompiler).
 * [Verify.ImageMagick](https://github.com/VerifyTests/Verify.ImageMagick): Verification and comparison of images via [Magick.NET](https://github.com/dlemstra/Magick.NET).
 * [Verify.ImageSharp](https://github.com/VerifyTests/Verify.ImageSharp): Verification of images via [ImageSharp](https://github.com/SixLabors/ImageSharp).
 * [Verify.NServiceBus](https://github.com/NServiceBusExtensions/Verify.NServiceBus): Verify NServiceBus Test Contexts.
 * [Verify.Phash](https://github.com/VerifyTests/Verify.Phash): Comparison of documents via [Phash](https://github.com/pgrho/phash).
 * [Verify.Selenium](https://github.com/VerifyTests/Verify.Selenium): Verification of Web UIs using [Selenium](https://www.selenium.dev/).
 * [Verify.SqlServer](https://github.com/VerifyTests/Verify.SqlServer): Verification of SqlServer bits.
 * [Verify.Uno](https://github.com/VerifyTests/Verify.Uno): Verification to allow verification of [Uno UIs](https://platform.uno/).
 * [Verify.Web](https://github.com/VerifyTests/Verify.Web): Verification of web bits.
 * [Verify.WinForms](https://github.com/VerifyTests/Verify.WinForms): Verification of WinForms UIs.
 * [Verify.Xaml](https://github.com/VerifyTests/Verify.Xaml): Verification of Xaml UIs.


## More Documentation

  * [Clipboard](/docs/clipboard.md) <!-- include: doc-index. path: /docs/mdsource/doc-index.include.md -->
  * [Compared to assertions](/docs/compared-to-assertion.md)
  * [Verify options](/docs/verify-options.md)
  * [Serializer Settings](/docs/serializer-settings.md)
  * [File naming](/docs/naming.md)
  * [Parameterised tests](/docs/parameterised.md)
  * [Named Tuples](/docs/named-tuples.md)
  * [Scrubbers](/docs/scrubbers.md)
  * [Diff Engine](https://github.com/VerifyTests/DiffEngine)
  * [Diff Tools](https://github.com/VerifyTests/DiffEngine/blob/master/docs/diff-tool.md)
  * [Diff Tool Order](https://github.com/VerifyTests/DiffEngine/blob/master/docs/diff-tool.order.md)
  * [Custom Diff Tool](https://github.com/VerifyTests/DiffEngine/blob/master/docs/diff-tool.custom.md)
  * [Using anonymous types](/docs/anonymous-types.md)
  * [Verifying binary data](/docs/binary.md)
  * [Build server](/docs/build-server.md)
  * [Comparers](/docs/comparer.md)
  * [Converters](/docs/converter.md)
  * [Compared to ApprovalTests](/docs/compared-to-approvaltests.md) <!-- end include: doc-index. path: /docs/mdsource/doc-index.include.md -->


## Alternatives

Projects/tools that may be a better alternative to Verify

 * [ApprovalTests](https://github.com/approvals/ApprovalTests.Net)
 * [Snapshooter](https://github.com/SwissLife-OSS/Snapshooter)
 * [Snapper](https://github.com/theramis/Snapper)


## Security contact information

To report a security vulnerability, use the [Tidelift security contact](https://tidelift.com/security). Tidelift will coordinate the fix and disclosure.


## Icon

[Helmet](https://thenounproject.com/term/helmet/9554/) designed by [Leonidas Ikonomou](https://thenounproject.com/alterego) from [The Noun Project](https://thenounproject.com).
