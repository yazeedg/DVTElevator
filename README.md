# DVT Elevator Challenge

This solution is a console application in C# that simulates the movement of
elevators within a large building, with the aim of optimizing passenger transportation efficiently. The
application should adhere to Object-Oriented Programming (OOP) principles to ensure modularity
and maintainability.
The console application must include the following key features:

- **Real-Time Elevator Status**  
    Display the real-time status of each elevator, including its current floor, direction of movement,
whether it's in motion or stationary, and the number of passengers it is carrying.
- **Interactive Elevator Control**  
    Allow users to interact with the elevators through the console application. Users should be able
to call an elevator to a specific floor and indicate the number of passengers waiting on each floor.
- **Multiple Floors and Elevators Support**  
Design the application to accommodate buildings with multiple floors and multiple elevators.
Ensure that elevators can efficiently move between different floors.
- **Efficient Elevator Dispatching**  
Implement an algorithm that efficiently directs the nearest available elevator to respond to an
elevator request. Minimize wait times for passengers and optimize elevator usage.
- **Passenger Limit Handling**  
Consider the maximum passenger limit for each elevator. Prevent the elevator from becoming
overloaded and handle scenarios where additional elevators might be required.
- **Consideration for Different Elevator Types**  
Although the challenge focuses on passenger elevators, consider the existence of other elevator
types, such as high-speed elevators, glass elevators, and freight elevators. Plan the application's
architecture to accommodate future extension for these types.
- **Real-Time Operation**  
Ensure that the console application operates in real-time, providing immediate responses to user
interactions and accurately reflecting elevator movements and status.

Those reviewing the submission consider the following:
### GitHub Submission
- Repo is public
- .gitignore present
- Code committed using Git commands (not upload)
- Main branch builds
- Meaningful commit messages
- Informative readme present
- ✨Bonus ✨: Tags, branching, automated builds
### Comments/Documentation
- Informative readme present
- Code comments in line with MS documentation
- Code comments per generally accepted industry guidelines
### Clean Architecture
- Project/s must be structured according to clean architecture or similar. Adopting clean architecture
or similar principles in C# code promotes better software design and development practices, leading
to more maintainable, robust, and scalable applications. It also enables better alignment with
business requirements and simplifies the development process for teams.
## SOLID principles are followed
- Single Responsibility Principle (SRP)
Each class or component in the application should have a single responsibility related to elevator
movement. For example, you might have separate classes for elevator control, floor management,
and passenger handling. This ensures that each class is focused and easier to understand.
- Open/Closed Principle (OCP)
Design the elevator system in a way that allows you to extend its behaviour without modifying
existing code. For example, if you want to add a new type of elevator or implement additional
control strategies, you should be able to do so by adding new classes or methods without changing
the existing ones.
- Liskov Substitution Principle (LSP)
Ensure that any subclass or derived class you create can be used interchangeably with its parent
class. In the context of the elevator simulation, this means that any specific elevator implementation
should be able to replace the base elevator class without changing the expected behaviour of the
system.
- Interface Segregation Principle (ISP)
Design interfaces that are specific to the needs of the client code. Avoid creating large, monolithic
interfaces that require the implementation of unnecessary methods. For instance, you might have
separate interfaces for elevator control, floor events, and passenger interactions, tailored to each
client's requirements.
- Dependency Inversion Principle (DIP)
Depend on abstractions rather than concrete implementations. For instance, your elevator control
system should depend on high-level interfaces (abstractions) that define elevator behaviour, rather
than directly relying on specific elevator classes. This allows for easier swapping of different elevator
implementations.

By adhering to these SOLID principles, your elevator simulation application will have a clear and
modular structure, making it easier to understand, maintain, and extend in the future. The
separation of concerns, flexible design, and decoupling of dependencies will contribute to a more
robust and scalable elevator simulation.
Unit Tests
Unit tests play a crucial role in the elevator coding challenge, just as they do in any so􀅌ware
development project. Here are some key reasons why unit tests are important in the context of the
elevator coding challenge:
 Validation of Elevator Behaviour
Unit tests allow you to verify that the elevator's core functionality is working correctly. By
writing tests for elevator movement, floor bu􀆩on presses, door operations, and other critical
aspects, you can ensure that the elevator behaves as expected and meets the requirements.
 Regression Testing
