namespace BlogProject.PresentationLayer.Areas.Admin.Models
{
    public class WeatherResponse
    {
        public MainInfo main { get; set; }
    }

    public class MainInfo
    {
        public double temp { get; set; }
    }
}
