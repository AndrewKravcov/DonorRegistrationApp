using DonorRegistrationApp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DonorRegistrationApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementsController : ControllerBase
    {
        private readonly ElementContext _elementContext;
        private ILogger _logger;
        public ElementsController(ILogger<ElementsController> logger, ElementContext elementContext)
        {
            _elementContext = elementContext;
            _logger = logger;
        }

        // GET: api/<ElementsController>
        [HttpGet]
        [HttpGet("{searchText}")]
        public List<Dictionary<string, JsonElement>> Get(string searchText="")
        {
            var ft = _elementContext.Elements;
            List<Dictionary<string, JsonElement>> elements = new();
            foreach (var t in ft)
            {
                var element = t.Convert();
                if (searchText != "")
                {
                    foreach (var searchParameter in searchText.Split(" "))
                    {
                        if (element.Values.Where(x => ConvertToString(x).Contains(searchParameter)).Any())
                            elements.Add(element);
                    }
                }
                else
                    elements.Add(element);
            }
            return elements;
        }
        private string ConvertToString(JsonElement jsonElement)
        {
            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Number: return jsonElement.GetInt32().ToString();
                case JsonValueKind.Array: return string.Join(" ", jsonElement.Deserialize<List<string>>());
                default: return jsonElement.GetString();
            }
        }

        // GET api/<ElementsController>/getitem/5
        [HttpGet("getitem/{id:int}")]
        public async Task<Dictionary<string, JsonElement>> Get(long id)
        {
            Element element = await _elementContext.Elements.FindAsync(id);
            return element.Convert();
        }

        // POST api/<ElementsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dictionary<string, JsonElement> value)
        {
            await _elementContext.Elements.AddAsync(new Element { elementProperties = value });
            await _elementContext.SaveChangesAsync();
            return CreatedAtAction("add item", value);
        }

        // PUT api/<ElementsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Dictionary<string, JsonElement> value)
        {
            Element element = await _elementContext.Elements.FindAsync(id);
            element.elementProperties = value;
            _elementContext.Elements.Update(element);
            await _elementContext.SaveChangesAsync();
            return CreatedAtAction("edit item", value);
        }

        // DELETE api/<ElementsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            Element element = await _elementContext.Elements.FindAsync(id);
            _elementContext.Elements.Remove(element);
            await _elementContext.SaveChangesAsync();
            return CreatedAtAction("delete item", id);
        }
    }
}
