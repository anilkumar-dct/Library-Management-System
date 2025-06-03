using LibraryManagementSystemUsingMVC.Data;
using LibraryManagementSystemUsingMVC.Models;
using LibraryManagementSystemUsingMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

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
       
        private readonly IBookRepo _bookRepo;// Using IBookRepo interface for better abstraction

        // Constructor to inject the database context
        public BookController(IBookRepo bookRepo)
        {
             // Assign the injected context to the private field
            _bookRepo = bookRepo;
        }

        // Method to fetch and display all books
        public IActionResult BookSection()
        {
            //below thing is used to read prams and it is pass to the same view where you are added searching option.
         //   string searchTerm = HttpContext.Request.Query["search"];

            //step 2. THE Implement of step 2.
            var books = _bookRepo.GetAllAsync().Result; // Fetch all books from the database using the repository

            if (!books.Any())
            {
                //while using ModelState.AddModelError we need to pass the view of the  first "BookSection" from where the error is coming and the error message.after ",".
                ModelState.AddModelError("BookSection", $"No Result is Found");
            }

            // Retrieve all books from the database
            return View(books); // Pass the book list to the view
        }

        //Search Query. 
        //public IActionResult SearchBook(string? searchQuery)
        //{
        //    // Get all books from the database
              
        //    var books = _db.BookData.AsQueryable();
          
        //    // If search query is provided, filter books
        //    if (!string.IsNullOrEmpty(searchQuery))
        //    {
        //        searchQuery = searchQuery.ToLower(); // Convert query to lowercase for case-insensitive search
        //        books = books.Where(b => b.BookTitle.ToLower().Contains(searchQuery)
        //                              || b.Author.ToLower().Contains(searchQuery)
        //                              || b.Genre.ToLower().Contains(searchQuery));
        //    }
            
        //    //if (string.IsNullOrWhiteSpace(searchQuery)) return NotFound();

        //    return View("BookSection", books.ToList()); // Pass filtered books to the view
        //}

        //Controller for Live Search for searchSection
        //[HttpGet]
        //public JsonResult LiveSearch(string searchQuery)
        //{
        //    var books = _db.BookData.AsQueryable();

        //    if (!string.IsNullOrEmpty(searchQuery))
        //    {
        //        books = books.Where(b => b.BookTitle.Contains(searchQuery)
        //                              || b.Author.Contains(searchQuery)
        //                              || b.Genre.Contains(searchQuery));
        //    }

        //    return Json(books.ToList());  // Return JSON response
        //}

        //For viewing purpose we created this method 
        public IActionResult AddNewBook()
        {
            return View();
        }
        //THIS post method will hit when you enter the data in the provided fields and then the form method will call this method add apply the changes
        [HttpPost]
        public async Task<IActionResult> AddNewBook(Book book)
        {
            
            if (ModelState.IsValid)
            {
                // Check if the book title already exists
                bool bookExists = _bookRepo.BookExists(book.BookTitle);

                if (bookExists)
                {
                    ModelState.AddModelError("BookTitle", "A book with this title already exists.");
                    return View(book); // Return the view with validation error
                }

                 await _bookRepo.AddAsync(book); // Add the book to the database using the repository
             
                return RedirectToAction("BookSection");
            }
            return View(book);
        }
        //Creating Edit View
        public async Task<IActionResult> EditBook(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            //this below syntax help us to fetch data that is being select or reterived
            Book? book =await _bookRepo.GetByIdAsync(b => b.Id == id); // Using the repository to get the book by ID
            if (book == null) { return NotFound(); }
            return View(book);
        }
        [HttpPost]
        public async Task<IActionResult> EditBook(Book book)
        {
            await _bookRepo.UpdateAsync(book); // Update the book in the database using the repositor
            return RedirectToAction("BookSection");
        }
        //Delete method 
        public async Task<IActionResult> DeleteBook(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            //this below syntax help us to fetch data that is being select or reterived
            Book? book = await _bookRepo.GetByIdAsync(u => u.Id == id); // Using the repository to get the book by ID
            if (book == null) { return NotFound(); }
            return View(book);
        }
        //Here don't concerne about how parameter "(Book book)" is working it is done by the connection string that you add into program.cs file and entity framework
        [HttpPost]
        public async Task<IActionResult> DeleteBook(Book book)
        {
            await _bookRepo.DeleteAsync(book); // Delete the book from the database using the repository
            return RedirectToAction("BookSection");
        }
        //Genre ActionMethod
        public IActionResult Genre(string? genre)
        {
            var filteredBooks = _bookRepo.GetBooksByGenre(genre);


          
            if (!filteredBooks.Any()) // Check if no books match the genre
            {
                ModelState.AddModelError("Genre", $"No books found in the '{genre}' genre.");
              // Return 404 if no match
            }
            ViewBag.SelectedGenre = genre; // Store selected genre for the dropdown
            return View("BookSection", filteredBooks); // Return filtered books
        }

        #region API CALLS
        [HttpGet]
        public IActionResult getall()
        {
            var _db = _bookRepo.GetAllAsync().Result; // Fetch all books from the database using the repository
            return Json(_db);
        }
        #endregion
    }
}
