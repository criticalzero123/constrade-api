using ConstradeApi.Entity;

namespace ConstradeApi.Model.MSubcription
{
    public class DbHelper
    {
        private readonly DataContext _context;

        public DbHelper(DataContext dataContext)
        {
            _context = dataContext;
        }
    }
}
