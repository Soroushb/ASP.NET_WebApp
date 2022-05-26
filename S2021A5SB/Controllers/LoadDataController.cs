using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A5SB.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class LoadDataController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: LoadData
        public ActionResult Index()
        {
            if (m.LoadData())
            {
                return Content("data has been loaded");
            }
            else
            {
                return Content("data exists already");
            }
        }

        // GET: LoadData/Genre
        public ActionResult Genre()
        {
            if (m.LoadDataGenre())
            {
                return Content("Genre data has been loaded");
            }
            else
            {
                return Content("Genre data exists already");
            }
        }

        public ActionResult Artist()
        {
            if (m.LoadDataArtist())
            {
                return Content("Artist data has been loaded");
            }
            else
            {
                return Content("Artist data exists already");
            }
        }

        public ActionResult Album()
        {
            if (m.LoadDataAlbum())
            {
                return Content("Album data has been loaded");
            }
            else
            {
                return Content("Album data exists already");
            }
        }

        public ActionResult Track()
        {
            if (m.LoadDataTrack())
            {
                return Content("Track data has been loaded");
            }
            else
            {
                return Content("Track data exists already");
            }
        }

        public ActionResult Remove()
        {
            if (m.RemoveData())
            {
                return Content("data has been removed");
            }
            else
            {
                return Content("could not remove data");
            }
        }

        public ActionResult RemoveDatabase()
        {
            if (m.RemoveDatabase())
            {
                return Content("database has been removed");
            }
            else
            {
                return Content("could not remove database");
            }
        }

    }
}