using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RSACrackstation.Models;

namespace RSACrackstation.Controllers;

public class ApiController : Controller{
    public int[] GetFactors(string inputNum){
        int[] nums = { -1, -1 };
        return nums;
    }
}