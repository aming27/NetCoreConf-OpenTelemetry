using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var apiUrl = Environment.GetEnvironmentVariable("DEMO_API_URL");

            using var httpClient = new HttpClient();
            await httpClient.GetAsync(apiUrl);

            return RedirectToPage("Success");
        }
    }
}