using System;

namespace MyAppBack.Models.Payment
{
  public class Leg
  {
    public string DepartureAirport { get; set; }
    public string DestinationAirport { get; set; }
    public DateTime DepartureDate { get; set; }
  }
}