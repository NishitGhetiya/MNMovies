public class CommonVariable
{
    private static IHttpContextAccessor _HttpContextAccessor;

    static CommonVariable()
    {
        _HttpContextAccessor = new HttpContextAccessor();
    }


    public static int? UserID()
    {

        if (_HttpContextAccessor.HttpContext.Session.GetString("UserID") == null)
        {
            return null;
        }

        return Convert.ToInt32(_HttpContextAccessor.HttpContext.Session.GetString("UserID"));
    }

    public static string UserName()
    {
        if (_HttpContextAccessor.HttpContext.Session.GetString("UserName") == null)
        {
            return null;
        }

        return _HttpContextAccessor.HttpContext.Session.GetString("UserName");
    }

    public static bool? Role()
    {

        if (_HttpContextAccessor.HttpContext.Session.GetString("Role") == null)
        {
            return null;
        }

        return Convert.ToBoolean(_HttpContextAccessor.HttpContext.Session.GetString("Role"));
    }

}