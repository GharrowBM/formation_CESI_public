namespace TP04.Datas
{
    public interface IRepository<T> where T : class
    {
        public T Add(T entity);
        public T GetById(int id);
        public List<T> GetAll();
        public T Update(int id, T entity);
        public bool Delete(int id);
    }
}
