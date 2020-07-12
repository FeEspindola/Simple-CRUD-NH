using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;
using SimpleCRUD.Site.Data.Interfaces;
using SimpleCRUD.Site.Entities;

namespace SimpleCRUD.Site.Controllers
{
    public class ClientController : Controller
    {
        private readonly INHMapperSession nhMapperSession;

        public ClientController(INHMapperSession nhMapperSession)
        {
            this.nhMapperSession = nhMapperSession;
        }

        [HttpGet("get-all-clients")]
        public IActionResult Index()
        {
            var clients = nhMapperSession.GetAll().ToList();
            return View(clients);
        }

        [HttpGet]
        public IActionResult Create()
            => View();

        [HttpPost]
        public async Task<IActionResult> Create(Client model)
        {
            try
            {
                nhMapperSession.StartTransaction();

                await nhMapperSession.Save(model);
                await nhMapperSession.Commit();
            }
            catch (Exception e)
            {
                await nhMapperSession.Rollback();
            }
            finally
            {
                nhMapperSession.EndTransaction();
            }
         


            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{Id:Guid}")]
        public  async Task<IActionResult> Edit(Guid id)
        {
            var client = await nhMapperSession.Get(id);

            if (client == null) return RedirectToAction(nameof(Index));

            return View(client);
        }

        [HttpPost("{Id:Guid}")]

        public async Task<IActionResult> Edit(Guid id, Client client)
        {
            if (id != client.Id) return RedirectToAction(nameof(Index));

            try
            {
                nhMapperSession.StartTransaction();
                await nhMapperSession.Update(client);
                await nhMapperSession.Commit();

            }
            catch (Exception e)
            {
                nhMapperSession.EndTransaction();
            }
            finally
            {
                nhMapperSession.EndTransaction();
            }
      


            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{Id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var client = await nhMapperSession.Get(id);

            if (client == null) return RedirectToAction(nameof(Index));

            return View(client);
            
        }

        [HttpPost("{Id:Guid}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(Guid id, Client client)
        {

            if (id != client.Id) return RedirectToAction(nameof(Index));


            try
            {
                nhMapperSession.StartTransaction();
                await nhMapperSession.Delete(client);
                await nhMapperSession.Commit();
            }
            catch (Exception e)
            {
                await nhMapperSession.Rollback();
            }
            finally
            {
                nhMapperSession.EndTransaction();
            }
            return View();
        }


    }
}
