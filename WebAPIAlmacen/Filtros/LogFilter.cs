using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPIAlmacen.Filtros
{
    public class LogFilter : ActionFilterAttribute
    {
        private readonly ILogger<LogFilter> _logger;

        public LogFilter(ILogger<LogFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.ActionDescriptor.DisplayName;

            _logger.LogInformation(action);

            base.OnActionExecuting(context);
        }
    }

}