As you make changes and improvements to the elevator code, unit tests act as a safety net to
catch any unintended side effects or regressions. Running unit tests a􀅌er each modification
helps to quickly identify issues and prevent new bugs from being introduced.
 Refactoring Support
When refactoring or optimizing the codebase, unit tests provide confidence that the
behaviour of the elevator remains intact. If all tests pass a􀅌er refactoring, it indicates that
the changes did not break any existing functionality.
 Isolation of Bugs
Unit tests help isolate bugs and pinpoint the specific areas where problems exist. This
reduces the time and effort required to debug and fix issues in the code.
 Code Design and Modularity
Writing unit tests o􀅌en requires breaking down the code into smaller, testable units. This
promotes be􀆩er code design, modularity, and separation of concerns, making the codebase
more maintainable.
 Documentation and Usage Examples
Unit tests serve as executable documentation that showcases how different parts of the
elevator system should behave. Other developers can use these tests to understand the
intended behaviour and usage of various components.
 Collaboration and Teamwork
When multiple developers are working on the elevator challenge, unit tests provide a
common language for understanding and validating each other's code. They act as a form of
communication between team members.
 Test-Driven Development (TDD)
Adopting TDD principles in the elevator coding challenge can lead to a more deliberate and
systematic approach to development. Writing tests first helps to drive the design and
implementation of the elevator system.
 Continuous Integration (CI)
Unit tests are essential for se􀆫ng up a continuous integration pipeline. Automated tests can
be executed on each code commit, ensuring that the elevator system remains functional as
new code is integrated.
 Scalability and Extensibility
As the elevator system grows and evolves, unit tests provide a foundation for adding new
features and extending functionality without introducing regressions.
In summary, unit tests are a critical aspect of the elevator coding challenge because they improve
code quality, provide a safety net for changes, enhance maintainability, and ensure the elevator
system behaves as expected. Investing time in writing comprehensive and well-structured unit tests
pays off in the long run by reducing the likelihood of bugs, improving developer productivity, and
increasing the overall reliability of the application.
Logic/Algorithm
 Use Built-in Data Structures and Collections
Utilize C# built-in collections like List<T>, Dictionary<TKey, TValue>, and Queue<T> to manage
elevator states, track floor bu􀆩on presses, and handle passenger queues efficiently.
 Avoid Unnecessary Code Duplication
Encapsulate common elevator movement and floor handling logic in separate methods to
avoid duplicating code across different parts of the application.
 Choose the Right Algorithm for the Task
For sorting floors or optimizing elevator movement, consider using efficient sorting
algorithms like quicksort or mergesort to minimize processing time.
 Optimize for Readability and Maintainability
Use descriptive variable and method names to make the code self-explanatory. Add
comments where needed to explain complex elevator control strategies or algorithms.
 Handle Exceptions Gracefully
Use try-catch blocks to handle exceptional cases, such as invalid floor numbers or passenger
queues, and provide meaningful error messages.
 Avoid Magic Numbers and Strings
Define constants or enumerations for elevator states, floor numbers, or passenger-related
values, making the code more readable and maintainable.
 Use LINQ for Data Manipulation
Utilize LINQ for querying and manipulating passenger queues, floor requests, or elevator
state management, as it simplifies code and enhances readability.
 Prefer Asynchronous Programming for IO-bound Operations
For handling multiple elevators or passengers asynchronously, use async and await when
performing I/O-bound operations like simulating elevator movement or handling bu􀆩on
presses.
 Implement Proper Error Handling
Gracefully handle exceptions in scenarios like incorrect input or invalid elevator operations.
Log errors and provide helpful messages to assist with debugging.
 Use Recursion with Care
Consider iterative approaches to manage elevator movement and passenger queues,
especially for larger elevator systems, to avoid potential stack overflow issues.
 Profile and Optimize Performance
Profile the elevator system to identify any performance bo􀆩lenecks. Optimize critical
sections or algorithms that affect elevator movement and passenger handling.
 Unit Test Your Code
Write unit tests to verify the correctness of the elevator's movement logic, floor handling,
and passenger interactions. Automated tests ensure that the elevator system behaves as
expected and remains reliable as you make changes.
By applying these industry best practices to the elevator challenge, you can build a well-structured,
efficient, and maintainable C# application that accurately mimics elevator movement and provides a
seamless experience for passengers.
