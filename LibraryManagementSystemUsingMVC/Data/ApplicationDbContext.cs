using LibraryManagementSystemUsingMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemUsingMVC.Data
{
    //To add Connection String Between Our project and database
    //steps are 1. create class file and which inherit the dbcontext which a class file of entity framework that provide the functionallity needed to connect your project to database
    //step 2. Add Configration using constructor and pass Dbcontextoptions for adding configration this will help you in to connect or add connection string
    //step 3. Next step is to register you applicationdbcontext class and it is done in program.cs file
    //step 4. once you are done with registration using sqlserver then you have to define connection string as well and this is also done inside the program.cs file
    //step 5. once you are done with above then run a command on package manage controller "update-database" this with update your database
    //now check if you have success fully create a database in your sql server or not.
    public class ApplicationDbContext : DbContext
    {
        //step 2 given below.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //Now if you are done with the steps that are above mention next step are begin that is creating table for your data into database.
        //step 1. To Create table in your database using entity framework you need to use Dbset<> property or method of entity framework that help you to create table into you database.
        //step 2. Once you use Dbset<> it require genric value that is class file that have the data which table you want create for example : here class file is library and the data for table is the dataMember of class library 
        //step 3. Now hit migration command to add migration that will generate code required to create table with using sqlquery, reason : this migration is have all the query required for creating table you can simply go and view this migration and get idea what is it donig this command present in entity framework tools
        //Note* Before hit add-migration command you have to define a data member as primary key in the class file which you want to create table 
       //Once you hit add-migration command you will see a new folder with name of migration and in this you will have migration file which have code written by enitity framework for creating table and you don't have to alter the file.
       //step 4. Hit update-database command and will apply the changes to your database,

        public DbSet<Book> BookData { get; set; }

        //Now once you done with this step you want to perfome CRUD operation and to do that you have create controller for the section or file or view where you want to perform the CRUD operation
        //So go to the Contoller and create a contoller which having action with CRUD operation and view after the operations
        //Here in this project we want to create controller for two views first for "library" and second for "add book"
        //so go to contoller and create a controller and remember file name should be similer as folder name in views and action method should be similer as the files in the folder and Controller key word attacted to it for example: HomeController , here "Home" is the folder name in views and "Controller" is the required keyword .

        //once you are done with controller thing and now you want to add some data to your table in your database you can do in your database but we have to add data form user or some default data.
        //To achieve this entity framework provide functionality that are in dbcontext that help us to achieve this task
        //step 1. First step is to override the OnModelCreating function that are present in dbcontext class
        // This function is having a "ModelBuilder" and this helps us to seed or add data into our database 
        // step 2. To seed data in your table use this modelBuilder instance of ModelBuilder.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Below the Syntax is written which is required to seed data.
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    BookTitle = "The Great Gatsby",
                    Author = "F.Scott Fiztgerald",
                    Genre = "Fiction",
                    Year = 1925,
                    Status = true
                },
                 new Book
                 {
                     Id = 2,
                     BookTitle = "1984",
                     Author = "Gorge Overwall",
                     Genre = "Science Fiction",
                     Year = 1950,
                     Status = false
                 },
                  new Book
                  {
                      Id = 3,
                      BookTitle = "To Kill a Mockingbird",
                      Author = "Harper Lee",
                      Genre = "Fiction",
                      Year = 1960,
                      Status = true
                  }
                );
        }
    }
}
