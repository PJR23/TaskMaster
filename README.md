# TaskMaster

**TaskMaster** is a task management application developed using .NET MAUI (Multi-platform App UI). The app helps users organize and track their tasks, manage projects, and streamline their workflow. It features a user-friendly interface, task categorization, and a robust backend to support task management.

## Features

- **Task Management:** Create, edit, and delete tasks.
- **Project Management:** Organize tasks into projects.
- **User Authentication:** Secure login system.
- **Task Notifications:** Reminders and due dates for tasks.
- **Data Storage:** Backend integration with a database.

## Prerequisites

To run TaskMaster locally, you'll need the following:

- **Visual Studio 2022** (or later) with .NET MAUI workload installed.
- **.NET 7 SDK** (or later).
- **Android SDK** (included with Visual Studio).
- **Android Emulator**: We recommend using a Pixel 8 Pro virtual device for best performance.

## Installation

### Setting Up the Development Environment

1. **Install Visual Studio:**

    - Download and install [Visual Studio 2022](https://visualstudio.microsoft.com/).
    - During installation, select the **.NET MAUI** workload.

2. **Install .NET SDK:**

    - Ensure you have the **.NET 7 SDK** (or later) installed. You can download it from [Microsoft .NET](https://dotnet.microsoft.com/download/dotnet).

3. **Set Up Android Emulator:**

    - Open Visual Studio.
    - Go to **Tools** > **Android** > **Android Emulator Manager**.
    - Create a new virtual device. We recommend using **Pixel 8 Pro** for optimal performance.

### Cloning the Repository

1. **Clone the repository:**

    ```bash
    git clone https://github.com/PJR23/TaskMaster.git
    ```

2. **Navigate to the project directory:**

    ```bash
    cd taskmaster
    ```

### Running the Project

1. **Open the Project in Visual Studio:**

    - Launch Visual Studio 2022.
    - Select **File** > **Open** > **Project/Solution**.
    - Navigate to the `taskmaster` directory and open the `.sln` file.

2. **Restore Dependencies:**

    - Visual Studio should automatically restore any necessary dependencies. If not, you can manually restore them by opening the **Package Manager Console** and running:

      ```bash
      dotnet restore
      ```

3. **Build the Project:**

    - In Visual Studio, select the appropriate target (e.g., Android) from the toolbar.
    - Click **Build** > **Build Solution** to compile the project.

4. **Run the Project:**

    - Ensure your Android emulator (Pixel 8 Pro) is running.
    - In Visual Studio, click the **Run** button (or press `F5`) to deploy and start the app on the emulator.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
