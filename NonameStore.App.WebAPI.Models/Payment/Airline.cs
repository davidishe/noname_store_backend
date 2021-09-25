using System.Collections.Generic;

namespace NonameStore.App.WebAPI.Models.Payment
{
  public class Airline
  {
    public string BookingReference { get; set; }
    public string TicketNumber { get; set; }
    public List<Passenger> Passengers { get; set; } = new List<Passenger>();
    public List<Leg> Legs { get; set; } = new List<Leg>();
  }
}