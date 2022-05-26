using S2021A5SB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A5SB.Controllers
{
    public class ArtistsController : Controller
    {
        private Manager m = new Manager();

        // GET: Artists
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            var artist = m.ArtistGetById(id.GetValueOrDefault());

            if (artist == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(artist);
            }

        }

        // GET: Artists/Create
        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
            var artist = new ArtistAddFormViewModel();
            artist.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
            return View(artist);
        }

        // POST: Artists/Create
        [Authorize(Roles = "Executive")]
        [HttpPost]
        public ActionResult Create(ArtistAddViewModel newArtist)
        {

            if (!ModelState.IsValid)
                return View();

            // TODO: Add insert logic here
            var addedItem = m.ArtistAdd(newArtist);

            if (addedItem == null)
                return View();
            else
                return RedirectToAction("Details", new { id = addedItem.Id });


        }


        [Authorize(Roles = "Coordinator"), Route("artists/{id}/addalbum")]
        public ActionResult AddAlbum(int? id)
        {
            var artist = m.ArtistGetById(id.GetValueOrDefault());

            if (artist == null)
            {
                return HttpNotFound();
            }
            else
            {
                var albumAdd = new AlbumAddFormViewModel();
                albumAdd.ArtistName = artist.Name;
                albumAdd.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                albumAdd.Id = artist.Id;
                var selected = new List<int> { artist.Id };
                albumAdd.ArtistList = new MultiSelectList(m.ArtistGetAll(), "Id", "Name", selected);
                albumAdd.TrackList = new MultiSelectList(m.TrackGetAllByArtistId(artist.Id), "Id", "Name");
                return View(albumAdd);
            }
        }

        [Authorize(Roles = "Coordinator"), Route("artists/{id}/addalbum")]
        [HttpPost]
        public ActionResult AddAlbum(AlbumAddViewModel album)
        {
           
                if (!ModelState.IsValid)
                    return View(album);
                

                var addedAlbum = m.AlbumAdd(album);

                if (addedAlbum == null)
                    return View();
                else
                  return RedirectToAction("details", "albums", new { id = addedAlbum.Id });
                
        }

        // GET: Artists/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }


        // POST: Artists/Edit/5
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

        // GET: Artists/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artists/Delete/5
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
