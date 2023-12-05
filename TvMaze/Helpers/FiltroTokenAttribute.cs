using System;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace Test.TvMaze.Helpers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FiltroTokenAttribute : ActionFilterAttribute
    {

        HttpActionContext _actionContext { get; set; }

        // Método que se ejecuta antes de la acción del controlador.
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            _actionContext = actionContext;

            string actionName = actionContext.ActionDescriptor.ActionName;
            string controllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            HandleError("Invalid Token", HttpStatusCode.BadRequest);
            var user = actionContext.RequestContext.Principal;
            Token token = new Token();
            try
            {
                token = ObtenerToken();
                if (token == null)
                {
                    HandleError("Invalid Token", HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                HandleError(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            string actionName = _actionContext.ActionDescriptor.ActionName;
            string controllerName = _actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            base.OnActionExecuted(actionExecutedContext);
        }
        private Token ObtenerToken()
        {
            Token token = new Token();
            var user = _actionContext.RequestContext.Principal;
            JwtService jwtService = new JwtService();
            token = jwtService.ObtenerInformacionToken(user.Identity as ClaimsIdentity);
            return token;
        }

        private void HandleError(string validacion, HttpStatusCode httpStatusCode)
        {
            using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "log.txt", true))
            {
                writer.WriteLine($"Error: {validacion}, Status Code: {httpStatusCode}");
            }
            return;
        }

        private void LogError(Exception ex)
        {
            HandleError(string.Format("Error: {0}", ex.Message), HttpStatusCode.InternalServerError);
            throw new Exception(ex.Message);

        }
    }
}
