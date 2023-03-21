using System;

namespace HangfireIntegration
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

        public static void Update()
        {
            var x = 2 * 3;
        }

    }
}
