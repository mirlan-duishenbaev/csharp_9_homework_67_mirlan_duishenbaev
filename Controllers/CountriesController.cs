using country_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace country_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {
        private readonly CountryContext _db;
        public CountriesController(CountryContext context)
        {
            _db = context;
            if (!_db.Countries.Any())
            {
                _db.Countries.Add(new Country { Name = "Russia", Capital = "Moscow", Language = "russian" });
                _db.Countries.Add(new Country { Name = "USA", Capital = "Washington", Language = "english" });
                _db.Countries.Add(new Country { Name = "Great Britain", Capital = "London", Language = "english" });
                _db.Countries.Add(new Country { Name = "Germany", Capital = "Berlin", Language = "german" });
                _db.Countries.Add(new Country { Name = "Italy", Capital = "Milan", Language = "italian" });
                _db.Countries.Add(new Country { Name = "Spain", Capital = "Madrid", Language = "spanish" });
                _db.Countries.Add(new Country { Name = "France", Capital = "Paris", Language = "french" });
                _db.Countries.Add(new Country { Name = "Kyrgyzstan", Capital = "Bishkek", Language = "kyrgyz" });
                _db.Countries.Add(new Country { Name = "China", Capital = "Beijing", Language = "chinese" });
                _db.Countries.Add(new Country { Name = "Japan", Capital = "Tokio", Language = "japanese" });
                _db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> Get()
        {
            return await _db.Countries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> Get(int id)
        {
            Country country = await _db.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (country == null)
                return NotFound();

            return new ObjectResult(country);
        }


        [HttpGet("name/{name}")]
        public async Task<ActionResult<Country>> Get(string name)
        {
            Country country = await _db.Countries.FirstOrDefaultAsync(x => x.Name.ToLower() == name);
            if (country == null)
                return NotFound();

            return new ObjectResult(country);
        }


        [HttpPost]
        public async Task<ActionResult<Country>> Post(Country country)
        {
            if (country == null)
                return BadRequest();

            _db.Countries.Add(country);
            await _db.SaveChangesAsync();
            return Ok(country);
        }
        
        [HttpPut]
        public async Task<ActionResult<Country>> Put(Country country)
        {
            if (country == null)
                return BadRequest();

            if (!_db.Countries.Any(x => x.Id == country.Id))
                return NotFound();

            _db.Countries.Update(country);
            await _db.SaveChangesAsync();
            return Ok(country);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> Delete(int id)
        {
            Country country = _db.Countries.FirstOrDefault(x => x.Id == id);
            if (country == null)
                return NotFound();
            _db.Countries.Remove(country);
            await _db.SaveChangesAsync();
            return Ok(country);
        }
    }
}
