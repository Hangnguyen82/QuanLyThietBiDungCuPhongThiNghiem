using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabManagement.Data;
using LabManagement.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuPhanBoTBController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PhieuPhanBoTBController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
