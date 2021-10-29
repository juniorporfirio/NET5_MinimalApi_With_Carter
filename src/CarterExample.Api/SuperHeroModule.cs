using System;
using System.Collections.Generic;
using Carter;
using Carter.Request;
using Microsoft.AspNetCore.Http;
using Carter.ModelBinding;
using System.Linq;
using FluentValidation;

namespace CarterExample.Api
{
    public class SuperHeroModule : CarterModule
    {
        private readonly IEnumerable<SuperHero> superHero = new List<SuperHero>()
        {
            new SuperHero(Guid.NewGuid(),"Batman"),
            new SuperHero(Guid.NewGuid(),"SuperMan"),
            new SuperHero (Guid.NewGuid(),"Robin")
        };
        public SuperHeroModule()
        {
            Get("/superhero", async (request, response) =>
               await response.WriteAsJsonAsync(superHero)
            );

            Get("superhero/{id}", async (request, response) =>
            {
                var id = request.RouteValues.As<Guid>("id");
                await response.WriteAsJsonAsync(superHero.First());
            });

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
        }
    }
     public class SuperHeroValidator:AbstractValidator<SuperHero>
    {
        public SuperHeroValidator()
        {
            RuleFor(rule=>rule.Id).NotEmpty().WithMessage("Informe o Id");
            RuleFor(rule=>rule.Name).NotEmpty().WithMessage("Informe o nome");
        }
    }

    public record SuperHero(Guid Id,  string Name);
}