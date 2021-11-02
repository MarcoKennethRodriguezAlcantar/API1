using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API1.Models;

namespace API1.Controllers
{
    public class UsuariosController : ApiController
    {
        private IDGS01_Api1Entities db = new IDGS01_Api1Entities();

        // GET: api/Usuarios
        public IQueryable<DBUsuario> GetDBUsuario()
        {
            return db.DBUsuario;
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(DBUsuario))]
        public IHttpActionResult GetDBUsuario(int id)
        {
            DBUsuario dBUsuario = db.DBUsuario.Find(id);
            if (dBUsuario == null)
            {
                return NotFound();
            }

            return Ok(dBUsuario);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDBUsuario(int id, DBUsuario dBUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dBUsuario.Id)
            {
                return BadRequest();
            }

            db.Entry(dBUsuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DBUsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Usuarios
        [ResponseType(typeof(DBUsuario))]
        public IHttpActionResult PostDBUsuario(DBUsuario dBUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DBUsuario.Add(dBUsuario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dBUsuario.Id }, dBUsuario);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(DBUsuario))]
        public IHttpActionResult DeleteDBUsuario(int id)
        {
            DBUsuario dBUsuario = db.DBUsuario.Find(id);
            if (dBUsuario == null)
            {
                return NotFound();
            }

            db.DBUsuario.Remove(dBUsuario);
            db.SaveChanges();

            return Ok(dBUsuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DBUsuarioExists(int id)
        {
            return db.DBUsuario.Count(e => e.Id == id) > 0;
        }
    }
}