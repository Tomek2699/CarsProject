using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.DataBase;

namespace WebApplication1.Models
{
    public class CarRepository : ICarRepository
    {
        private readonly CarDb carDb;

        public CarRepository(CarDb carDb)
        {
            this.carDb = carDb;
        }

        public CarModel AddCar(CarModel car)
        {
            var item = carDb.Cars.Add(car).Entity;
            carDb.SaveChanges();
            return item;
        }

        public IList<CarModel> FindAllCars()
        {
            return carDb.Cars.ToList();
        }
    }
}
