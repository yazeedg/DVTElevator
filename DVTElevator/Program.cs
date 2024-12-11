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

    //Let us set a constant for 10 passengers per elevator as the maximum.
    public const int MaxPassengers = 10;
   // private Queue<PassengerRequest> requests = new Queue<PassengerRequest>();
    public Queue<PassengerRequest> requests { get; private set; } = new Queue<PassengerRequest>();

    public void AddRequest(PassengerRequest request)
    {
        try
        {
            // Check if our requested passengers is greater than the max allowed for the elevator
            //  Alert the user if it does exceed
            if (PassengerCount + request.PassengerCount > MaxPassengers)
            {
                throw new CapacityExceededException($"You requested for {request.PassengerCount} passengers. The elevator can hold a maximum of {MaxPassengers} passengers. Please select again.");
                
            }
            requests.Enqueue(request);
        }
        catch (CapacityExceededException ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadKey(); 
        }
    }

    public async Task MoveAsync()
    {
        while (requests.Any())
        {
            var request = requests.Dequeue();
            try
            {
                await MoveToFloorAsync(request.Floor);
                PassengerCount += request.PassengerCount;
            }
            catch (InvalidFloorException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }

    public async Task MoveToFloorAsync(int floor)
    {
        // Validate if an acceptable floor is requested. we are assuming 10 floors for this app
        if (floor < 1 || floor > 10) 
        {
            throw new InvalidFloorException("*** Invalid floor selection. Please choose a valid floor. ***");
        }
        else
        {
            Console.WriteLine("Elevator for floor {0} is on its way to you.", floor);
        }
        // Simulate elevator movement
        await Task.Delay(Math.Abs(CurrentFloor - floor) * 1000);
        CurrentFloor = floor;
        Status = ElevatorStatus.Stationary;
    }
}


public class PassengerRequest
{
    //This request will be setting the floor and passenger count
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
    // This class will be instantiated for each building
    public List<Elevator> Elevators { get; set; }
    public List<int> Floors { get; set; }

    public Building(int numberOfFloors, int numberOfElevators)
    {
        Floors = Enumerable.Range(1, numberOfFloors).ToList();
        Elevators = new List<Elevator>();  
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
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Elevator Status");
        Console.WriteLine("ID | Floor | Direction | Status     | Passengers");
        Console.WriteLine("-----------------------------------------------");
        
        
        foreach (var elevator in building.Elevators)
        {
            Console.WriteLine($"{elevator.Id,2} | {elevator.CurrentFloor,5} | {elevator.Direction,-9} | {elevator.Status,-10} | {elevator.PassengerCount,10}");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.ResetColor();
    }

    public void HandleRequest(PassengerRequest request)
    {
        try
        {
            if (request.Floor < 1 || request.Floor > building.Floors.Count)
            {
                throw new InvalidFloorException("** Invalid floor selection. Please choose a valid floor.", request.Floor);
            }

            var nearestElevator = FindNearestElevator(request.Floor);

            if (nearestElevator == null)
            {
                throw new InvalidOperationException("** No available elevators to handle the request.");
            }
            nearestElevator.AddRequest(request);
            Console.WriteLine($"Elevator {nearestElevator.Id} is being dispatched to floor {request.Floor}.");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            //LogException(ex);
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
            Console.BackgroundColor = ConsoleColor.DarkGreen; 
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("DVT Elevator System");
            Console.ResetColor();
            Console.WriteLine("1. View Elevator Status");
            Console.WriteLine("2. Call Elevator");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    // List the status of all the elevators
                    DisplayElevatorStatus();
                    break;
                case "2":
                    Console.Write("Enter floor number: ");
                    if (int.TryParse(Console.ReadLine(), out int floor))
                    {
                        // Check if the floor entered is a valid value, we are assuming 10 floors for this app
                        if (floor < 1 || floor > 10)
                        {
                            // Alert the user and return to the menu
                            Console.Write("** Invalid floor number entered. Press any key to continue..");
                            Console.ReadKey();
                            break;
                        }
                        Console.Write("Enter number of passengers: ");
                        if (int.TryParse(Console.ReadLine(), out int passengers))
                        {
                            HandleRequest(new PassengerRequest(floor, passengers));
                        }
                        else
                        {
                            Console.Write("Invalid number of passengers. Press any key to continue..");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Calling an elevator...");
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

    public void LogException(Exception ex)
    {
        Console.WriteLine($"[{DateTime.Now}] Error: {ex.Message}");
    }
}
public class InvalidFloorException : Exception
{
    // Custom exception class for checking if the floor selected is valid for the building being processed.
    public int InvalidFloor { get; }
    public InvalidFloorException(string message) : base(message) { }
    public InvalidFloorException(string message, int invalidFloor) : base(message)
    {
        InvalidFloor = invalidFloor;
    }

    public InvalidFloorException(string message, Exception innerException) : base(message, innerException) { }
    public override string ToString()
    {
        return $"{base.ToString()}, Invalid Floor: {InvalidFloor}";
    }
}

public class CapacityExceededException : Exception
{
    // Custom exception class for checking if the capacity of the elevator had been exceeded

    public int CurrentCapacity { get; }
    public int MaxCapacity { get; }

    public CapacityExceededException(string message) : base(message) { }

    public CapacityExceededException(string message, int currentCapacity, int maxCapacity) : base(message)
    {
        CurrentCapacity = currentCapacity;
        MaxCapacity = maxCapacity;
    }

    public CapacityExceededException(string message, Exception innerException) : base(message, innerException) { }

    public override string ToString()
    {
        return $"{base.ToString()}, Current Capacity: {CurrentCapacity}, Max Capacity: {MaxCapacity}";
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        var building = new Building(10, 4); // 10 floors, 4 elevators
        var controller = new ElevatorController(building);

        await controller.RunAsync();
    }
}
