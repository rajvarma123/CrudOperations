using CrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CrudOperations.Controllers
{
    public class EmployeeDetailsController : Controller
    {
        // GET: EmployeeDetails
        public ActionResult Index()
        {
            IEnumerable<Employee> employees = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61465/Api/");
                //HTTP GET
                var responseTask = client.GetAsync("Employee");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Employee>>();
                    readTask.Wait();

                    employees = readTask.Result;
                }
                else 
                { 

                    employees = Enumerable.Empty<Employee>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(employees);
        }
     
        public ActionResult Create()
        {
            return View();
        }

      
        public ActionResult Create(Employee employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61465/Api/Employee");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Employee>("Employee", employee);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }
            }
            return View(employee);
        }



        public ActionResult Edit(int id)
        {
            Employee employee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61465/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Employee?id=" + id.ToString());
                responseTask.Wait();


                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Employee>();
                    readTask.Wait();
                     employee = readTask.Result;
                }
            }

            return View(employee);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61465/Api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Employee/" + id.ToString());
                deleteTask.Wait();
               

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");


        }
    }
}
