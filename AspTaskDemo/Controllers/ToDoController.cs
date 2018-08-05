using AspTaskDemo.Models;
using Bogus;
using System.Linq;
using System.Web.Mvc;

namespace AspTaskDemo.Controllers
{
    namespace WebApplication2.Controllers
    {
        public class ToDoController : Controller
        {
            public System.Collections.Generic.List<Task> tasks;

            // GET: Default
            public ActionResult Index()
            {
                tasks = Session["TaskList"] as System.Collections.Generic.List<Task>;

                if (tasks is null)
                {
                    // https://github.com/bchavez/Bogus
                    var testTasks = new Faker<Task>()
                        .RuleFor(d => d.Description, (f, d) => f.Lorem.Sentence());

                    tasks = testTasks.Generate(new System.Random().Next(3, 8));

                    Session.Add("TaskList", tasks);
                }

                return View(tasks);
            }

            [HttpPost]
            public ActionResult Add(string newTask)
            {
                if(!string.IsNullOrEmpty(newTask))
                {
                    tasks = Session["TaskList"] as System.Collections.Generic.List<Task>;
                    tasks.Add(new Task() { Description = newTask });
                }
                
                return View("Index", tasks);
            }

            [HttpPost]
            public ActionResult Remove(string taskValue)
            {
                tasks = Session["TaskList"] as System.Collections.Generic.List<Task>;

                tasks.RemoveAll(x => x.Description.Equals(taskValue));

                return View("Index", tasks);
            }

            [HttpPost]
            public ActionResult Update(string taskValue, bool isCompleted)
            {
                tasks = Session["TaskList"] as System.Collections.Generic.List<Task>;
                
                var task = tasks.FirstOrDefault(c => c.Description.Equals(taskValue));

                if(task!=null)
                {
                    task.IsCompleted = isCompleted;
                }
 
                return View("Index", tasks);
            }
        }
    }
}