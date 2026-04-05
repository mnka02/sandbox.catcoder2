# sandbox.catcoder
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white)![Rider](https://img.shields.io/badge/Rider-000000.svg?style=for-the-badge&logo=Rider&logoColor=white&color=black&labelColor=crimson)


|                      | Reference |
| -------------------- | --------- |
| Documentation        |           |
| Latest Release       |           |
| Supported Plattforms |           |

This repository contains my solutions to various challenges from the level-based [Catalysts Coding Contest (CCC)](https://register.codingcontest.org). Each solution reflects my approach to problem-solving across different difficulty levels. Beyond solving the tasks, I aim to integrate common design patterns, improve code structure, and apply principles like Test-Driven Development (TDD). My goal is to evolve each challenge into a compact, standalone project that emphasizes readability, maintainability, and thoughtful complexity.



## 🚀 Getting Started

### Prerequisites
- [.NET SDK 6 or later](https://dotnet.microsoft.com/en-us/download)
- [Git](https://git-scm.com)

### 1. Clone the repository
````bash
$ git clone https://github.com/mnka02/sandbox.catcoder.git
````



## Coding Conventions

### Unit Testing

````csharp
[Fact]
// public void Expect[Expected result]_When[State under test]() 
public void ExpectZero_WhenItemsEmpty () {
  // Arrange the state of the data to set it up for testing.
  var cart = new ShoppingCart();
  // Act on the data through some method that performs an action.
  var result = cart.CalculateTotal();
  // Assert that the result from acting on that data is what we expect it to be.
  Assert.Equal(0, result);
}
````



## Doc Convetions

## Pull Requests

````markdown
## Description
Briefly explain what this PR does.

## Changes
- Renamed file X → Y
- Updated logic in Z
- Fixed bug in ...

## Why
Explain the reason for the change (bug fix, improvement, refactor, etc.)

## How to Test
Steps to verify the changes:
1. Run the app
2. Do X
3. Expect Y

## Screenshots (if applicable)
Add screenshots or recordings if UI changes are involved.

## Notes
Anything reviewers should know (edge cases, limitations, etc.)
````

````markdown
## Description
Renamed a file to follow naming conventions.

## Changes
- Renamed `oldFileName.js` → `new-file-name.js`

## Why
To maintain consistent file naming across the project.

## How to Test
1. Run the project
2. Ensure imports still work correctly
3. Verify no errors occur

## Notes
No functional changes were made.
````

