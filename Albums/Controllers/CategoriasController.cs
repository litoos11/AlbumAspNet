using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Albums.Models;
using Albums.Services;
using System.IO;

namespace Albums.Controllers
{
    public class CategoriasController : Controller
    {
        private AlbumsDbContext db = new AlbumsDbContext();        

        // GET: Categorias
        public async Task<ActionResult> Index()
        {
            return View(await db.Categorias.ToListAsync());
            //var Lista = _service.getCategorias();
            //return View( _service.getCategorias());
        }

        // GET: Categorias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Categoria categoria = await db.Categorias.FindAsync(id);
            Categoria categoria = await db.Categorias.Include(x => x.FilePaths).SingleOrDefaultAsync(x => x.Id == id);

            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,Descripcion")] Categoria categoria, HttpPostedFileBase upload)
        {
                        
            if (ModelState.IsValid)
            {                
                if(upload != null && upload.ContentLength > 0) {
                    var random = new Random();
                    var nombre = DateTime.Now.ToShortDateString().Replace("/", "") + "_" + random.Next(1, int.MaxValue).ToString() + Path.GetExtension(upload.FileName);
                    var foto = new FilePath {
                        Nombre = nombre,//Path.GetFileName(upload.FileName),
                        TipoArchivo = FileType.Foto
                    };
                    var path = Path.Combine(Server.MapPath("~/Uploads/Img"), foto.Nombre);
                    upload.SaveAs(path);
                    //var foto = _service.UploadFile(upload);

                    categoria.FilePaths = new List<FilePath>();
                    categoria.FilePaths.Add(foto);
                }
                //_service.saveCategoria(categoria);

                db.Categorias.Add(categoria);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Descripcion")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Categoria categoria = await db.Categorias.FindAsync(id);
            db.Categorias.Remove(categoria);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Categorias/About
        public ActionResult About() {
            return View();
        }
    }
}
