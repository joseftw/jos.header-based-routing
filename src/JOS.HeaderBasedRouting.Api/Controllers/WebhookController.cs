using Microsoft.AspNetCore.Mvc;

namespace JOS.HeaderBasedRouting.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        [WebhookOperation(WebhookHeader.Operation, WebhookOperation.UserAdded)]
        public IActionResult UserAdded()
        {
            return new OkObjectResult($"Hello from {nameof(UserAdded)} action");
        }

        [HttpPost]
        [WebhookOperation(WebhookHeader.Operation, WebhookOperation.UserRemoved)]
        public IActionResult UserRemoved()
        {
            return new OkObjectResult($"Hello from {nameof(UserRemoved)} action");
        }

        [HttpPost]
        [WebhookOperation(WebhookHeader.Operation, WebhookOperation.UserUpdated)]
        public IActionResult UserUpdated()
        {
            return new OkObjectResult($"Hello from {nameof(UserUpdated)} action");
        }
    }
}
