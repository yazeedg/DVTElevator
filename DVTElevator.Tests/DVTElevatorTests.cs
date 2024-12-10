using System.Threading.Tasks;
using Xunit;
using DVTElevator;

namespace DVTElevator.Tests
{
    public class ElevatorTests
    {
        [Fact]
        public async Task Elevator_Should_Move_To_Correct_Floor()
        {
            var elevator = new Elevator { Id = 1, CurrentFloor = 1 };
            await elevator.MoveToFloorAsync(5);
            Assert.Equal(5, elevator.CurrentFloor);
        }

        [Fact]
        public void Elevator_Should_Not_Exceed_Max_Passengers()
        {
            var elevator = new Elevator { Id = 1, PassengerCount = 8 };
            var request = new PassengerRequest(3, 3);
            elevator.AddRequest(request);
            Assert.True(elevator.PassengerCount <= Elevator.MaxPassengers);
        }

        [Fact]
        public void Elevator_Should_Handle_Passenger_Request()
        {
            var elevator = new Elevator { Id = 1, PassengerCount = 0 };
            var request = new PassengerRequest(3, 2);
            elevator.AddRequest(request);
            Assert.Contains(request, elevator.requests);
        }
    }

    public class ElevatorControllerTests
    {
        [Fact]
        public void Should_Find_Nearest_Elevator()
        {
            // Initialize the building with 10 floors and 3 elevators
            var building = new Building(10, 3);
            var controller = new ElevatorController(building);

            // Create a passenger request on the 5th floor
            var request = new PassengerRequest(5, 2);

            // Find the nearest elevator to the 5th floor
            var nearestElevator = controller.FindNearestElevator(request.Floor);

            // Assert that the nearest elevator is not null
            Assert.NotNull(nearestElevator);

            // Additional assertions to verify the nearest elevator's properties
            Assert.Equal(1, nearestElevator.CurrentFloor); // Assuming all elevators start at floor 1
        }

        [Fact]
        public void Should_Handle_Passenger_Request()
        {
            var building = new Building(10, 3);
            var controller = new ElevatorController(building);
            var request = new PassengerRequest(5, 2);

            controller.HandleRequest(request);
            var nearestElevator = controller.FindNearestElevator(request.Floor);
            Assert.Contains(request, nearestElevator.requests);
        }

        [Fact]
        public async Task Elevator_Should_Handle_Multiple_Requests()
        {
            var elevator = new Elevator { Id = 1, CurrentFloor = 1 };
            var request1 = new PassengerRequest(3, 2);
            var request2 = new PassengerRequest(5, 1);

            elevator.AddRequest(request1);
            elevator.AddRequest(request2);

            await elevator.MoveAsync();

            Assert.Equal(5, elevator.CurrentFloor);
            Assert.Equal(3, elevator.PassengerCount);
        }

        [Fact]
        public void Should_Handle_Invalid_Floor_Number()
        {
            // Here we will deal with invalid floor numbers entered 

            var building = new Building(10, 3);
            var controller = new ElevatorController(building);
            var exception = Record.Exception(() => controller.HandleRequest(new PassengerRequest(11, 2)));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }

        [Fact]
        public void Should_Handle_Invalid_Passenger_Count()
        {
            // Here we will deal with invalid passenger count entered

            var building = new Building(10, 3);
            var controller = new ElevatorController(building);
            var exception = Record.Exception(() => controller.HandleRequest(new PassengerRequest(5, -1)));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }
    }
}