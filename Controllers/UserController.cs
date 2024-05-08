using CRUD_application_2.Models;
using System.Linq;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();
        // GET: User
        public ActionResult Index()
        {
            // write code to return the list of users
            return View(userlist);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            // write code to return a the details of a user with the specified id
            return View(userlist.FirstOrDefault(x => x.Id == id));
        }

        // GET: User/Create
        public ActionResult Create()
        {
            // write code to return the view to create a user
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            // write code to implement the POST method to create a user from the create view
            user.Id = userlist.Count + 1;
            userlist.Add(user);
            return RedirectToAction("Index");
        }
 
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // This method is responsible for displaying the view to edit an existing user with the specified ID.
            // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.
            // Write code to display the view to edit an existing user with the specified ID
            return View(userlist.FirstOrDefault(x => x.Id == id));
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            // This method is responsible for handling the HTTP POST request to update an existing user with the specified ID.
            // It receives user input from the form submission and updates the corresponding user's information in the userlist.
            // If successful, it redirects to the Index action to display the updated list of users.
            // If no user is found with the provided ID, it returns a HttpNotFoundResult.
            // If an error occurs during the process, it returns the Edit view to display any validation errors.
            // Write code to handle the HTTP POST request to update an existing user with the specified ID
            var userToUpdate = userlist.FirstOrDefault(x => x.Id == id);
            if (userToUpdate != null)
            {
                userToUpdate.Name = user.Name;
                userToUpdate.Email = user.Email;
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Implement code to return the view to delete a user with the specified ID
            return View(userlist.FirstOrDefault(x => x.Id == id));
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Implement the Delete method (POST) here
            var userToDelete = userlist.FirstOrDefault(x => x.Id == id);
            if (userToDelete != null)
            {
                userlist.Remove(userToDelete);
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }
    }
}
