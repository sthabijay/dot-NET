using CRUD.Data;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationContext context;

        public EmployeeController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var data = context.Employees.ToList();  
            return View(data);
        }

        public IActionResult Create() 
        {
            return View();       
        }

        [HttpPost]
        public IActionResult Create(Employees Model)
        {
            if (ModelState.IsValid)
            {
                var data = new Employees
                {
                    Name = Model.Name,
                    Salary = Model.Salary,
                };
                context.Employees.Add(data);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(Model);
            }
        }

        public IActionResult Delete(int id)
        {
            var emp = context.Employees.FirstOrDefault(x => x.Id == id);
            if (emp != null)
            {
                context.SaveChanges();
            }
            context.Employees.Remove(emp);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var emp = context.Employees.FirstOrDefault(x => x.Id == id);
            var result = new Employees
            {
                Name = emp.Name,
                Salary = emp.Salary,
            };
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(Employees Model)
        {
            if (ModelState.IsValid)
            {
                var emp = context.Employees.FirstOrDefault(x => x.Id == Model.Id);

                if (emp != null)
                {
                    emp.Name = Model.Name;
                    emp.Salary = Model.Salary;
                }
                else
                {
                    return NotFound();
                }

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                 return View(Model);
            }
           
        }
    }

}
