using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;


namespace topNamesGiven.Controllers;

[ApiController]
[Route("[controller]")]
public class NameGivenController : ControllerBase
{
    public NameGivenController()
    {
    }

    [HttpGet("")]
    public string GetTopThreeNamesPerYear([FromQuery] string name = "", [FromQuery] string sex ="")
    {
        if (!IsValidName(name))
        {
            return "Please provide a regular name format.";
        }
        if (sex != "M" && sex != "F" && sex != "")
        {
            return "Sex is either M or F";
        }
        return name+sex;
    }

        private bool IsValidName(string name)
    {
        string nameRegex = @"^[A-Za-z]{1,5}$";
        Regex regex = new Regex(nameRegex);

        return regex.IsMatch(name);
    }
}
