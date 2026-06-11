using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages
{
    public class EditorModel : PageModel
    {
        private DataContext context;
        public Product Product { get; set; }
        public EditorModel(DataContext ctx)
        {
            context = ctx;
        }
        public async Task OnGetAsync(long id)
        {
            Product = await context.Products.FindAsync(id);
        }
        public async Task<IActionResult> OnPostAsync(long id, decimal price)
        {
            Product p = await context.Products.FindAsync(id);
            p.Price = price;
            await context.SaveChangesAsync();
            return RedirectToPage();    /*redirects the client to the URL for the Razor Page. This may seem odd, but the effect is to tell the browser to send a GET request
                                        to the URL it used for the POST request. This type of redirection means that the browser won’t resubmit the POST request if the
                                        user reloads the browser, preventing the same action from being accidentally performed more than once.*/
        }
    }
}