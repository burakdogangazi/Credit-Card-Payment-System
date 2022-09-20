using DataAccess.Basics;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace KrediKartiOdeme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericBaseController<T, TDal> : ControllerBase 
        where TDal: IEfRepository<T>, new()
        where T : DbEntity, new()
    {
        protected TDal _dal;
        public GenericBaseController()
        {
            _dal = new TDal();
        }

        // GET: api/<TerminalConfigurationController>
        [HttpGet]
        public List<T> Get()
        {
            return _dal.Get(x => x.IsDeleted == false);
        }

        // GET api/<TerminalConfigurationController>/5
        [HttpGet("{id}")]
        public T Get(int id)
        {
            return _dal.Get(id);

            //using(var c = new EfTerminalConfigurationDAL())
            //{
            //    if(id.HasValue)
            //    {
            //        return 
            //    }
            //    else
            //    {

            //    }
            //}
        }

        // POST api/<TerminalConfigurationController>
        [HttpPost]
        public T Post([FromBody] T configuration)
        {
            return _dal.Create(configuration);
        }

        // PUT api/<TerminalConfigurationController>/5
        [HttpPut("{id}")]
        public T Put(int id, [FromBody] T configuration)
        {
            return _dal.Update(configuration);
        }

        // DELETE api/<TerminalConfigurationController>/5
        [HttpDelete("{id}")]
        public T Delete(int id)
        {
            return _dal.Delete(id);
        }
    }
}
