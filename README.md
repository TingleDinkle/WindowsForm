# Water Bill Calc(short for calculator)

A robust Windows Forms application for managing water utility customers, built with C# and .NET 8.0. This system handles customer data management, bill calculation based on specific customer types (Household, Administrative, Production, Business), invoice generation, and data persistence.

## Architecture

The application follows a strict **Object-Oriented Programming (OOP)** architecture, emphasizing separation of concerns and maintainability.

### 1. Application Entry Point (`Program.cs`)

This is where the application starts running.

*   **`Main()` Method:**
    1.  **`ApplicationConfiguration.Initialize()`**: Sets up standard Windows settings (fonts, DPI scaling).
    2.  **`LoginForm login = new LoginForm()`**: Creates an instance of the Login screen.
    3.  **`login.ShowDialog()`**: Displays the login window modally (meaning the code pauses here until the window closes).
    4.  **`if (Result == DialogResult.OK)`**:
        *   If the user logged in successfully, the Login Form sets its result to `OK`.
        *   **`Application.Run(new Form1())`**: The program then launches the Main Dashboard (`Form1`).
    5.  **`else`**: If the user closed the login window or clicked Cancel, the app exits immediately.

---

### 2. The Login Screen (`LoginForm.cs`)

A simple security gatekeeper.

*   **`btnLogin_Click`**:
    *   Gets the text from `txtUser` and `txtPass`.
    *   **Logic**: Checks if `user == "admin"` and `pass == "admin"`.
    *   **Success**: Sets `this.DialogResult = DialogResult.OK` (tells `Program.cs` to proceed) and closes itself.
    *   **Failure**: Shows a `MessageBox` error.
*   **`btnExit_Click`**: Sets `DialogResult.Cancel` and closes, causing the app to shut down.

---

### 3. The Domain Logic (`Customer.cs`)

This file contains the **Business Rules**. It defines *what* a customer is and *how* to calculate their bill. It uses **Inheritance** and **Polymorphism**.

*   **`abstract class Customer` (The Base Class):**
    *   **`Name`, `LastMonthReading`, `ThisMonthReading`**: Properties that hold data. They have `private set` to ensure they can only be changed via specific methods (Encapsulation).
    *   **`ValidateReadings()`**: A private helper that ensures you cannot enter negative numbers or have "This Month" less than "Last Month".
    *   **`CalculateBill()` (Abstract):** This is a "contract". It forces every subclass (Household, Admin, etc.) to provide their own specific math formula.
    *   **`CalculateBillWithVAT()`**: A shared method. It calls the specific `CalculateBill()` and then multiplies by `1.1` (adds 10% VAT). This ensures VAT is calculated the same way for everyone.

*   **`class HouseholdCustomer : Customer`**:
    *   Adds a `PeopleCount` property (specific to households).
    *   **`CalculateBill()`**: Implements the complex **Tiered Logic**:
        *   Calculates `tier1 = 10 * people`, `tier2 = 20 * people`, etc.
        *   Uses an `if/else if` ladder to check which pricing tier the usage falls into (Price: 5973, 7052, 8699, 15929).
        *   Adds 10% Environment Fee (`billAmount * 0.10`).
        *   Returns the total.

*   **`class AdminCustomer`, `ProductionCustomer`, `BusinessCustomer`**:
    *   These are simpler. They override `CalculateBill()` to use a **Flat Rate** (9955, 11615, or 22068).
    *   They also add the 10% Environment Fee.

---

### 4. The Data Manager (`CustomerManager.cs`)

This class acts as the "Brain" or "Controller". The UI (`Form1`) never talks to the file system directly; it talks to this Manager.

*   **`_customers`**: A generic `List<Customer>` that holds all the customer objects in memory.
*   **`AddCustomer(...)`**:
    *   Takes raw inputs (strings, ints).
    *   Calls **`CreateCustomerFactory`** to create the correct object type.
    *   Adds it to the list.
    *   Calls `_repository.Save()` to write to the file.
*   **`CreateCustomerFactory(...)`**:
    *   A **Factory Pattern**. It takes the `type` string (e.g., "Household") and uses a `switch` statement to return `new HouseholdCustomer(...)`, `new AdminCustomer(...)`, etc.
