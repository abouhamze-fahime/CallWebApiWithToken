using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
