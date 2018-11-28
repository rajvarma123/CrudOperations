using CrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrudOperations.Controllers
{
    public class EmployeeController : ApiController
    {
        static List<Employee> Employees = new List<Employee>()
        {
            new Employee(){Id=1,Name="Raj",Gender="Male",Department="Cse",City="Ban"},
            new Employee(){Id=2,Name="Varma",Gender="Male",Department="Cse",City="Hyd"},
            new Employee(){Id=3,Name="Swathi",Gender="Female",Department="Ece",City="Tpt"}
        };
        public IEnumerable<Employee> GetAllEmployees()
        {
            return Employees.ToList();
        }
        public Employee Get(int id)
        {
            return Employees.SingleOrDefault(x => x.Id == id);
        }
        public HttpResponseMessage Post(Employee employee)
        {
            Employees.Add(employee);
            var response = Request.CreateResponse(HttpStatusCode.Created);
            return response;

        }
        public HttpResponseMessage Delete(int id)
        {
            var item = Employees.SingleOrDefault(x => x.Id == id);
            Employees.Remove(item);
            var response= Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
        [HttpPut]
        public HttpResponseMessage Put(int id,Employee employee)
        {
            var item = Employees.SingleOrDefault(x => x.Id == id);
            item.Id = employee.Id;
            item.Name = employee.Name;
            item.Gender = employee.Gender;
            item.Department = employee.Department;
            item.City = employee.City;
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}
