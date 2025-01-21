using ApiRestMinimal.Common.Behavior;
using ApiRestMinimal.Common.Interfaces.Users;
using ApiRestMinimal.Contracts.DTOs;
using ApiRestMinimal.Custom;
using FluentValidation;
using System.Security.Claims;

namespace ApiRestMinimal.Endpoints.Users
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            // Register user
            app.MapPost("/api/users/register", async (
                UserDTOs userDto,
                IUserService userService,
                IValidator<UserDTOs> validator) =>
            {
                var validationResult = ValidationBehavior.ValidateRequest<UserDTOs>(userDto, validator);

                if (validationResult != null)
                    return validationResult;

                try
                {
                    var registeredUser = await userService.RegisterUserAsync(userDto);
                    return Results.Created($"/api/users/{registeredUser.Email}", registeredUser);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { Message = ex.Message });
                }
            });

            // Login user
            app.MapPost("/api/users/login", async (
                LoginDTOs loginDto,
                IUserService userService,
                Utility utility,
                IConfiguration configuration,
                IValidator<LoginDTOs> validator) =>
            {
                var validationResult = ValidationBehavior.ValidateRequest<LoginDTOs>(loginDto, validator);

                if (validationResult != null)
                    return validationResult;

                var user = await userService.AuthenticateUserAsync(loginDto);

                if (user is null)
                    return Results.Json(new { Message = "Invalid credentials" }, statusCode: 401);


                var token = utility.GenerateJWT(user);
                return Results.Ok(new { Token = token });
            });

            // Get user by ID
            app.MapGet("/api/users/{id:guid}", async (Guid id, IUserService userService) =>
            {
                var user = await userService.GetUserByIdAsync(id);

                if (user is null)
                    return Results.NotFound(new { Message = "User not found" });

                return Results.Ok(new
                {
                    user.Id,
                    user.Name,
                    user.Email
                });
            });
        }
    }
}

