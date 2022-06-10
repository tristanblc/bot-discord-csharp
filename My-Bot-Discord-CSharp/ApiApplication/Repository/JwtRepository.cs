
using AutoMapper;
using BotClassLibrary;

namespace ApiApplication.Repository
    {
        public class JwtRepository 
        {
            public IMapper _mapper { get; }

            private ApplicationDbContext SqlContext { get; set; }

            private List<Users> _listEntity { get; set; } = new List<Users>();

          private List<Tokens> _listTokens { get; set; } = new List<Tokens>();

        public JwtRepository(ApplicationDbContext apiContext)
            {
                SqlContext = apiContext;
            }


             public Users Add(Users entity)
            {

                SqlContext.Set<Users>().Add(entity);

                SqlContext.SaveChanges();

                return entity;
            }

        public Tokens Add(Tokens entity)
        {
            if (entity == null)
            {
                return null;
            }

            SqlContext.Set<Tokens>().Add(entity);
            SqlContext.SaveChanges();

            return entity;
        }

        public Tokens Update(Tokens entity)
        {
            if (entity == null)
            {
                return null;
            }

            SqlContext.Set<Tokens>().Update(entity);
            SqlContext.SaveChanges();

            return entity;
        }
        public Users GetByEmail(string email)
            {
                return SqlContext.Set<Users>().Where(element => element.Email == email).FirstOrDefault();
            }



    }

}
