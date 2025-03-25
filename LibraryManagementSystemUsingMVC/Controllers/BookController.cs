using LibraryManagementSystemUsingMVC.Data;
using LibraryManagementSystemUsingMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemUsingMVC.Controllers
{
    //This Controller folder have controller and this controller have action method and views related to the action method 
   //Once you create a controller the class file created own its own with the same name of your file and inherit controller which is in built class that provide method required for the view and operation you want to perform
   //And when you are done with above steps you need to create views also for the action method to see your controller in action.
   //here our controller comes with default index view and we also created a index view as for example further we are going to create required view with suitable naming 
    public class BookController : Controller
    {
        //Default View
        public IActionResult Index()
        {
            return View();
        }
        //Retreiving data form the database with controller
        //step 1. proivde implementation and that is comming from program.cs file
        //this below code is help us to read the data that are present into our database and below constructor provide the data to the private readonly variable
        //step 2. Is to pass the database context hold by private variable to new variable as list to retreive the data .
        //for example : we are reteriving the database context and passing to the "var" datatype variable "books" as list in BookSection action
        
        //chatgpt explanation
        // Field to store the database context
        private readonly ApplicationDbContext _db;

        // Constructor to inject the database context
        public BookController(ApplicationDbContext db)
        {
            _db = db; // Assign the injected context to the private field
        }

        // Method to fetch and display all books
        public IActionResult BookSection()
        {
            //step 2. THE Implement of step 2.
            var books = _db.BookData.ToList(); // Retrieve all books from the database
            return View(books); // Pass the book list to the view
        }
        //Search Query. 
        public IActionResult SearchBook(string searchQuery)
        {
            // Get all books from the database
            var books = _db.BookData.AsQueryable();

            // If search query is provided, filter books
            if (!string.IsNullOrEmpty(searchQuery))
            {
                books = books.Where(b => b.BookTitle.Contains(searchQuery)
                                      || b.Author.Contains(searchQuery)
                                      || b.Genre.Contains(searchQuery));
            }

            return View("BookSection",books.ToList()); // Pass filtered books to the view
        }
        //For viewing purpose we created this method 
        public IActionResult AddNewBook()
        {
            return View();
        }
        //THIS post method will hit when you enter the data in the provided fields and then the form method will call this method add apply the changes
        [HttpPost]
        public IActionResult AddNewBook(Book book)
        {
            //if (ModelState.IsValid)
            //{
            //    _db.BookData.Add(book);
            //    _db.SaveChanges();
            //    return RedirectToAction("BookSection");
            //}

            //_db.BookData.Add(book);
            //_db.SaveChanges();
            //return RedirectToAction("BookSection");
            ////return View();
            //-----------
            //by chatgpt
            if (ModelState.IsValid)
            {
                // Check if the book title already exists
                bool bookExists = _db.BookData.Any(b => b.BookTitle == book.BookTitle);

                if (bookExists)
                {
                    ModelState.AddModelError("BookTitle", "A book with this title already exists.");
                    return View(book); // Return the view with validation error
                }

                _db.BookData.Add(book);
                _db.SaveChanges();
                return RedirectToAction("BookSection");
            }
            return View(book);
        }
        //Creating Edit View
        public IActionResult EditBook(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            //this below syntax help us to fetch data that is being select or reterived
            Book? book = _db.BookData.FirstOrDefault(find => find.Id == id);
            if (book == null) { return NotFound(); }
            return View(book);
        }
    }
}
