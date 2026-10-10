# :video_game: Unity Portfolio: Bartender

An arcade action mechanic inspired by classic Tapper. The bartender serves drinks from right to left while managing incoming waves of customers walking down parallel bars.

## :file_folder: Quick Code Links
Skip the folder navigation! Dive straight into the core scripts:
* [Customer.cs](Assets/Scripts/Customer/Customer.cs) - Drives customer navigation, collision tracking with mugs and triggers the fail-state conditions
* [Mug.cs](Assets/Scripts/Mug/Mug.cs) - Handles object pooling and physics logic for the drink containers, managing collision states with both consumers and the end-of-bar boundaries.
* [UI_Manager.cs](Assets/Scripts/UI/UI_Manager.cs) - Manages central gameplay state UI, including scoring, player lives, and game-over overlays via events.
* [MoveBartender.cs](Assets/Scripts/Player/MoveBartender.cs) - Implements player-controlled grid/lane movement, enabling responsive shifting between separate bar counters.

## :hammer_and_wrench: Key Technical Highlights
* **Design Patterns:** Leveraged a clean **Observer Pattern** to broadcast scoring events from the Customer and Mug interactions directly to the UI_Manager without hard decoupling dependencies.
* **Architecture:** Utilized **Abstract Tagging/Interfaces** for physics interactions, making it easy to identify when a moving object is a valid customer vs. an empty bar threshold.
* **Code Cleanliness:** Structured around strict lane/grid logic to prevent unpredictable physics bugs and ensure arcade-precise collision detection.
