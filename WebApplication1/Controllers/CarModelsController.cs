using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.DataBase;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CarModelsController : Controller
    {
        private CarDb _context;

        public CarModelsController(CarDb context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var cars = _context.Cars.Select(i => new {
                i.CarId,
                i.Mark,
                i.Model,
                i.Color,
                i.FuelType,
                i.Mileage
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "CarId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(cars, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new CarModel();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Cars.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.CarId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Cars.FirstOrDefaultAsync(item => item.CarId == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Cars.FirstOrDefaultAsync(item => item.CarId == key);

            _context.Cars.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(CarModel model, IDictionary values) {
            string CAR_ID = nameof(CarModel.CarId);
            string MARK = nameof(CarModel.Mark);
            string MODEL = nameof(CarModel.Model);
            string COLOR = nameof(CarModel.Color);
            string FUEL_TYPE = nameof(CarModel.FuelType);
            string MILEAGE = nameof(CarModel.Mileage);

            if(values.Contains(CAR_ID)) {
                model.CarId = Convert.ToInt32(values[CAR_ID]);
            }

            if(values.Contains(MARK)) {
                model.Mark = Convert.ToString(values[MARK]);
            }

            if(values.Contains(MODEL)) {
                model.Model = Convert.ToString(values[MODEL]);
            }

            if(values.Contains(COLOR)) {
                model.Color = Convert.ToString(values[COLOR]);
            }

            if(values.Contains(FUEL_TYPE)) {
                model.FuelType = Convert.ToString(values[FUEL_TYPE]);
            }

            if(values.Contains(MILEAGE)) {
                model.Mileage = Convert.ToInt32(values[MILEAGE]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}