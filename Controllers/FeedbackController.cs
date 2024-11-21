using MaxHelp_System_Upgrade.Data;
using MaxHelp_System_Upgrade.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MaxHelp_System_Upgrade.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly DataDbContext _dataDbContext;

        public FeedbackController(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public IActionResult Index(string search = "", string sortBy = "latest")
        {
            var businessUnitId = int.Parse(User.Claims.First(x => x.Type == "BusinessUnitId").Value);

            var feedbacks = _dataDbContext.Feedbacks
                .Where(f => f.BusinessUnitId == businessUnitId)
                .Select(f => new FeedbackViewModel
                {
                    Id = f.Id,
                    SenderEmail = f.SenderEmail,
                    MessageSnippet = $"{f.Message.Substring(0, Math.Min(15, f.Message.Length))}...",
                    DivisionOfComplaint = f.DivisionOfComplaint,
                    DateSentFormatted = f.DateSent,
                    //DateSentFormatted = FormatDate(f.DateSent),
                    IsRead = f.IsRead,
                    EmailIconColor = GetRandomColor(f.Id)
                });

            if(!string.IsNullOrEmpty(search))
            {
                feedbacks = feedbacks.Where(f =>
                    f.SenderEmail.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    f.MessageSnippet.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            feedbacks = sortBy == "earliest" ? feedbacks.OrderBy(f => f.DateSentFormatted) : feedbacks.OrderByDescending(f => f.DateSentFormatted);


            return View(feedbacks.ToList());
        }

        public IActionResult MarkAllAsSeen()
        {
            var businessUnit = int.Parse(User.Claims.First(x => x.Type == "BusinessUnitId").Value);

            var feedbacks = _dataDbContext.Feedbacks.Where(f => f.BusinessUnitId == businessUnit && !f.IsRead).ToList();
            feedbacks.ForEach(f => f.IsRead = true);
            _dataDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private string FormatDate(DateTime date)
        {
            var now = DateTime.Now;

            if (date.Date == now.Date) return $"Today, {date:hh:mm tt}";
            if (date.Date == now.AddDays(-1).Date) return $"Yesterday, {date:hh:mm tt}";
            if (date.Date > now.AddDays(-7).Date) return $"{date:dddd, hh:mm tt}";

            return date.ToString("MMM d, yyyy");
        }

        private static string GetRandomColor(int id)
        {
            var colors = new[] { "Yellow", "Red", "Blue", "Green", "Purple" };
            return colors[id % colors.Length];
        }
    }
}
