using System.Web.Mvc;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;
using July09v31.UI.Models.Forms;
using MvcContrib;

namespace July09v31.UI.Controllers
{
    public class AdminController : SmartController
    {
        private readonly IUserRepository _repository;

        public AdminController(IUserRepository repository)
        {
            _repository = repository;
        }


        public ActionResult Index(Conference conference)
        {
            User user = _repository.GetByUserName("admin");
            if (user == null)
            {
                return RedirectToAction<UserController>(c => c.Edit(null));
            }
            var model = new AdminForm() { ConferenceIsSelected = conference != null };

            return View(model);
        }
        public ActionResult AttendeeEmail(Conference conference)
        {
            ViewData.Add(conference.GetAttendees());

            return View();
        }
    }
}