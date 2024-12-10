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
            // Handle overflow
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
        Elevators = new List<Elevator>(numberOfElevators);
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
        foreach (var elevator in building.Elevators)
        {
            Console.WriteLine($"Elevator {elevator.Id}: Floor {elevator.CurrentFloor}, Direction {elevator.Direction}, " +
                              $"Status {elevator.Status}, Passengers {elevator.PassengerCount}");
        }
    }

    public void HandleRequest(PassengerRequest request)
    {
        var nearestElevator = FindNearestElevator(request.Floor);
        nearestElevator.AddRequest(request);
    }

    public Elevator FindNearestElevator(int floor)
    {
        return building.Elevators.OrderBy(e => Math.Abs(e.CurrentFloor - floor)).First();
    }

    public async Task RunAsync()
    {
        while (true)
        {
            DisplayElevatorStatus();

            Console.WriteLine("Enter floor number to call an elevator (or 'exit' to quit):");
            var input = Console.ReadLine();
            if (input.ToLower() == "exit") break;

            if (int.TryParse(input, out int floor))
            {
                Console.WriteLine("Enter number of passengers:");
                input = Console.ReadLine();
                if (int.TryParse(input, out int passengers))
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

            await Task.Delay(1000);
        }
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        var building = new Building(10, 3); // 10 floors, 3 elevators
        var controller = new ElevatorController(building);

        await controller.RunAsync();
    }
}
