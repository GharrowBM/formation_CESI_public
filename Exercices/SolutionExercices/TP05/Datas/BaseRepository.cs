namespace TP05.Datas
{
    public abstract class BaseRepository
    {
        protected ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
