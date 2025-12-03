# Water Bill Management System

A robust Windows Forms application for managing water utility customers, built with C# and .NET 8.0. This system handles customer data management, bill calculation based on specific customer types (Household, Administrative, Production, Business), invoice generation, and data persistence.

## 🏗 Architecture

The application follows a strict **Object-Oriented Programming (OOP)** architecture, emphasizing separation of concerns and maintainability.

### 1. Application Entry (`Program.cs`)
- **Role**: The entry point.
- **Flow**: Initializes application settings -> Launches `LoginForm` -> If login successful (Result=OK) -> Launches `Form1` (Dashboard).

### 2. Security (`LoginForm.cs`)
- **Role**: Simple authentication gatekeeper.
- **Credentials**: Default is `admin` / `admin`.
- **Function**: Prevents unauthorized access to the main dashboard.

### 3. Domain Logic (`Customer.cs`)
- **Role**: Encapsulates the core business rules and bill calculations.
- **Pattern**: Uses **Inheritance** and **Polymorphism**.
- **Base Class**: `Customer` (Abstract). Defines shared properties (`Name`, `Readings`) and the `CalculateBill()` contract.
- **Subclasses**:
  - `HouseholdCustomer`: Implements tiered pricing logic based on `PeopleCount`.
  - `AdminCustomer`: Flat rate calculation (9,955 VND/m³).
  - `ProductionCustomer`: Flat rate calculation (11,615 VND/m³).
  - `BusinessCustomer`: Flat rate calculation (22,068 VND/m³).
- **Calculations**: All billing methods include a 10% Environment Fee and allow for a 10% VAT calculation on top.

### 4. Data Management (`CustomerManager.cs`)
- **Role**: The "Controller" that orchestrates logic.
- **Responsibilities**:
  - Holds the in-memory list of customers.
  - Uses a **Factory Pattern** (`CreateCustomerFactory`) to instantiate the correct `Customer` subclass based on input strings.
  - Handles Adding, Updating (with type conversion support), Deleting, Searching, and Sorting.
  - Bridges the UI and the Data Repository.

### 5. Data Persistence (`CustomerRepository.cs`)
- **Role**: Handles saving and loading data.
- **Storage**: Uses a local `customers.json` file.
- **Pattern**: **Repository Pattern**. Decouples the storage mechanism from the business logic.
- **Serialization**: Uses `System.Text.Json` with a DTO (Data Transfer Object) approach to safely serialize polymorphic objects.
- **Export**: Includes functionality to export data to CSV format.

### 6. User Interface
#### Dashboard (`Form1.cs`)
- **Role**: The main hub.
- **Features**:
  - Displays customer list in a `ListView`.
  - Provides access to Add, Edit, Delete, Export, and Invoice functions.
  - Real-time updates when data changes.

#### Editor (`CustomerForm.cs`)
- **Role**: A dedicated modal dialog for data entry.
- **Features**:
  - **Dynamic UI**: Shows/hides the "People Count" field automatically based on the selected Customer Type.
  - **Validation**: Ensures inputs are valid (e.g., positive numbers, This Month >= Last Month) before allowing a save.

## 🚀 Key Features
- **Tiered Calculation**: Accurate billing for households based on per-person usage quotas.
- **Data Persistence**: Automatically saves data to `customers.json`; data persists across restarts.
- **Invoicing**: Generates detailed text-based invoices (`Invoice_Name_Date.txt`).
- **Export**: Exports the full customer list to `.csv` for Excel compatibility.
- **Search & Sort**: Filter customers by name or sort the list alphabetically.

## 🛠 Requirements
- .NET 8.0 SDK
- Windows OS (for Windows Forms support)