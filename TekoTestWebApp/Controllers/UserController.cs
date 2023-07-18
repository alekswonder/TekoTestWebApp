using Microsoft.AspNetCore.Mvc;
using TekoTestWebApp.Interfaces;
using TekoTestWebApp.Models;
using TekoTestWebApp.ViewModels;

namespace TekoTestWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<User> users = await _userRepository.GetAllAsync();
            return View(users);
        }

        public async Task<IActionResult> Detail(int id)
        {
            User user = await _userRepository.GetByIdAsync(id);
            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = userViewModel.Username,
                    Password = userViewModel.Password,
                    FullName = userViewModel.FullName,
                    Position = userViewModel.Position,
                    Department = userViewModel.Department,
                    Age = userViewModel.Age,
                    RegistrationTime = DateTime.Now,
                    Gender = userViewModel.Gender,
                };
                _userRepository.Add(user);
                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return View("Error");
            var userViewModel = new EditUserViewModel
            {
                Username = user.Username,
                Password = user.Password,
                FullName = user.FullName,
                Position = user.Position,
                Department = user.Department,
                Age = user.Age,
                Gender = user.Gender
            };
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditUserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit race");
                return View("Error", userViewModel);
            }

            var currentUser = await _userRepository.GetByIdNoTracking(id);
            if (currentUser != null)
            {
                var tempUser = new User
                {
                    Id = id,
                    Username = userViewModel.Username,
                    Password = userViewModel.Password,
                    FullName = userViewModel.FullName,
                    Position = userViewModel.Position,
                    Department = userViewModel.Department,
                    Age = userViewModel.Age,
                    Gender = userViewModel.Gender
                };
                _userRepository.Update(tempUser);
                return RedirectToAction("Index");
            }

            return View(userViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userDetails = await _userRepository.GetByIdAsync(id);
            if (userDetails != null) 
            {
                _userRepository.Delete(userDetails);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
