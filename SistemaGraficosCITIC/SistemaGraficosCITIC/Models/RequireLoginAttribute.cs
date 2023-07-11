using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;/*
using System.Web.Mvc;
using System.Web.Routing;

public class RequireLoginAttribute : AuthorizeAttribute
{
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
        {
            // El usuario no está autenticado, redirigir al inicio de sesión
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Account" },
                    { "action", "Login" }
                });
        }
        else
        {
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}*/