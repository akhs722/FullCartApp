using Microsoft.AspNetCore.Mvc;

namespace FullCartAPI.BaseClasses
{
    public class ExceptionHandler : ControllerBase
    {
        public virtual async Task<IActionResult> HandleRequestAsync(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action.Invoke();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
