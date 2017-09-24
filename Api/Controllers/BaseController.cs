using System;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected DropdownViewModel ToDropDownViewModel<T>(T input)
        {
            return new DropdownViewModel
            {
                Id = Convert.ToInt32(input.GetType().GetProperty("Id").GetValue(input)),
                Name = Convert.ToString(input.GetType().GetProperty("Name").GetValue(input)),
                Description = Convert.ToString(input.GetType().GetProperty("Description").GetValue(input)),
            };
        }
    }
}