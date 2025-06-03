# DeliverySchedulerBackend

A .NET Core Web API for calculating valid delivery time slots for an online grocery store, based on product-specific delivery constraints.

## Features
- Calculates valid delivery slots for a given cart of products
- Handles product-specific delivery rules:
  - **In-Stock:** Same-day if before 18:00
  - **Fresh Food:** Same-day if before 12:00
  - **External:** At least 3 days in advance, only Tuesday–Friday
- Only allows deliveries on weekdays (Monday–Friday)
- Supports “green” delivery slots (off-peak hours, e.g., 13:00–15:00, 20:00–22:00)
- Returns slots ordered by date, with green slots first each day
- In-memory product repository for easy testing (no database required)

## Getting Started

### Prerequisites
- [.NET 6 SDK or later](https://dotnet.microsoft.com/download)

### Installation & Running
1. Clone the repository:
   ```bash
   git clone <your-backend-repo-url>
   cd DeliverySchedulerBackend
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Run the API:
   ```bash
   dotnet run
   ```
   By default, the API will listen on `http://localhost:5162` (or as configured in `launchSettings.json`).

## API Usage

### Get Available Delivery Slots
- **Endpoint:** `POST /api/delivery/slots`
- **Body:** JSON array of product IDs (GUIDs)
  ```json
  [
    "a0f47a90-5fc4-4f31-bbb9-0f5f4a0a98e3",
    "b1c25c1f-9dc5-4cd1-9a17-89099e1ad1e6"
  ]
  ```
- **Response:** Array of delivery slots
  ```json
  [
    {
      "date": "2025-06-03T00:00:00",
      "start": "13:00:00",
      "end": "14:00:00",
      "isGreen": true
    },
    ...
  ]
  ```

## Product Types (Sample Data)
- **Milk** (Fresh Food): `a0f47a90-5fc4-4f31-bbb9-0f5f4a0a98e3`
- **Soap** (In-Stock): `b1c25c1f-9dc5-4cd1-9a17-89099e1ad1e6`
- **Imported Cheese** (External): `c2d57d4a-3e25-4a2b-a2e2-29c72a29b8e9`

## Customization
- To change green slot hours, edit the `greenRanges` in `DeliverySlotService`.
- To add more products, update the `InMemoryProductRepository`.

## Project Structure
- `Controllers/DeliveryController.cs` — API endpoint
- `Application/UseCases/GetAvailableDeliverySlots.cs` — Main business logic
- `Application/Services/DeliverySlotService.cs` — Slot generation and constraints
- `Infrastructure/Repositories/InMemoryProductRepository.cs` — Sample product data

## License
MIT
