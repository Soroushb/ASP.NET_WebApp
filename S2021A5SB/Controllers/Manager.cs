// **************************************************
// WEB524 Project Template V2 == edb960bf-cb1a-40cd-9a16-8ade9d9cff4a
// Do not change this header.
// **************************************************

using AutoMapper;
using S2021A5SB.EntityModels;
using S2021A5SB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace S2021A5SB.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Album, AlbumWithDetailViewModel>();
                cfg.CreateMap<AlbumAddViewModel, Album>();
                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<Artist, ArtistWithDetailViewModel>();
                cfg.CreateMap<ArtistAddViewModel, Artist>();
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<Genre, GenreBaseViewModel>();


            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()



        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(ds.Genres.OrderBy(c => c.Name));
        }

        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(ds.Artists.OrderBy(c => c.Name));
        }

        public ArtistBaseViewModel ArtistGetById(int? id)
        {
            var artist = ds.Artists.Include("Albums").SingleOrDefault(m => m.Id == id);

            return (artist == null) ? null : mapper.Map<Artist, ArtistWithDetailViewModel>(artist);
        }


        public ArtistBaseViewModel ArtistAdd(ArtistAddViewModel newArtist)
        {
            //var genre = ds.Genres.Find(newArtist.GenreId);
            newArtist.Executive = System.Web.HttpContext.Current.User.Identity.Name;

            if (newArtist.Executive == null)
                return null;

            var addedArtist = ds.Artists.Add(mapper.Map<ArtistAddViewModel, Artist>(newArtist));
            ds.SaveChanges();
            return addedArtist == null ? null : mapper.Map<Artist, ArtistBaseViewModel>(addedArtist);

        }

        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(ds.Albums.OrderBy(c => c.ReleaseDate));
        }

        public AlbumWithDetailViewModel AlbumGetById(int? id)
        {
            var album = ds.Albums.Include("Artists").Include("Tracks").SingleOrDefault(m => m.Id == id);

            return (album == null) ? null : mapper.Map<Album, AlbumWithDetailViewModel>(album);
        }


        public AlbumWithDetailViewModel AlbumAdd(AlbumAddViewModel newItem)
        {
            List<Artist> artistCollection = new List<Artist>();
            foreach (var artistId in newItem.ArtistIds)
            {
                var artist = ds.Artists.SingleOrDefault(m => m.Id == artistId);
                if (artist != null)
                    artistCollection.Add(artist);

            }

            List<Track> trackCollection = new List<Track>();
            if (newItem.TrackIds != null)
            {
                foreach (var trackId in newItem.TrackIds)
                {
                    var track = ds.Tracks.Include("Albums").SingleOrDefault(m => m.id == trackId);
                    if (track != null)
                        trackCollection.Add(track);

                }
            }
            if (artistCollection.Count > 0)
            {
                if (newItem.TrackIds.Count() > 0)
                {
                    var addedAlbum = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newItem));
                    addedAlbum.Artists = artistCollection;
                    addedAlbum.Tracks = trackCollection;
                    addedAlbum.Coordinator = HttpContext.Current.User.Identity.Name;
                    ds.SaveChanges();
                    return (addedAlbum == null) ? null : mapper.Map<Album, AlbumWithDetailViewModel>(addedAlbum);
                   
                }
            }

            return null;

        }

        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks.OrderBy(c => c.Name));
        }

        public TrackWithDetailViewModel TrackGetById(int? id)
        {
            var track = ds.Tracks.Include("Albums.Artists").SingleOrDefault(m => m.id == id);

            if (track == null)
            {
                return null;
            }
            else
            {
                var result = mapper.Map<Track, TrackWithDetailViewModel>(track);
                result.AlbumNames = track.Albums.Select(p => p.Name);
                return result;
            }
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllByArtistId(int id)
        {
            var artist = ds.Artists.Include("Albums.Tracks").SingleOrDefault(a => a.Id == id);

            if (artist == null)
            {
                return null;
            }

            var tracks = new List<Track>();
            foreach (var album in artist.Albums)
            {
                tracks.AddRange(album.Tracks);
            }

            tracks = tracks.Distinct().ToList();
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(tracks.OrderBy(t => t.Name));
        }


        public TrackBaseViewModel TrackAdd(TrackAddViewModel newTrack)
        {

            var album = ds.Albums.Find(newTrack.AlbumId);
            var albums = new List<Album> { album };
            newTrack.Clerk = HttpContext.Current.User.Identity.Name;
         
            var addedTrack = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newTrack));
            addedTrack.Albums = albums;
            ds.SaveChanges();
            return (addedTrack == null) ? null : mapper.Map<Track, TrackWithDetailViewModel>(addedTrack);
        }

        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here
                var executiveRole = new RoleClaim
                {
                    Name = "Executive"
                };

                ds.RoleClaims.Add(executiveRole);

                var coordinatorRole = new RoleClaim
                {
                    Name = "Coordinator"
                };

                ds.RoleClaims.Add(coordinatorRole);

                var clerkRole = new RoleClaim
                {
                    Name = "Clerk"
                };

                ds.RoleClaims.Add(clerkRole);

                var staffRole = new RoleClaim
                {
                    Name = "Staff"
                };

                ds.RoleClaims.Add(staffRole);


                ds.SaveChanges();
                done = true;
            }

            return done;
        }


        public bool LoadDataGenre()
        {
            if (ds.Genres.Count() > 0) { return false; }

            ds.Genres.Add(new Genre { Name = "Folk & Acoustic", });
            ds.Genres.Add(new Genre { Name = "Rock", });
            ds.Genres.Add(new Genre { Name = "Jazz", });
            ds.Genres.Add(new Genre { Name = "Heavy Metal", });
            ds.Genres.Add(new Genre { Name = "Pop", });
            ds.Genres.Add(new Genre { Name = "Indie Rock", });
            ds.Genres.Add(new Genre { Name = "Blues", });
            ds.Genres.Add(new Genre { Name = "Classical", });
            ds.Genres.Add(new Genre { Name = "Hip Hop", });
            ds.Genres.Add(new Genre { Name = "Country", });


            ds.SaveChanges();
            return true;
        }

        public bool LoadDataArtist()
        {

            if (ds.Artists.Count() > 0) { return false; }

            ds.Artists.Add(new Artist { BirthName = "Leonard Norman Cohen", BirthOrStartDate = DateTime.ParseExact("21/09/1934", "dd/MM/yyyy", null), Executive = "soroush@example.com", Genre = "Folk & Acoustic", Name = "Leonard Cohen", UrlArtist = "https://www.biography.com/.image/t_share/MTQyODQ2OTg4OTIzMTg0OTIz/leonard-cohen-gettyimages-109366805_1600jpg.jpg" });

            ds.Artists.Add(new Artist { BirthName = "John Michael Osbourne", BirthOrStartDate = DateTime.ParseExact("03/12/1948", "dd/MM/yyyy", null), Executive = "soroush@example.com", Genre = "Heavy Metal", Name = "Ozzy Osbourne", UrlArtist = "https://www.biography.com/.image/t_share/MTY5ODEzMTM4MzkxMTgxMTE1/ozzy-osbourne-gettyimages-660795784.jpg" });

            ds.Artists.Add(new Artist { BirthName = "Eleanora Fagan", BirthOrStartDate = DateTime.ParseExact("07/04/1915", "dd/MM/yyyy", null), Executive = "soroush@example.com", Genre = "Jazz", Name = "Billie Holiday", UrlArtist = "https://www.gannett-cdn.com/-mm-/e1dafe9a55870728c2402926847e66f82cd2c9fd/c=67-0-919-1136/local/-/media/2015/04/06/USATODAY/USATODAY/635639304740779107-BILLIE-HOLIDAY-spotlight-05.jpg" });


            ds.SaveChanges();
            return true;
        }

        public bool LoadDataAlbum()
        {

            var Leonard = ds.Artists.SingleOrDefault(a => a.Name == "Leonard Cohen");

            if (ds.Albums.Count() > 0) { return false; }

            ds.Albums.Add(new Album
            {
                Artists = new List<Artist> { Leonard },
                Coordinator = "soroush@example.com",
                Genre = "Folk & Acoustic",
                Name = "Songs of Leonard Cohen",
                ReleaseDate = DateTime.ParseExact("27/12/1967", "dd/MM/yyyy", null),
                UrlAlbum = "https://media.pitchfork.com/photos/5929b04bb1335d7bf169a0ba/1:1/w_320/f483135d.jpg"
            });

            ds.Albums.Add(new Album
            {
                Artists = new List<Artist> { Leonard },
                Coordinator = "soroush@example.com",
                Genre = "Folk & Acoustic",
                Name = "I'm Your Man",
                ReleaseDate = DateTime.ParseExact("02/02/1988", "dd/MM/yyyy", null),
                UrlAlbum = "https://media.pitchfork.com/photos/5929bfb3ea9e61561daa7ad5/1:1/w_600/e7caa9a3.jpg"
            });

            ds.SaveChanges();
            return true;
        }

        public bool LoadDataTrack()
        {

            var songOfLeonardCohen = ds.Albums.SingleOrDefault(a => a.Name == "Songs of Leonard Cohen");
            var imYourMan = ds.Albums.SingleOrDefault(a => a.Name == "I'm Your Man");

            if (ds.Tracks.Count() > 0) { return false; }

            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { songOfLeonardCohen },
                Composers = "Leonard Cohen",
                Genre = "Folk & Acoustic",
                Name = "Suzanne"
            });

            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { songOfLeonardCohen },
                Composers = "Leonard Cohen",
                Genre = "Folk & Acoustic",
                Name = "Master Song"
            });

            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { songOfLeonardCohen },
                Composers = "Leonard Cohen",
                Genre = "Folk & Acoustic",
                Name = "Winter Lady"
            });

            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { songOfLeonardCohen },
                Composers = "Leonard Cohen",
                Genre = "Folk & Acoustic",
                Name = "So Long, Marianne"
            });

            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { songOfLeonardCohen },
                Composers = "Leonard Cohen",
                Genre = "Folk & Acoustic",
                Name = "Sisters of Mercy"
            });

            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { imYourMan },
                Composers = "Leonard Cohen",
                Genre = "Folk & Acoustic",
                Name = "I'm Your Man"
            });

            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { imYourMan },
                Composers = "Leonard Cohen",
                Genre = "Folk & Acoustic",
                Name = "First We Take Manhattan"
            });

            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { imYourMan },
                Composers = "Leonard Cohen",
                Genre = "Folk & Acoustic",
                Name = "Everybody Knows"
            });

            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { imYourMan },
                Composers = "Leonard Cohen",
                Genre = "Folk & Acoustic",
                Name = "Take This Waltz"
            });

            ds.Tracks.Add(new Track
            {
                Albums = new List<Album> { imYourMan },
                Composers = "Leonard Cohen",
                Genre = "Folk & Acoustic",
                Name = "Tower of Song"
            });

            ds.SaveChanges();
            return true;

        }





        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}