<!--
GENERATED FILE - DO NOT EDIT
This file was generated by [MarkdownSnippets](https://github.com/SimonCropp/MarkdownSnippets).
Source File: /docs/mdsource/fsharp.source.md
To change this file edit the source file and then run MarkdownSnippets.
-->

# Usage in FSharp


## NullValueHandling

By default [DefaultValueHandling is Ignore](/docs/serializer-settings.md#default-settings). Since F# `Option.None` is treated as null, it will be ignored by default. To include `Option.None` use `VerifierSettings.AddExtraSettings` at module startup:

<!-- snippet: NullValueHandling -->
<a id='snippet-nullvaluehandling'></a>
```fs
VerifierSettings.AddExtraSettings(fun settings ->
  settings.NullValueHandling <- NullValueHandling.Include)
```
<sup><a href='/src/FSharpTests/Tests.fs#L9-L12' title='Snippet source file'>snippet source</a> | <a href='#snippet-nullvaluehandling' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Async Qwerks

F# does not respect implicit operator conversion. `SettingsTask` uses implicit operator conversion to provide a fluent api in combination with an instance that can be awaited. As such `SettingsTask.ToTask()` needs to be awaited when used inside F#.

<!-- snippet: FsTest -->
<a id='snippet-fstest'></a>
```fs
[<Fact>]
let MyTest () =
  async {
    do! Verifier.Verify(15)
          .ToTask() |> Async.AwaitTask
  }
```
<sup><a href='/src/FSharpTests/Tests.fs#L14-L21' title='Snippet source file'>snippet source</a> | <a href='#snippet-fstest' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Tasks can also be used directly via a `task` computation expression builder, such as the ones included in [Ply](https://github.com/crowded/ply), [TaskBuilder.fs](https://github.com/rspeele/TaskBuilder.fs), or (starting with F# 6.0) FSharp.Core:

<!-- snippet: FsTestTask -->
<a id='snippet-fstesttask'></a>
```fs
[<Fact>]
let MyTaskTest () =
  task {
    do! Verifier.Verify(15)
  }
```
<sup><a href='/src/FSharpTests/Tests.fs#L23-L29' title='Snippet source file'>snippet source</a> | <a href='#snippet-fstesttask' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

## Full tests

<!-- snippet: FSharpTests/Tests.fs -->
<a id='snippet-FSharpTests/Tests.fs'></a>
```fs
[<VerifyXunit.UsesVerify>]
module Tests

open Xunit
open VerifyTests
open VerifyXunit
open Newtonsoft.Json

VerifierSettings.AddExtraSettings(fun settings ->
  settings.NullValueHandling <- NullValueHandling.Include)

[<Fact>]
let MyTest () =
  async {
    do! Verifier.Verify(15)
          .ToTask() |> Async.AwaitTask
  }

[<Fact>]
let MyTaskTest () =
  task {
    do! Verifier.Verify(15)
  }

[<Fact>]
let WithFluentSetting () =
  async {
    do! Verifier.Verify(15)
          .UseMethodName("customName")
          .ToTask() |> Async.AwaitTask
  }
do ()
```
<sup><a href='/src/FSharpTests/Tests.fs#L1-L32' title='Snippet source file'>snippet source</a> | <a href='#snippet-FSharpTests/Tests.fs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->
