# Moon.AspNetCore changelog

3.0.1

- All libraries are targeting .NET Standard.
- `IUserAccessor` interface moved to `Moon.Security` project.

2.4.1

- Added extensions for ASP.NET Core rewriting middleware.
- Added `UserAccessor` providing access to current application user.
- Removed `Json` helper (use `IJsonHelper` in ASP.NET Core).