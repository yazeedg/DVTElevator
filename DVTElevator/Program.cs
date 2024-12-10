using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public enum ElevatorStatus { Moving, Stationary }
public enum Direction { Up, Down, None }

public class Elevator
{
    public int Id { get; set; }
    public int CurrentFloor { get; set; }
    public Direction Direction { get; set; }
    public ElevatorStatus Status { get; set; }
    public int PassengerCount { get; set; }
    public const int MaxPassengers = 10;
   // private Queue<PassengerRequest> requests = new Queue<PassengerRequest>();
    public Queue<PassengerRequest> requests { get; private set; } = new Queue<PassengerRequest>();

    public void AddRequest(PassengerRequest request)
    {
        if (PassengerCount + request.PassengerCount <= MaxPassengers)
        {
            requests.Enqueue(request);
        }
        else
        {
            // Handle overflow if number of passengers selected are more than the maximum for the elevator
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"You requested for {request.PassengerCount} passengers. The elevator can hold a maximum of {MaxPassengers} passengers. Please select again.");
            Console.ResetColor();
            
        }
    }

    public async Task MoveAsync()
    {
        while (requests.Any())
        {
            var request = requests.Dequeue();
            await MoveToFloorAsync(request.Floor);
            PassengerCount += request.PassengerCount;
        }
    }

    public async Task MoveToFloorAsync(int floor)
    {
        // Simulate elevator movement
        await Task.Delay(Math.Abs(CurrentFloor - floor) * 1000);
        CurrentFloor = floor;
        Status = ElevatorStatus.Stationary;
    }
}

public class PassengerRequest
{
    public int Floor { get; set; }
    public int PassengerCount { get; set; }

    public PassengerRequest(int floor, int passengerCount)
    {
        Floor = floor;
        PassengerCount = passengerCount;
    }
}

public class Building
{
    public List<Elevator> Elevators { get; set; }
    public List<int> Floors { get; set; }

    public Building(int numberOfFloors, int numberOfElevators)
    {
        Floors = Enumerable.Range(1, numberOfFloors).ToList();
        Elevators = new List<Elevator>(); //(numberOfElevators);
        for (int i = 0; i < numberOfElevators; i++)
        {
            Elevators.Add(new Elevator { Id = i + 1, CurrentFloor = 1, Direction = Direction.None, Status = ElevatorStatus.Stationary });
        }
    }
}

public class ElevatorController
{
    private Building building;

    public ElevatorController(Building building)
    {
        this.building = building;
    }

    public void DisplayElevatorStatus()
    {
        Console.WriteLine("Elevator Status:");
        Console.WriteLine("ID | Floor | Direction | Status     | Passengers");
        Console.WriteLine("-----------------------------------------------");
        foreach (var elevator in building.Elevators)
        {
            Console.WriteLine($"{elevator.Id,2} | {elevator.CurrentFloor,5} | {elevator.Direction,-9} | {elevator.Status,-10} | {elevator.PassengerCount,10}");
        }
    }

    public void HandleRequest(PassengerRequest request)
    {
        var nearestElevator = FindNearestElevator(request.Floor);
        if (nearestElevator != null)
        {
            nearestElevator.AddRequest(request);
            Console.WriteLine($"Elevator {nearestElevator.Id} is on its way to floor {request.Floor}.");
        }
        else
        {
            Console.WriteLine("No available elevators to handle the request.");
        }
    }

    public Elevator FindNearestElevator(int floor)
    {
        //Find the nearest elevator to fulfil the user request
        return building.Elevators.OrderBy(e => Math.Abs(e.CurrentFloor - floor)).FirstOrDefault();
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Elevator Control System");
            Console.WriteLine("1. View Elevator Status");
            Console.WriteLine("2. Call Elevator");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    DisplayElevatorStatus();
                    break;
                case "2":
                    Console.Write("Enter floor number: ");
                    if (int.TryParse(Console.ReadLine(), out int floor))
                    {
                        Console.Write("Enter number of passengers: ");
                        if (int.TryParse(Console.ReadLine(), out int passengers))
                        {
                            HandleRequest(new PassengerRequest(floor, passengers));
                        }
                        else
                        {
                            Console.WriteLine("Invalid number of passengers.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid floor number.");
                    }
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            await Task.Delay(1000);
        }
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        var building = new Building(10, 5); // 10 floors, 5 elevators
        var controller = new ElevatorController(building);

        await controller.RunAsync();
    }
}
