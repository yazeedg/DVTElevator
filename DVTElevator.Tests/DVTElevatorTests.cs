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
    }

    public class ElevatorControllerTests
    {
        [Fact]
        public void Should_Find_Nearest_Elevator()
        {
            var building = new Building(10, 3);
            var controller = new ElevatorController(building);
            var request = new PassengerRequest(5, 2);

            var nearestElevator = controller.FindNearestElevator(request.Floor);
            Assert.NotNull(nearestElevator);
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
            Assert.Contains(request, nearestElevator.Requests);
        }
    }
}