*   **`UpdateCustomer(...)`**:
    *   Finds the customer at the specific index.
    *   Checks if the "Type" has changed.
        *   If **Yes**: It creates a brand new object (because you can't turn a `Household` object into an `Admin` object).
        *   If **No**: It just updates the existing object's properties (`UpdateName`, `UpdateReadings`).
*   **`SortByName()` / `SearchByName()`**: Helper methods to organize or filter the list for the UI.

---

### 5. The Storage Layer (`CustomerRepository.cs`)

This handles **Saving and Loading** to the hard drive (`customers.json`).

*   **`CustomerDto` Class**: A "Data Transfer Object". It's a simple class with just public properties. We convert our complex `Customer` objects (which have logic and validation) into these simple `Dto` objects to make saving to JSON easier.
*   **`Save()`**:
    *   Converts `List<Customer>` -> `List<CustomerDto>`.
    *   Uses `JsonSerializer.Serialize` to turn the list into a text string.
    *   Writes that string to `customers.json`.
*   **`Load()`**:
    *   Reads `customers.json`.
    *   Converts the text back into `List<CustomerDto>`.
    *   The `CustomerManager` then takes these DTOs and runs them through the Factory to rebuild the real `Customer` objects.

---

### 6. The Main Dashboard (`Form1.cs`)

The visual interface where the user interacts.

*   **`lvCustomer` (ListView)**: The table showing the data.
*   **`RefreshListView()`**:
    *   Clears the table.
    *   Loops through every customer in the Manager.
    *   Calculates the final bill (`CalculateBillWithVAT`).
    *   Adds a row (`ListViewItem`) to the table.
*   **Buttons (`Add`, `Edit`, `Delete`)**:
    *   These buttons don't do math. They simply call methods on `_customerManager` or open the `CustomerForm`.
*   **`btnInvoice_Click`**:
    *   Generates a `.txt` file.
    *   Calls `customer.GetBillInfo()` to get the formatted text block.
    *   Saves it with a timestamped filename (e.g., `Invoice_John_20251203.txt`).

---

### 7. The Editor Dialog (`CustomerForm.cs`)

A specific window just for data entry.

*   **`CustomerForm()` Constructor**:
    *   Used for **Adding**. Starts empty.
*   **`CustomerForm(Customer existing)` Constructor**:
    *   Used for **Editing**. Pre-fills the text boxes with the existing customer's data.
*   **`UpdatePeopleFieldVisibility()`**:
    *   If "Household" is selected in the dropdown, it shows the "Number of People" box.
    *   For any other type, it hides that box (because businesses don't have "people count" for billing).
*   **`btnSave_Click`**:
    *   Performs **UI Validation**: checks if boxes are empty, if numbers are valid integers, etc.
    *   If valid, it sets `DialogResult = OK`, allowing the Main Form to know it can proceed to save.

---

## Flow Summary

1.  **Start**: `Program.cs` -> `LoginForm`.
2.  **Login**: User types `admin`/`admin` -> Success -> `Form1` opens.
3.  **Load**: `Form1` asks `Manager` to load data. `Manager` asks `Repository` to read JSON. `Manager` rebuilds objects.
4.  **View**: `Form1` displays the objects in the ListView.
5.  **Add**: User clicks Add -> `CustomerForm` opens -> User enters data -> `Manager` creates object -> `Manager` saves to JSON -> `Form1` refreshes list.
6.  **Calculate**: Inside `Customer.cs`, the `CalculateBill()` logic runs automatically whenever the bill is needed for display or invoice.

## Key Features
- **Tiered Calculation**: Accurate billing for households based on per-person usage quotas.
- **Data Persistence**: Automatically saves data to `customers.json`; data persists across restarts.
- **Invoicing**: Generates detailed text-based invoices (`Invoice_Name_Date.txt`).
- **Export**: Exports the full customer list to `.csv` for Excel compatibility.
- **Search & Sort**: Filter customers by name or sort the list alphabetically.

## Requirements
- .NET 8.0 SDK
- Windows OS (for Windows Forms support)
