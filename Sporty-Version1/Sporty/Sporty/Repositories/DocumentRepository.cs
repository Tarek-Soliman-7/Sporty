using Sporty.Data;
using Sporty.Models;

namespace Sporty.Repositories
{
    public class DocumentRepository
    {
        private AppDbContext _context;

        public DocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddDocument(Document document)
        {
          await  _context.Documents.AddAsync(document);
            
        }

        public async Task save()
        {
           await  _context.SaveChangesAsync();

        }
    }
}
