# Guidelines for IdeaLab

 
### -2: Don't bully anyone, especially Adam

---

### -1: Never base scenes on Adam's work

---

### 0: Fields must be private by default, not public
This ensures that we don’t end up with a _NegativeFeedbackLoop_ and _PowerUp_ problem again.

###### // Evt. brug properties istedet for public variabler

---

### 1: Naming conventions

* public: _TheVariable
* private: _theVariable
* protected: theVariable
* parameters: theVariable
* local variables: theVariable
* ScriptableObjects classes: SOClassName
* ScriptableObjects variables: SO_TheVariable || SO_theVariable
* static: s_TheVariable or s_theVariable

###### // Måske bør underscore genovervejes i public variabler, da det ikke standard i C#. Dette kan potentielt skabe en dårlig vane. 
###### // Forslag: Metoder bør være udsagnsord (UpdateX, ApplyX, SetX, DisplayX) og variabler bør være beskrivende og meningsfulde navneord (timeDuration, elevatorSpeed). Booleans bør hedde `is`, `has`, `can`, or `should`.
###### // Desuden er der kode highlights så man kan alligevel se på farven om man tilgår en variabel eller metode, og metoder har altid paranteser.
###### // Andet forslag: tilføj `s_` prefix på statiske variabler, eks: `public static string s_name`.

---

### 2: Always write comments in your code!
We all want to understand our code, using comments will help a lot.

###### // Skal vi altid skrive kommentarer? Kommentar bør skrives når det er nødvendigt, f.eks forklaring af kompleks logik og hvorfor koden bruges. Overdriven kommentarer kan gøre kode ulæseligt og rodet.
###### // Hvis koden er velstruktureret, bruger meningsfulde navne og overholder konventioner, kan det tale for sig selv. Her er mindre gode eksempler på kommentarer fra vores tidligere projekter:

```
--
public float time = 7f; // Duration of the power up.
# public float durationOfPowerUp = 7f;
--

private float targetMaxOrthoSize = 8f;    // Set your target maximum orthographic size here
private float _speed = 1.5f;     // Elevator speed. Change in inspector
let gridX;   // columns
let gridY;   // rows
public bool _ExitLadder   // Used to check when you can interact


// This instanciates the GameInput script if there is non
if (_gameInput == null) 
{
    _gameInput = new GameInput();
}
```
I sidste ende er målet at skrive ren og vedligeholdelsesfri kode, der minimerer behovet for kommentarer. Kommentarer er vigtige, men kun når de behøves.
"A comment is a failure to express yourself in code." - Robert C. Martin


```
// To standardize our codebase, 
// our comment block should 
// look like this
 
 
/* 
    To standardize our codebase, 
    our comment block should 
    look like this
*/
```

---

### 3: Avoid working on the same scene together
This will cause huge conflicts.

If it's necessary to work on the same scene together, let person A finish their changes before person B add theirs.

---

### 4: Use `Update()` only when it's necessary
The `Update()` function is heavy on the CPU, therefore it should only be used when it's truly necessary such as physics. 

---

### 5: No use of `Time.time`!
This can cause issues when the game runs for a long time. Use `Time.deltaTime` instead as `Time.time` are easy to misuse.

---

### 6: Scripts must be decoupled from each other
This would for example mean that scripts that control Movement-logic should not include any polish or juice. 

Separating code helps us to prevent large dependency issues as we did in Jerk'n'Jolt. Like the `PlayerMovement.cs` that didn't work unless all the sounds were added in the Unity Editor.
  * The best likely solution for this is some kind of event-based system.
  * And we have separation of concerns. Here we can make use of the ScriptableObject class.

---

### 7: Better Architecture in Unity!
Jerk'n'Jolt was horrid to look at! Everything must be nice and tidy so it's easy to find things. 

We should use always have an elaborate folder system. Alternatively, make some more use of inheritance and interfaces. (UML diagrams may be useful here)

---

### 8: Never use `var`
The `var` keyword obfuscates code and makes it less clear. Data types should always be explicitly typed to enhance readability. 

---

### 9: Structure your code
Don't leave empty whitespaces, lines, and gaps in your code. Unorganised code can negatively impact readability, navigation, and generally looks much less appealing.

---

### 10: Don't neglect Null Checks
Failing to account for null-safety may lead to runtime errors. Here are ways to check for null values:
* Nullable type modifier: `Type? theVariable`
* If-checks: `if (obj == null) Debug.LogError("Message");`
* Null-coalescing operator: 
```
int? number = null;
int result = number ?? 42; // Result will be 42
```



