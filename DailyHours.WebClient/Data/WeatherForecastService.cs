using System;
using System.Linq;
using System.Threading.Tasks;

namespace DailyHours.WebClient.Data
{
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }

        public ProjectTask[] GetProjectTasks(DateTime taskTime)
        {
            return Enumerable.Range(1, 5).Select(index => GetProjectTask(taskTime)).ToArray();
        }

        private readonly Random _rng = new Random();

        public ProjectTask GetProjectTask(DateTime taskTime)
        {
            return new ProjectTask()
            {
                Name = $"Task {_rng.Next(0, 55)}",
                Hours = (double)(_rng.Next(0, 6)) / 2,
                DateTime = taskTime,
            };
        }
    }
}
