using _net_angular_demo.Domain.Entities;

namespace _net_angular_demo.Data;


public class Entities{

    public IList<Passenger> passengers = new List<Passenger>();

    static Random rand = new Random();
    public Flight[] flights = new Flight[]{
            new (Guid.NewGuid(),
                "American Airlines",
                rand.Next(90,5000).ToString(),
                new TimePlace("Los Angeles", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Istanbul", DateTime.Now.AddHours(rand.Next(4,10))),
                2
            ),
            new (Guid.NewGuid(),
                "Deutsche BA",
                rand.Next(90,5000).ToString(),
                new TimePlace("Munchen", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Schiphol", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "British Airways",
                rand.Next(90,5000).ToString(),
                new TimePlace("London", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Rome", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "Basiq Air",
                rand.Next(90,5000).ToString(),
                new TimePlace("Amsterdam", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Glasgow", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "BB Heliag",
                rand.Next(90,5000).ToString(),
                new TimePlace("Zurich", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Baku", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "Adria Airways",
                rand.Next(90,5000).ToString(),
                new TimePlace("Ljubljana", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Warsaw", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
    };

}