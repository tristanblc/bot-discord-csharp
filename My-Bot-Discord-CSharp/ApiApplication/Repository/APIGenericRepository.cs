using ApiApplication.Model;
using ApiApplication.Repository.Interface;
using AutoMapper;
using BusClassLibrary;

namespace ApiApplication.Repository
{
    public class APIGenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {


        public IMapper mapper { get; set; }

        private readonly MyContext myContext;

        private List<T> myList { get; set; } = new List<T>();

        public APIGenericRepository(MyContext _myContext)
        {
            myContext = _myContext;
        }


        public void Add(T entity)
        {
           
            myContext.Set<T>().Add(entity);

            myContext.SaveChanges();

        }

        public bool Delete(T entity)
        {
            myContext.Set<T>().Remove(entity);
            if (myContext.SaveChanges() == 1)
            {
                return myContext.SaveChanges() == 1;
            }
            return myContext.SaveChanges() == 0;
        }

        public T? Get(Guid id)
        {
            return myContext.Set<T>().Where(element => element.Id == id).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return myContext.Set<T>().ToList();
        }

        public void Update(T entity)
        {
            myContext.Set<T>().Update(entity);
        }
    }
}
