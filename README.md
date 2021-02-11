# PotatoServer
PotatoServer is simple .NET 5.0 foundament for building ASP.NET Applications

## Features
- Sign-Up and Sign-In 
- Database with handling soft deletion, and saving when record was modified/deleted 
- API Exception handling
- SignalR exception handling
- Logging API calls (in progress...)
- Usefull Extension methods
- Univarsal ViewModels
- Easy configuration

## Startup
- Create new <YourProject> and add reference to PotatoServer
- Set <YourProject> as startup
- Set configuration in `appsettings.json` (more in **Configuration** section)
- Extend `BaseStartup` in `Startup` class
```
public class Startup : BaseStartup
{
    public override IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) : base(configuration)
    {
        Configuration = configuration;
    }
    public override void ConfigureServices(IServiceCollection services)
    {
        // You can use Your own User by extending User class
        // and create DbContext by extending BaseDbContext<TIdentityUser>
        
        services.SetupIdentity<User, PotatoDbContext>(Configuration);
        services.AddDbContext<PotatoDbContext>(o => o.UseSqlServer("connectionString"));
        base.ConfigureServices(services);
    }
}
```

## Configuration

#### appsettings.json
To run application You need to add set some configuration data in startup's appsettings
| Name | Meaning |
| ------ | ------ |
| Tokens.Key | Random GUID |
| Tokens.Issuer | JWT Token Issuer |
| Tokens.Audience | JWT Token Audience |
| Tokens.Expires | JWT Token Expire time in minutes |
| Password.RequiredLength | Required password length |
| Password.RequireDigit | Require digit in password |
| Password.RequireLowercase | Require lowercase character in password |
| Password.RequireUpperCase | Require uppercase character in password |
| Password.RequireNonAlphanumeric | Require non alphanumeric character in password  |

#### Startup
- `services.SetupCors(params string[] addresses)` - Cors configuration for specified origins
- `services.SetupIdentity<TUser, TDbContext>(IConfiguration config)` - Identity configuration for specified TUser and TDbContext
- `services.SetupAuthentication(IConfiguration config)` - Authentication with JWT Token configuration, also for SignalR

#### Default Auth controller
To start using default `api/auth/sign-in` and `api/auth/sign-up` endpoints You need to extend `BaseAuthController<TUser>`
```
[Route("api/auth")]
public class AuthController : BaseAuthController<User>
{
    public AuthController(UserManager<User> userManager,
                          IStringLocalizer<SharedResources> localizer,
                          IConfiguration configuration)
    : base(userManager, localizer, configuration) { }
}
```

## Usage

### IBaseModel
IBaseModel is an interface providing simple way to add and handle additional fileds in database.
| Name | Type |
| ------ | ------ |
| Id | int |
| Created | DateTime |
| Changed | DateTime |
| IsDeleted | bool |

Created and Changed fields are automaticly filled - check `BaseDbContext<TUser>` for more information

### Exceptions and Exceptions handling
PotatoServer provides few default exceptions, that can be used to return specific HTTP request and message to client
| Name | StatusCode | Description |
| ------ | ------ | ------ |
| PotatoServerException | 500 | Base class for all other exceptions |
| ServerErrorException | 500 | |
| NotFoundException | 404 | |
| BadRequestException | 400 | |
All other exceptions are handled as HTTP 500 and sends default *"Internal Server Error"* message to client.
You can add new exceptions by extending `PotatoServerException`.

### Filters
PotatoServer provides three filters:
- `HandleExceptionHubFilter`
- `HandleExceptionFilter`
- `LoggedActionFilter`

Filters can be used globally or as Attributes for single actions
```
[HttpGet("get-some-data")]
[HandleExceptionFilter]
[LoggedActionFilter]
public ActionResult GetSomeData() { return someData; }
```
```
services.AddControllers(options =>
{
    options.Filters.Add(typeof(HandleExceptionFilterAttribute));
    options.Filters.Add(typeof(LoggedActionFilterAttribute));
});
services.AddSignalR(hubOptions =>
{
    hubOptions.AddFilter<HandleExceptionHubFilter>();
});
```
### Extensions
#### GetPagedAsync
`Task<PagedVmResult<T>> IQueryable.GetPagedAsync<T>(int? skip, int? take)`
`PagedVmResult`
####  MapPagedViewModel
`PagedVmResult<TViewModel> IMapper.MapPagedViewModel<TModel, TViewModel>(PagedVmResult<TModel> pagedModel)`
#### Usage
```
public async Task<ActionResult> GetSomePagedDataAsync(int skip, int take)
{
    var pagedData = await await dbContext.SomeData.GetPagedAsync(skip, take);
    return Ok(mapper.MapPagedViewModel<SomeDbModel, SomeViewModel>(pagedData));
}
```
# License
MIT
