﻿using System;
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
        if (floor < 1 || floor > 10) // Assuming 10 floors
        {
            throw new InvalidFloorException("*** Invalid floor selection. Please choose a valid floor. ***");
        }

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
        Console.WriteLine("Elevator Status:");
        Console.WriteLine("ID | Floor | Direction | Status     | Passengers");
        Console.WriteLine("-----------------------------------------------");
        
        
        foreach (var elevator in building.Elevators)
        {
            Console.WriteLine($"{elevator.Id,2} | {elevator.CurrentFloor,5} | {elevator.Direction,-9} | {elevator.Status,-10} | {elevator.PassengerCount,10}");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public void HandleRequest(PassengerRequest request)
    {
        try
        {
            if (request.Floor < 1 || request.Floor > building.Floors.Count)
            {
                throw new InvalidFloorException("** Invalid floor selection. Please choose a valid floor.");
            }

            var nearestElevator = FindNearestElevator(request.Floor);

            if (nearestElevator == null)
            {
                throw new InvalidOperationException("** No available elevators to handle the request.");
            }
            nearestElevator.AddRequest(request);
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

    public void LogException(Exception ex)
    {
        Console.WriteLine($"[{DateTime.Now}] Error: {ex.Message}");
    }
}
public class InvalidFloorException : Exception
{
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
