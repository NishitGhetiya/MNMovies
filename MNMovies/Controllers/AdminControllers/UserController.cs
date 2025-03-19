using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MNMovies.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace MNMovies.Controllers.AdminControllers
{
    public class UserController : Controller
    {
        #region Uri
        private readonly Uri baseAddress = new Uri("https://localhost:44301/api");
        private readonly HttpClient _Client;
        
        public UserController()
        {
            _Client = new HttpClient { BaseAddress = baseAddress }; 
        }
        #endregion

        [CheckAccess]
        #region UserList
        [HttpGet]
        [Route("/User")]
        public IActionResult UserList()
        {
            AddAuthorizationHeader();

            List<UserModel> userList = new List<UserModel>();
            HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/User").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                userList = JsonConvert.DeserializeObject<List<UserModel>>(data);
            }
            return View("UserList", userList);
        }
        #endregion

        [CheckAccess]
        #region UserAddEdit
        [HttpGet]
        [Route("/User/UserAddEdit")]
        public IActionResult UserAddEdit(int? UserID)
        {
            AddAuthorizationHeader();

            UserModel model = new UserModel();

            if (UserID.HasValue && UserID > 0)
            {
                HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/User/{UserID}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<UserModel>(data);
                }
            }

            return View("UserAddEdit", model);
        }
        #endregion

        #region UserSave
        [HttpPost]
        public IActionResult UserSave(UserModel model)
        {
            if (ModelState.IsValid)
            {
                AddAuthorizationHeader();

                HttpResponseMessage response;
                if (model.UserID == null)
                {
                    // Add operation
                    response = _Client.PostAsJsonAsync($"{_Client.BaseAddress}/User", model).Result;
                }
                else
                {
                    // Edit operation
                    response = _Client.PutAsJsonAsync($"{_Client.BaseAddress}/User/{model.UserID}", model).Result;
                }

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("UserList");
                }

                var errorMessage = response.Content.ReadAsStringAsync().Result;
                TempData["ErrorMassage"] = $"Failed to save the User: {errorMessage}";

            }

            // Return to the form if validation or API call fails
            return View("UserAddEdit", model);
        }
        #endregion

        #region UserDelete
        [HttpPost]
        public IActionResult UserDelete(int UserID)
        {
            AddAuthorizationHeader();

            HttpResponseMessage response = _Client.DeleteAsync($"{_Client.BaseAddress}/User/{UserID}").Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("UserList");
            }
            else
            {
                var errorMessage = response.Content.ReadAsStringAsync().Result;
                TempData["ErrorMassage"] = $"Error deleting the User. Error : {errorMessage}";
                return RedirectToAction("UserList");
            }
        }
        #endregion

        #region UserLogin
        [HttpGet]
        [Route("/User/UserLogin")]
        public IActionResult UserLogin()
        {
            return View("UserLogin");
        }

        //[HttpPost]
        //[Route("/User/UserLogin")]
        //public IActionResult UserLogin(UserLoginModel loginModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        HttpResponseMessage response = _Client.PostAsJsonAsync($"{_Client.BaseAddress}/User/Login", loginModel).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string data = response.Content.ReadAsStringAsync().Result;

        //            // Deserialize into strongly-typed model
        //            var responseData = JsonConvert.DeserializeObject<UserLoginResponseModel>(data);

        //            if (responseData != null)
        //            {
        //                // Extract user and token
        //                var token = responseData.Token;
        //                var user = responseData.User;

        //                // Store token and user details in session
        //                HttpContext.Session.SetString("JWTToken", token);
        //                HttpContext.Session.SetString("UserID", user.UserID.ToString());
        //                HttpContext.Session.SetString("UserName", user.UserName);
        //                HttpContext.Session.SetString("Role", user.Role.ToString());

        //                // Redirect based on user role
        //                return user.Role ? RedirectToAction("Index", "Admin") : RedirectToAction("Index", "Home");
        //            }
        //        }

        //        TempData["ErrorMessage"] = "Invalid login credentials.";
        //    }

        //    return View("UserLogin", loginModel);
        //}
        [HttpPost]
        [Route("/User/UserLogin")]
        public IActionResult UserLogin(UserLoginModel loginModel, bool remember)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = _Client.PostAsJsonAsync($"{_Client.BaseAddress}/User/Login", loginModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var responseData = JsonConvert.DeserializeObject<UserLoginResponseModel>(data);

                    if (responseData != null)
                    {
                        var token = responseData.Token;
                        var user = responseData.User;

                        // Store token in session
                        HttpContext.Session.SetString("JWTToken", token);
                        HttpContext.Session.SetString("UserID", user.UserID.ToString());
                        HttpContext.Session.SetString("UserName", user.UserName);
                        HttpContext.Session.SetString("Role", user.Role.ToString());

                        // Store token in cookie if "Remember Me" is checked
                        if (remember)
                        {
                            var cookieOptions = new CookieOptions
                            {
                                HttpOnly = true,
                                Secure = true,
                                Expires = DateTime.UtcNow.AddDays(30) // Set expiration for 30 days
                            };
                            Response.Cookies.Append("JWTToken", token, cookieOptions);
                            Response.Cookies.Append("UserID", user.UserID.ToString(), cookieOptions);
                            Response.Cookies.Append("UserName", user.UserName, cookieOptions);
                            Response.Cookies.Append("Role", user.Role.ToString(), cookieOptions);
                        }

                        //return user.Role ? RedirectToAction("Index", "Admin") : RedirectToAction("Index", "Home");
                        if (user.Role)
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }
                }

                TempData["ErrorMessage"] = "Invalid login credentials.";
            }

            return View("UserLogin", loginModel);
        }

        #endregion

        #region UserRegister
        [HttpGet]
        [Route("/User/UserRegister")]
        public IActionResult UserRegister()
        {
            AddAuthorizationHeader();

            return View("UserRegister");
        }

        [HttpPost]
        [Route("/User/UserRegister")]
        public IActionResult UserRegister(UserRegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                AddAuthorizationHeader();

                HttpResponseMessage response = _Client.PostAsJsonAsync($"{_Client.BaseAddress}/User/Register", registerModel).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMassage"] = "Registration successful! Please login.";
                    //return RedirectToAction("UserLogin");
                }

                TempData["ErrorMassage"] = "Failed to register the user. Please try again.";
            }

            return View("UserRegister", registerModel);
        }
        #endregion

        #region UserLogout
        //[Route("/user/logout")]
        //public IActionResult logout()
        //{
        //    HttpContext.Session.Clear();
        //    return RedirectToAction("UserLogin", "User");
        //}
        [Route("/user/logout")]
        public IActionResult logout()
        {
            Response.Cookies.Delete("JWTToken");
            Response.Cookies.Delete("UserID");
            Response.Cookies.Delete("UserName");
            Response.Cookies.Delete("Role");

            return RedirectToAction("UserLogin", "User");
        }

        #endregion

        #region UserProfile
        [HttpGet]
        [Route("/User/UserProfile")]
        public IActionResult UserProfile()
        {
            AddAuthorizationHeader();

            // Retrieve the logged-in user's ID from the session
            var userIdString = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                TempData["ErrorMessage"] = "User not found. Please log in again.";
                return RedirectToAction("UserLogin");
            }

            UserModel userProfile = new UserModel();
            HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/User/{userId}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                userProfile = JsonConvert.DeserializeObject<UserModel>(data);
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to load user profile.";
            }

            return View("UserProfile", userProfile);
        }
        #endregion

        #region AddAuthorizationHeader
        private void AddAuthorizationHeader()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (!string.IsNullOrEmpty(token))
            {
                _Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
        #endregion
    }
}
