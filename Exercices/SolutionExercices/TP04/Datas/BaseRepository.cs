namespace TP04.Datas
{
    public abstract class BaseRepository
    {
        protected AppplicationDbContext _context;

        public BaseRepository(AppplicationDbContext context)
        {
            _context = context;
        }
    }
}
