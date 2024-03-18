using Microsoft.EntityFrameworkCore;
using PrivateLibrary.Data;
using Quartz;

namespace PrivateLibrary.Jobs
{
    public class ReturnBookJob : IJob
    {
        private readonly ApplicationDbContext _context;

        public ReturnBookJob(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var takenBooks = await _context.TakenBooks
                .Include(a => a.Book)
                .Where(a => a.Book.IsTaken)
                .ToListAsync();

            foreach (var takenBook in takenBooks) 
            {
                if(DateTime.Now > takenBook.DateOfReturn)
                {
                    var book = await _context.Books.FirstOrDefaultAsync(a => a.Id == takenBook.BookId);

                    if(book != null)
                    {
                        book.IsTaken = false;
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
