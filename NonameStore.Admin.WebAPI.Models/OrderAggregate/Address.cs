using Microsoft.EntityFrameworkCore;

namespace NonameStore.Admin.WebAPI.Models.Models
{

  [Owned]
  public class Address
  {
    public Address()
    {
    }

    public Address(string firstName, string lastName, string street, string city, string house, string zipCode)
    {
      FirstName = firstName;
      LastName = lastName;
      Street = street;
      City = city;
      House = house;
      ZipCode = zipCode;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string House { get; set; }
    public string ZipCode { get; set; }
  }
}