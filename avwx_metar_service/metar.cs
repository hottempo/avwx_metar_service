using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace avwx_metar_service
{
    public class Metar
    {
        [JsonPropertyName("station")]
        public required string Station { get; set; }

        [JsonPropertyName("time")]
        public required Time Time { get; set; }

        [JsonPropertyName("wind_direction")]
        public required WindDirection WindDirection { get; set; }

        [JsonPropertyName("wind_speed")]
        public required WindSpeed WindSpeed { get; set; }

        [JsonPropertyName("visibility")]
        public required Visibility Visibility { get; set; }

        [JsonPropertyName("wx_codes")]
        public required List<WxCode> WxCodes { get; set; }

        [JsonPropertyName("clouds")]
        public required List<Cloud> Clouds { get; set; }

        [JsonPropertyName("temperature")]
        public required Temperature Temperature { get; set; }

        [JsonPropertyName("dewpoint")]
        public required Temperature Dewpoint { get; set; }

        [JsonPropertyName("altimeter")]
        public required Altimeter Altimeter { get; set; }

    }

    public class Time
    {
        [JsonPropertyName("dt")]
        public required DateTime Dt { get; set; }
    }

    public class WindDirection
    {
        [JsonPropertyName("value")]
        public required int Value { get; set; }
    }

    public class WindSpeed
    {
        [JsonPropertyName("value")]
        public required int Value { get; set; }
    }

    public class Visibility
    {
        [JsonPropertyName("value")]
        public required int Value { get; set; }
    }

    public class WxCode
    {
        [JsonPropertyName("repr")]
        public required string Value { get; set; }
    }

    public class Cloud
    {
        [JsonPropertyName("type")]
        public required string Type { get; set; }
        [JsonPropertyName("altitude")]
        public required int Altitude { get; set; }
    }

    public class Temperature
    {
        [JsonPropertyName("value")]
        public required int Value { get; set; }
    }

    public class Altimeter
    {
        [JsonPropertyName("value")]
        public required int Value { get; set; }

    }
}
