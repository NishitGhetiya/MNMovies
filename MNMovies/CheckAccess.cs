using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public class CheckAccess : ActionFilterAttribute, IAuthorizationFilter
{
    //public void OnAuthorization(AuthorizationFilterContext filterContext)
    //{
    //    if (filterContext.HttpContext.Session.GetString("UserID") == null)
    //    {
    //        filterContext.Result = new RedirectResult("~/User/UserLogin");
    //    }
    //}
    public void OnAuthorization(AuthorizationFilterContext filterContext)
    {
        var httpContext = filterContext.HttpContext;
        var requestPath = httpContext.Request.Path.Value.ToLower(); // Get current URL

        var userId = httpContext.Session.GetString("UserID");

        if (string.IsNullOrEmpty(userId))
        {
            // Try retrieving user details from cookies
            userId = httpContext.Request.Cookies["UserID"];
            if (!string.IsNullOrEmpty(userId))
            {
                httpContext.Session.SetString("UserID", userId);
                httpContext.Session.SetString("JWTToken", httpContext.Request.Cookies["JWTToken"] ?? "");
                httpContext.Session.SetString("UserName", httpContext.Request.Cookies["UserName"] ?? "");
                httpContext.Session.SetString("Role", httpContext.Request.Cookies["Role"] ?? "");
            }
        }

        // If still no user is found, redirect to login page
        if (string.IsNullOrEmpty(userId))
        {
            filterContext.Result = new RedirectResult("~/User/UserLogin");
            return;
        }

        // Ensure the user is redirected correctly based on role
        var role = httpContext.Session.GetString("Role");

        if (!string.IsNullOrEmpty(role) && role == "True") // Admin user
        {
            // Redirect to /Admin/Index **ONLY IF** they are not already in the admin section
            if (requestPath == "/" || requestPath == "/home/index" || requestPath == "/user/userlogin")
            {
                filterContext.Result = new RedirectResult("~/Admin/Index");
            }
        }
        else // Regular user
        {
            // Prevent non-admin users from accessing the admin panel
            if (requestPath.Contains("/admin"))
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
            }
        }
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
        context.HttpContext.Response.Headers["Expires"] = "-1";
        context.HttpContext.Response.Headers["Pragma"] = "no-cache";
        base.OnResultExecuting(context);
    }
}