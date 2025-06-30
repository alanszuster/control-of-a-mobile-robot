# 🤖 Control of a Mobile Robot - Desktop Application

[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![WPF](https://img.shields.io/badge/WPF-512BD4?style=for-the-badge&logo=.net&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
[![.NET Framework](https://img.shields.io/badge/.NET_Framework-512BD4?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/en-us/download/dotnet-framework)

## 📋 Description

A Windows desktop application for controlling mobile robots via TCP/IP connection. Built with WPF (Windows Presentation Foundation) and C#, this application provides a comprehensive interface for robot control, sensor monitoring, and real-time communication.

## ✨ Features

### 🔌 Connection Management

- **TCP/IP Communication**: Connect to your robot via configurable IP address and port
- **Real-time Status**: Visual connection indicators (green/red/yellow status lights)
- **Auto-disconnect Detection**: Automatic handling of connection failures

### 🎮 Robot Control Modes

- **Manual Control**: Direct command input via text interface
- **Slider Control**: Visual motor speed control with real-time feedback
- **Keyboard Control**: Intuitive keyboard-based robot navigation
- **Reset Function**: Emergency stop and reset capabilities

### 🔧 Motor Control

- **Dual Motor Support**: Independent control of left and right motors
- **Speed Range**: -128 to +127 speed control for each motor
- **Real-time Feedback**: Live display of current motor values
- **Visual Sliders**: Intuitive slider interface for motor adjustment

### 📡 Sensor Monitoring

- **5-Sensor Array**: Real-time monitoring of 5 distance sensors
- **Visual Progress Bars**: Graphical representation of sensor readings
- **Numerical Values**: Precise sensor data display
- **Range**: 0-2000 units sensor range

### 💡 LED Control

- **Dual LED Support**: Control of two independent LEDs
- **Checkbox Interface**: Simple on/off control for each LED
- **Real-time Updates**: Immediate LED state changes

### 🔋 System Monitoring

- **Battery Status**: Real-time battery level monitoring
- **Message Display**: Incoming and outgoing message visualization
- **Connection Status**: Live connection state monitoring

## 🛠️ Technical Specifications

- **Framework**: .NET Framework 4.6.1
- **UI Technology**: WPF (Windows Presentation Foundation)
- **Language**: C#
- **Communication**: TCP/IP Sockets
- **Threading**: Multi-threaded for responsive UI
- **Real-time Updates**: Continuous sensor and status monitoring

## 🚀 Getting Started

### Prerequisites

- Windows OS (Windows 7 or later)
- .NET Framework 4.6.1 or higher
- Visual Studio 2017 or later (for development)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/control-of-a-mobile-robot.git
   ```

2. Open the solution in Visual Studio:

   ```text
   WpfApp2.sln
   ```

3. Build and run the application:
   - Press `F5` or click "Start" in Visual Studio
   - Or build and run the executable from `bin/Debug/MobileRobots.exe`

### Usage

1. **Connect to Robot**:
   - Click "IP" button to configure connection settings
   - Enter robot's IP address (default: 127.0.0.1)
   - Set port number (default: 8000)
   - Click "Connect" to establish connection

2. **Control Robot**:
   - Use "Manual" mode for direct command input
   - Use "Sliders" mode for visual motor control
   - Use "Keyboard" mode for intuitive navigation
   - Toggle LEDs using checkboxes

3. **Monitor Status**:
   - Watch sensor readings in real-time
   - Monitor battery level
   - Check connection status via status lights

## 🎯 Use Cases

- **Educational Robotics**: Perfect for learning robot control concepts
- **Prototype Testing**: Test mobile robot behaviors and responses
- **Remote Operation**: Control robots from a distance via network
- **Sensor Monitoring**: Real-time data collection and analysis
- **Research Projects**: Academic and research applications

## 🔧 Configuration

The application supports configurable connection parameters:

- **IP Address**: Robot's network address
- **Port**: Communication port (default: 8000)
- **Motor Limits**: Speed range -128 to +127
- **Sensor Range**: 0-2000 units


## 📄 License

This project is open source and available under the [MIT License](LICENSE).

## 🏷️ Tags

`robotics` `wpf` `csharp` `tcp-ip` `mobile-robot` `control-system` `sensors` `motors` `desktop-application` `real-time` `automation` `embedded-systems`

