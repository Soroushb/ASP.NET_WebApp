using S2021A5SB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A5SB.Controllers
{
    public class AlbumsController : Controller
    {
        Manager m = new Manager();
        // GET: Albums
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            var album = m.AlbumGetById(id.GetValueOrDefault());

            if (album == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(album);
            }
        }



        [Authorize(Roles = "Clerk"), Route("albums/{id}/addtrack")]
        public ActionResult AddTrack(int? id)
        {
            var album = m.AlbumGetById(id.GetValueOrDefault());


            if (album == null)
            {
                return HttpNotFound();
            }
            else
            {
                var trackAdd = new TrackAddFormViewModel();
                trackAdd.AlbumId = album.Id;
                trackAdd.AlbumName = album.Name;
                trackAdd.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");

                return View(trackAdd);
            }
        }

        [Authorize(Roles = "Clerk"), Route("albums/{id}/addtrack")]
        [HttpPost]
        public ActionResult AddTrack(TrackAddViewModel track)
        {

            if (!ModelState.IsValid)
                return View(track);


            var addedTrack = m.TrackAdd(track);

            if (addedTrack == null)
                return View();
            else
                return RedirectToAction("details", "tracks", new { id = addedTrack.id });



        }

        // POST: Albums/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Albums/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Albums/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
