# Diagnosticism.NET - TODO <!-- omit in toc -->


## Table of Contents <!-- omit in toc -->

- [Functional improvements](#functional-improvements)
- [Performance improvements](#performance-improvements)
- [Packaging improvements](#packaging-improvements)


## Functional improvements

* [x] ~~~port recovered **Reflection** API (`AnonymousUtil`, `StructureConversionOptions`) and unit tests~~~ - ✅;
* [ ] port recovered **TimingsMap** / **Timing** API and coverage;
* [ ] decide **ExceptionUtil** (implement HRESULT mapping vs drop / TODO);
* [ ] port recovered **Diagnosticism.Testing** (`Assist`) — package vs test-only;
* [ ] document and sample Core APIs once ported;
* [ ] assess sibling-language facilities (logging / tracing / contingent reporting) for later .NET parity;


## Performance improvements

* \<none>


## Packaging improvements

* [ ] first NuGet.org publish once Core APIs needed by dependents are present;
* [ ] decide whether Testing ships as a second package or stays in-tree only;
* [ ] align package / assembly versioning with sibling **Diagnosticism.*** releases;


<!-- ########################### end of file ########################### -->
