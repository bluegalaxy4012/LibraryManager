# Library Management System

## Prerequisites
- Install .NET 8 SDK: https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- Git to clone the repo

## Setup Instructions
1. Clone the repo:
   ```bash
   git clone https://github.com/bluegalaxy4012/LibraryManager.git
   ```
2. Go to the project folder:
   ```bash
   cd LibraryManager
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```

## Running the Application
1. Run the app:
   ```bash
   dotnet run
   ```
2. Use the console menu:
   - 1: Add a book (enter title, author, copies)
   - 2: Delete a book (enter ID)
   - 3: Update a book (enter ID, new details)
   - 4: List all books
   - 5: Find book by ID
   - 6: Search books (by title or author)
   - 7: Borrow a book (enter book ID, user ID)
   - 8: Return a book (enter transaction ID)
   - 9: View overdue books
   - 10: View active transactions for a book
   - 11: Exit

### Alternative running option
- For a simpler experience, you can use the included Windows executable, LibraryManager.exe, located in the windows-x64release folder within the cloned repository. If the app doesn’t start, you may need to install the Microsoft Visual C++ Redistributable.

## Innovative Feature: Overdue Book Tracking
- **What it does**: Shows books that are borrowed but not returned after 30 days.
- **How it works**: Each borrow creates a transaction with a due date (30 days from borrow date). Menu option 9 lists transactions where the due date has passed and the book isn’t returned.



## Note
- Database (`library.db`) is created in the project folder and keeps data between runs. If `library.db` has issues or you want to delete your data, delete the file and rerun to recreate.


