using Microsoft.AspNetCore.Mvc;
using MyMainWebMVC.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
//using MyMainWebMVC.Models;
using Newtonsoft.Json;

namespace MyMainWebMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        //employee
        public async Task<IActionResult> Index()
        {
            var employees = await GetEmployeesAsync();

            return View("EmployeeView", employees);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Create()
        {
            Employee employee = new Employee();
            return View("CreateEmployee", employee);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await PostEmployeeAsync(employee);

                if (isSuccess)
                {
                    return RedirectToAction("Index");  // Redirect to Index or success page
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to insert employee.");
                }
            }
            return View(employee);
        }
        private async Task<bool> PostEmployeeAsync(Employee employee)
        {
            HttpClient client = new HttpClient();
            // Set base address and headers
            client.BaseAddress = new Uri("https://localhost:7053/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Serialize the department object to JSON
            var json = System.Text.Json.JsonSerializer.Serialize(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Send the POST request to the Web API
            HttpResponseMessage response = await client.PostAsync("api/Employees", content);

            // Return true if the request was successful
            return response.IsSuccessStatusCode;
        }
        private static async Task<List<Employee>> GetEmployeesAsync()
        {
            HttpClient client = new HttpClient();
            // Set the base address of the API
            client.BaseAddress = new Uri("https://localhost:7053/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Call the API
            HttpResponseMessage response = await client.GetAsync("api/Employees");

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Deserialize the JSON response to a List<Department>
                var responseData = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<List<Employee>>(responseData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else
            {
                throw new Exception("Failed to retrieve employees from API");
            }

        }




        //Edit employee

        public async Task<IActionResult> Edit(int id)
        {
            Employee employee = new Employee();
            employee = await GetEmployeesAsync(id);
            return View("EditEmployee", employee);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            id = employee.EmpId;
            HttpClient client = new HttpClient();

            string apiUrl = $"https://localhost:7053/api/Employees/{id}";
            var jsonContent = JsonConvert.SerializeObject(employee);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // or appropriate action
            }
            else
            {
                ModelState.AddModelError("", "Error updating employee.");
                return View("EditEmployee", employee);
            }
        }

        private static async Task<Employee> GetEmployeesAsync(int id)
        {
            HttpClient client = new HttpClient();
            // Set the base address of the API
            client.BaseAddress = new Uri("https://localhost:7053/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"api/Employees/{id}");
            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Deserialize the JSON response to a List<Employee>
                var responseData = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<Employee>(responseData,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else
            {
                throw new Exception("Failed to retrieve employees from API");
            }
        }




        //Delete Employee

        // DELETE Employee by ID
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccess = await DeleteEmployeeAsync(id);

            if (isSuccess)
            {
                return RedirectToAction("Index"); // Redirect to Index or success page
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete employee.");
                return RedirectToAction("Index");
            }
        }

        // Method to send DELETE request to the Web API
        private async Task<bool> DeleteEmployeeAsync(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7053/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.DeleteAsync($"api/Employees/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}

