# NET5 Minimal Api with Carter
It's example i am show, who is possible does one Web Api using "Minimal Api" using Carter

## Features

- Package Carter, more information the link below:
https://github.com/CarterCommunity/Carter

- Package FluentValidation, to validate the requests, more information the link below:
https://fluentvalidation.net/

- Top level statements (C# 9) - https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements

- Record class (C# 9) -  https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record

- Without Startup.cs.

## Example with Carter
This is some example using Carter on .NET5.

### Get - Super Heros

Returning one list of Super Heros
```csharp

Get("/superhero", async (request, response) =>
               await response.WriteAsJsonAsync(superHero)
            );

```
### Get - Super Hero per Id

Returning one  Super Hero per id
```csharp
Get("superhero/{id}", async (request, response) =>{
                var id = request.RouteValues.As<Guid>("id");
                await response.WriteAsJsonAsync(superHero.First());
            });


```

### POST - New Super Hero with Validation(FluentValidation)

Returning the new Super Hero created
```csharp
Post("/superhero", async (req, res) =>
            {
                var hero = await req.Bind<SuperHero>();
                var validate = req.Validate(hero);
                if (validate.IsValid)
                {
                    res.StatusCode = 201;
                    await res.WriteAsJsonAsync(hero);
                }

                res.StatusCode = 400;
                res.ContentType = "application/problem+json";
                await res.WriteAsJsonAsync(validate.GetFormattedErrors());
            });

```


