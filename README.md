# DVT Elevator Challenge

This solution is a console application in C# that simulates the movement of
elevators within a large building, with the aim of optimizing passenger transportation efficiently. The
application should adhere to Object-Oriented Programming (OOP) principles to ensure modularity
and maintainability.

| Plugin | README |
| ------ | ------ |
| Application Type | Console |
| .NET Version | 8.0 |
| Test | Xunit V 2.9.2 |

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
  
  ![image](https://github.com/user-attachments/assets/6b934aa9-9917-4eb7-8c6e-51e3bbce24c5)



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

![image](https://github.com/user-attachments/assets/93f9ac7d-935d-4de9-8de8-6b1b6207d553)

- **Passenger Limit Handling**  
Consider the maximum passenger limit for each elevator. Prevent the elevator from becoming
overloaded and handle scenarios where additional elevators might be required.
![image](https://github.com/user-attachments/assets/e276fef4-a31c-4e13-a716-5e804011d6d4)

- **Consideration for Different Elevator Types**  
Although the challenge focuses on passenger elevators, consider the existence of other elevator
types, such as high-speed elevators, glass elevators, and freight elevators. Plan the application's
architecture to accommodate future extension for these types.
- **Real-Time Operation**  
Ensure that the console application operates in real-time, providing immediate responses to user
interactions and accurately reflecting elevator movements and status.

![image](https://github.com/user-attachments/assets/44edfc85-20be-4425-a01f-f8913ffc0f89)


- **Evaluation and validation of valid floor number** 
![image](https://github.com/user-attachments/assets/3f25d6a4-ea0d-41c1-a0df-61fa427bdcce)

- **Evaluation and validation of maximum passengers for an elevator** 
![image](https://github.com/user-attachments/assets/8e8d8ab0-1fad-41c7-aa5e-a7a2eac86058)


