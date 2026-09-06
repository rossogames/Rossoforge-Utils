# Rosso Games

<table>
  <tr>
    <td><img src="https://github.com/rossogames/Rossoforge-Utils/blob/main/logo.png?raw=true" alt="Rossoforge" width="64"/></td>
    <td><h2>Rossoforge - Utils</h2></td>
  </tr>
</table>

## Overview

**Rossoforge-Utils** is a general-purpose utility package for the Rossoforge ecosystem. It provides essential, reusable components designed to accelerate and simplify Unity development workflows. Whether you need robust logging, efficient state management, or reliable file handling, this package has you covered.

### Key Features

- **Configurable Logging System** - A flexible logging framework with multiple output targets and customizable formatting
- **Lightweight State Machine** - An efficient implementation for managing complex state transitions in your game logic
- **File Handling Utilities** - Streamlined APIs for reading, writing, and managing files across different platforms
- **Pure C# Implementation** - No external dependencies required for core functionality
- **Production-Ready** - Battle-tested components designed for professional game development

---

## Requirements

| Requirement | Version |
|------------|---------|
| **Unity** | 6.0 or higher |
| **Newtonsoft JSON** | com.unity.nuget.newtonsoft-json |

## Installation

1. Add the package to your Unity project via Package Manager
2. Ensure `com.unity.nuget.newtonsoft-json` is installed
3. Start using Rossoforge-Utils components in your project

---

## Quick Start

### Logging System

```csharp
// Initialize the logger
var logger = new Logger();

// Log messages at different levels
logger.Log("Application started");
logger.Warning("This is a warning");
logger.Error("An error occurred");
```

### State Machine

```csharp
// Create and configure a state machine
var stateMachine = new StateMachine<GameState>();

// Add states and transitions
stateMachine.AddState(GameState.Menu);
stateMachine.AddState(GameState.Playing);
stateMachine.Transition(GameState.Menu, GameState.Playing);
```

### File Handling

```csharp
// Read and write files easily
var fileManager = new FileManager();
fileManager.WriteFile("path/to/file.txt", "content");
string content = fileManager.ReadFile("path/to/file.txt");
```

---

## Package Composition

- **Logging Module** - Configurable logging with multiple output handlers
- **State Machine Module** - Generic state machine implementation with transition support
- **File Utilities Module** - Cross-platform file I/O operations
- **Core Extensions** - Utility methods and helpers for common operations

---

## Documentation

For detailed API documentation and advanced usage examples, please refer to the inline code documentation and the [Rossoforge Documentation](https://github.com/rossogames).

---

## About

This package is part of the **Rossoforge** suite, a comprehensive toolkit designed to streamline and enhance Unity development workflows. It enables developers to focus on game logic while providing battle-tested utilities for common tasks.

### Developer

**Agustin Rosso**  
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Profile-blue)](https://www.linkedin.com/in/rossoagustin/)

---

## License

Rossoforge-Utils is provided as part of the Rossoforge ecosystem. Please refer to the LICENSE file for terms and conditions.

---

**Rossoforge** - *Empowering Unity Game Development*
