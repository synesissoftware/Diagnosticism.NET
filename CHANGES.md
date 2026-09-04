# Diagnosticism.NET - Changes <!-- omit in toc -->


## 0.1.0 - 4th September 2026

* SDK-style cross-platform shell: multi-target library (`net8.0`, `netstandard2.0`), xUnit tests, QuickStart sample;
* Synesis markdown set (**AUTHORS.md**, **CHANGES.md**, **CONTRIBUTING.md**, **EXAMPLES.md**, **FAQ.md**, **INSTALL.md**, **NEWS.md**, **README.md**, **TODO.md**);
* GitHub Actions **ci.yml** (Ubuntu, Windows, macOS) and **release.yml** NuGet publish workflow;
* NuGet packaging id **Diagnosticism** with portable PDB symbol packages (`.snupkg`) and Source Link;
* Synesis-standard build scripts (**build.sh**, **build.ps1**) and **.sis/** project identity;
* stub public API: **Diagnosticism.LibraryVersion** (`Major` / `Minor` / `Patch` / `VersionString`);
* Ported **Diagnosticism.Reflection** from the recovered tree: **AnonymousUtil** (`IsAnonymousType`, `ConvertToDictionary`) and **StructureConversionOptions**;
* xUnit coverage for empty / scalar / array / nested anonymous conversions and null-argument behaviour;
* Null-argument guards and nullable-aware dictionary values (`IDictionary<string, object?>`);
* Ported **Diagnosticism.Diagnostics** **Timing** / **TimingsMap** (execute-around timing, min / max / mean / frequencies);
* xUnit coverage for **TimingsMap** recording and aggregate helpers;


<!-- ########################### end of file ########################### -->
