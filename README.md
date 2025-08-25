# CheckInSimulation
# API de Asignación de Asientos ✈️

## 📌 Descripción
Esta API permite asignar asientos a pasajeros de vuelos siguiendo estas reglas:
- Los pasajeros menores de edad (<18) deben quedar sentados al lado de al menos un adulto de su grupo (según el `purchaseId`).
- Se intenta ubicar a los pasajeros del mismo grupo juntos o lo más cercanos posible.
- Cada pasajero solo puede ocupar asientos de su clase (`económica`, `económica premium` o `primera clase`).
