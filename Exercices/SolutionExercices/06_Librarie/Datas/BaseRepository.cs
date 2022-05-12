namespace _06_Librarie.Datas
{
    public abstract class BaseRepository
    {
        protected readonly ApplicationDbContext context;

        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
    }
}
