using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalorieCounter.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CalorieCounter.Api.Controllers
{
    public class ApiControllerBase : Controller
    {
        protected readonly ICommandDispatcher CommandDispatcher;

        public ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            CommandDispatcher = commandDispatcher;
        }
    }
}