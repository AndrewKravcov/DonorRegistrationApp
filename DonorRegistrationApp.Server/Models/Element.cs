using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace DonorRegistrationApp.Server.Models
{
    public class Element
    {
        [Key]
        public long Id { get; set; }
        public DateTime dateCreated { get; set; } = DateTime.UtcNow;
        public Dictionary<string, JsonElement> elementProperties { get; set; }
        public Dictionary<string, JsonElement> Convert()
        {
            elementProperties["id"] = JsonSerializer.SerializeToElement(Id);
            elementProperties["dateCreated"] = JsonSerializer.SerializeToElement(dateCreated);
            return elementProperties;
        }
    }
}
