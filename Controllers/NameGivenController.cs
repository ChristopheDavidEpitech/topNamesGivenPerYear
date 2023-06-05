using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using System.Text.RegularExpressions;

namespace topNamesGivenPerYear.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class NameGivenController : ControllerBase
{

    public NameGivenController(IConfiguration configuration)
    {
    }

    [HttpGet("")]
    public IEnumerable<dynamic> GetTopThreeNamesPerYear(IConfiguration configuration, [FromQuery] string name = "", [FromQuery] string sex ="")
    {
        if (!IsValidName(name))
        {
            return new List<ErrorMessage> { new ErrorMessage { Message = "Please provide a regular name format." } };            
        }
        if (sex != "M" && sex != "F" && sex != "")
        {
            return new List<ErrorMessage> { new ErrorMessage { Message = "Sex is either M or F" } };
        }
        
        IDbConnection conn = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        
        string cmd = @"
            CALL sp_ranked_names(@name, @sex);
            SELECT annee, prenoms, nombre, rank
            FROM ranked_names
            WHERE rank <= 3
            ORDER BY annee, nombre DESC;
        ";
        var parameters = new { name = name, sex = sex };
        IEnumerable<dynamic> result = conn.Query(cmd, parameters);
        
        return result;   
    }

        private bool IsValidName(string name)
    {
        string nameRegex = @"^[A-Za-z]{0,5}$";
        Regex regex = new Regex(nameRegex);

        return regex.IsMatch(name);
    }
    public class ErrorMessage
    {
        public string? Message { get; set; }
    }
}
