using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Devtalk.EF.CodeFirst;

namespace Hostal_El_Eden.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string RoomNumber { get; set; }
        
        [Required]
        public int Beds { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public float Price { get; set; }
        
        public virtual Category Category { get; set; }


        public Room()
        {
        }

        public Room(string rn, int b, string d, int f, int cId)
        {
            this.RoomNumber = rn;
            this.Beds = b;
            this.Description = d;
            this.Floor = f;
            this.CategoryId = cId;
        }
    }

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageURL { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int Beds{ get; set; }

        public Category() { }

        public Category(string n, string i, float p, int b)
        {
            this.Name = n;
            this.ImageURL = i;
            this.Price = p;
            this.Beds = b;
        }
    }

    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public DateTime ResCheckInDate { get; set; }
        public DateTime ResCheckOutDate { get; set; }
        public int NumOfGuests { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }

        public virtual Guest Guest { get; set; }
        public virtual Room Room { get; set; }
    }

    public class Guest
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class HotelContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Guest> Guests { get; set; }

    }

    public class HotelDBInitializer : IDatabaseInitializer<HotelContext>
    {

        public void InitializeDatabase(HotelContext context)
        {
            if (!context.Database.Exists())
                context.Database.Create();

            DontDropDbJustCreateTablesIfModelChanged<HotelContext> dontDropDb = new DontDropDbJustCreateTablesIfModelChanged<HotelContext>();
            dontDropDb.InitializeDatabase(context);


            List<Category> initialCategories = new List<Category>();
            initialCategories.Add(new Category("Sencillo", "gallery-1.jpg", 5.50f, 1));
            initialCategories.Add(new Category("Doble", "gallery-3.jpg", 8.30f, 2));
            initialCategories.Add(new Category("Triple", "gallery-4.jpg", 11.5f, 3));


            foreach (Category cat in initialCategories)
            {
                context.Categories.Add(cat);
                context.SaveChanges();
            }

            

            List<Room> initialRooms = new List<Room>();
                initialRooms.Add(new Room("Cuarto 1", 1, "Primero", 1, ((Category)(context.Categories.Single(c => c.Name=="Sencillo"))).Id));
                initialRooms.Add(new Room("Cuarto 2", 1, "Segundo", 1, ((Category)(context.Categories.Single(c => c.Name == "Sencillo"))).Id));
                initialRooms.Add(new Room("Cuarto 3", 2, "Tercero", 1, ((Category)(context.Categories.Single(c => c.Name == "Doble"))).Id));
                initialRooms.Add(new Room("Cuarto 4", 2, "Cuarto", 1, ((Category)(context.Categories.Single(c => c.Name == "Doble"))).Id));
                initialRooms.Add(new Room("Cuarto 11", 3, "Quinto", 2, ((Category)(context.Categories.Single(c => c.Name == "Triple"))).Id));
                initialRooms.Add(new Room("Cuarto 12", 3, "Sexto", 2, ((Category)(context.Categories.Single(c => c.Name == "Triple"))).Id));
                initialRooms.Add(new Room("Cuarto 13", 1, "Septimo", 2, ((Category)(context.Categories.Single(c => c.Name == "Sencillo"))).Id));

            foreach (Room room in initialRooms)
            {
                context.Rooms.Add(room);
                context.SaveChanges();
            }
        }
    }
}