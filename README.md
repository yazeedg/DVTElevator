# DVT Elevator Challenge

This solution is a console application in C# that simulates the movement of
elevators within a large building, with the aim of optimizing passenger transportation efficiently. The
application should adhere to Object-Oriented Programming (OOP) principles to ensure modularity
and maintainability.

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

The console application must include the following key features:
- **Elevator System Menu**
  
  ![image](https://github.com/user-attachments/assets/45df3fc1-3833-4266-b011-ccf4c7ef81b6)

- **Real-Time Elevator Status**  
    Display the real-time status of each elevator, including its current floor, direction of movement,
whether it's in motion or stationary, and the number of passengers it is carrying.
    ![image](https://github.com/user-attachments/assets/fed75678-b8a7-4110-98d1-66b817c42642)

- **Interactive Elevator Control**  
    Allow users to interact with the elevators through the console application. Users should be able
to call an elevator to a specific floor and indicate the number of passengers waiting on each floor.
![image](https://github.com/user-attachments/assets/5c341b77-1508-48fe-96a2-036beaa5e6be)

- **Multiple Floors and Elevators Support**  
Design the application to accommodate buildings with multiple floors and multiple elevators.
Ensure that elevators can efficiently move between different floors.
- **Efficient Elevator Dispatching**  
Implement an algorithm that efficiently directs the nearest available elevator to respond to an
elevator request. Minimize wait times for passengers and optimize elevator usage.
- **Passenger Limit Handling**  
Consider the maximum passenger limit for each elevator. Prevent the elevator from becoming
overloaded and handle scenarios where additional elevators might be required.
![image](https://github.com/user-attachments/assets/66a6cdb0-9e0a-4e68-b207-35a0d3278ad3)

- **Consideration for Different Elevator Types**  
Although the challenge focuses on passenger elevators, consider the existence of other elevator
types, such as high-speed elevators, glass elevators, and freight elevators. Plan the application's
architecture to accommodate future extension for these types.
- **Real-Time Operation**  
Ensure that the console application operates in real-time, providing immediate responses to user
interactions and accurately reflecting elevator movements and status.




