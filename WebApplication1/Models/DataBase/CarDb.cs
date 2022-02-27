using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.DataBase
{
    public class CarDb : DbContext
    {
        public CarDb(DbContextOptions<CarDb> options) : base(options) { }

        public DbSet<CarModel> Cars { get; set; }
    }
}
