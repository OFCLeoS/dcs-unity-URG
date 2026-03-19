# Coding Practices
---------
- Most C# naming conventions are to be used

- Most documentation should live in the code. Self-Explanatory code does not need extensive documentation.
    - Outside Documentation should only be done for complicated features.

- Variables that include positions, rotations or scales are expected to be local unless stated otherwise.

- Debug commands should be commented out/deleted when not in use, as they clutter the console and are known to cause a considerable amount of performance issues.

- The use of the #region feature is recommended. Most scripts should have an "Initialization" region which contains everything that is run before the first Update() call.

- Awake(), Start() and Update() methods should be kept clean. Functionality that must be added to those methods should be done by creating other methods, which are then placed in the former ones.
