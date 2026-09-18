# Diagnosticism.NET <!-- omit in toc -->

Basic diagnostic facilities, for .NET

![Language](https://img.shields.io/badge/.NET-512BD4?style=flat&logo=dotnet&logoColor=white)
[![License](https://img.shields.io/badge/License-BSD_3--Clause-blue.svg)](https://opensource.org/licenses/BSD-3-Clause)
[![GitHub release](https://img.shields.io/github/v/release/synesissoftware/Diagnosticism.NET.svg)](https://github.com/synesissoftware/Diagnosticism.NET/releases/latest)
[![Last Commit](https://img.shields.io/github/last-commit/synesissoftware/Diagnosticism.NET)](https://github.com/synesissoftware/Diagnosticism.NET/commits/master)
[![CI](https://github.com/synesissoftware/Diagnosticism.NET/actions/workflows/ci.yml/badge.svg)](https://github.com/synesissoftware/Diagnosticism.NET/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Diagnosticism.svg)](https://www.nuget.org/packages/Diagnosticism/)
![TFM](https://img.shields.io/badge/TFM-net8.0%20%7C%20netstandard2.0-lightgrey)


## Table of Contents <!-- omit in toc -->

- [Introduction](#introduction)
- [Installation](#installation)
- [Quick start](#quick-start)
- [Components](#components)
- [Platform support](#platform-support)
- [Repository layout](#repository-layout)
- [Building from source](#building-from-source)
- [Examples](#examples)
- [Project Information](#project-information)
  - [Where to get help](#where-to-get-help)
  - [Contribution guidelines](#contribution-guidelines)
  - [Dependencies](#dependencies)
  - [Related projects](#related-projects)
  - [License](#license)


## Introduction

**Diagnosticism** provides low-level diagnostics facilities to support
library programming. The first **Diagnosticism** library was a C library
with a C++ wrapper. There have been several implementations in other
languages. **Diagnosticism.NET** is the **.NET** version.

This repository ships an SDK-style multi-target library (`net8.0`,
`netstandard2.0`) with CI and NuGet packaging. The **0.1.0** surface covers
**Reflection**, **Diagnostics** (**TimingsMap**), and **Testing**
(**Assist**).


## Installation

```bash
dotnet add package Diagnosticism
```

See [INSTALL.md](./INSTALL.md) for source checkout restore, build, and test.


## Quick start

```csharp
using Diagnosticism;
using Diagnosticism.Diagnostics;
using Diagnosticism.Reflection;
using Diagnosticism.Testing;

Console.WriteLine($"Diagnosticism.NET {LibraryVersion.VersionString}");

var attrs = new { verbose = true, path = "." };
IDictionary<string, object?> map = AnonymousUtil.ConvertToDictionary(
    attrs,
    StructureConversionOptions.None);

TimingsMap<string> timings = new();
timings.Add("noop", () => { });

string captured = Assist.ExecuteAroundWriter(writer => writer.Write("ok"));
```

See [`samples/Diagnosticism.NET.QuickStart`](./samples/Diagnosticism.NET.QuickStart) for a
runnable example. A short index is in [EXAMPLES.md](./EXAMPLES.md).


## Components


### Diagnostics

* **`Timing`** — tick / **TimeSpan** result for a measured operation;
* **`TimingsMap<TKey>`** — execute-around timing keyed by `TKey`, with min / max / mean / frequency helpers;


### Reflection

* **`AnonymousUtil.IsAnonymousType`** — detect compiler-generated anonymous types;
* **`AnonymousUtil.ConvertToDictionary`** — map anonymous-type properties to a dictionary (nested anonymous types recursively);
* **`StructureConversionOptions`** — flags reserved for conversion behaviour;


### Testing

* **`Assist.ExecuteAroundWriter`** — execute-around capture of text written to a **TextWriter**;


## Platform support

The library multi-targets:

| Target | Rationale |
| --- | --- |
| `net8.0` | Modern .NET runtime / AOT-friendly surface |
| `netstandard2.0` | Broad consumer reach (.NET Framework and older runtimes) |


## Repository layout

| Path | Purpose |
| --- | --- |
| `src/Diagnosticism.NET/` | Main library (published to NuGet as `Diagnosticism`) |
| `tests/Diagnosticism.NET.Tests/` | Unit tests |
| `samples/Diagnosticism.NET.QuickStart/` | Minimal consumer example |


## Building from source

Requires the [.NET SDK](https://dotnet.microsoft.com/download) version
specified in [`global.json`](./global.json).

```bash
./build.sh        # Linux / macOS
./build.ps1       # Windows PowerShell
```

Or:

```bash
dotnet restore Diagnosticism.NET.sln
dotnet build Diagnosticism.NET.sln --configuration Release
dotnet test Diagnosticism.NET.sln --configuration Release
dotnet pack src/Diagnosticism.NET/Diagnosticism.NET.csproj --configuration Release --output artifacts/packages
```


## Examples

See [EXAMPLES.md](./EXAMPLES.md).


## Project Information


### Where to get help

[GitHub Issues](https://github.com/synesissoftware/Diagnosticism.NET/issues)


### Contribution guidelines

Defect reports, feature requests, and pull requests are welcome. See
[CONTRIBUTING.md](./CONTRIBUTING.md).


### Dependencies

This library has **no** runtime package dependencies.


### Related projects

* [**Diagnosticism**](https://github.com/synesissoftware/Diagnosticism/) (C/C++)
* [**Diagnosticism.Go**](https://github.com/synesissoftware/Diagnosticism.Go/)
* [**Diagnosticism.Python**](https://github.com/synesissoftware/Diagnosticism.Python/)
* [**Diagnosticism.Ruby**](https://github.com/synesissoftware/Diagnosticism.Ruby/)
* [**Diagnosticism.Rust**](https://github.com/synesissoftware/Diagnosticism.Rust/)
* [**libCLImate.NET**](https://github.com/synesissoftware/libCLImate.NET/)


### License

**Diagnosticism.NET** is released under the 3-clause BSD license. See
[LICENSE](./LICENSE) for details.


<!-- ########################### end of file ########################### -->
