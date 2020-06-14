using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AniversarioDados;
using AniversarioModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empresa.WebApp.Controllers
{

    public class AniversarioController : Controller
    {
        public static List<Aniversariantes> Todos { get; set; } = new List<Aniversariantes>();
        // GET: Funcionarios
        public ActionResult Index()
        {
            var aniversario = BancoDeDadosEmMemoria.BuscarTodosOsAniversarios();

            return View(aniversario);
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Funcionarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Aniversariantes aniversario)
        {
            try
            {
                // TODO: Add insert logic here

                BancoDeDadosEmMemoria.Salvar(aniversario);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Buscar()
        {
            return View();
        }

        // POST: Funcionarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Busca(string nomeBusca)
        {
            var todo = from m in Todos
                       select m;
            if (!String.IsNullOrEmpty(nomeBusca))
            {
                todo = Todos.Where(x => x.nomeCompleto.Contains(nomeBusca));
            }
            return View(await todo.ToListAsync());
        }
        /*InvalidOperationException: The model item passed into the ViewDataDictionary is of type 'System.Linq.Enumerable+WhereListIterator`
         * [AniversarioModel.Aniversariantes]', but this ViewDataDictionary instance requires a model item of type 'AniversarioModel.Aniversariantes'.*/
        // GET: Funcionarios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Funcionarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Funcionarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Funcionarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}