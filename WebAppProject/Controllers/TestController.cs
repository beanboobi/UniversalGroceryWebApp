using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace test.Controllers;

public class TestController : Controller
{
    // 
    // GET: /HelloWorld/
    public string Index()
    {
        return "This is my default action...";
    }
    // 
    // GET: /HelloWorld/Welcome/ 
    public string Welcome()
    {
        return "This is the Welcome action method...";
    }

    //Testing if the routing to the product screen is correct
    public string Product()
    {
        return "Product Screen Test...";
    }
}