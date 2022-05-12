﻿namespace _07_eBooks.Datas
{
    public interface IRepository<T> where T : class
    {
        public T Add(T entity);
        public T GetById(int id);
        public ICollection<T> GetAll();
        public T Update(int id, T entity);
        public bool Delete(int id);
    }
}
