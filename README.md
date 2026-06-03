# 🚗 AutoTallerManager Backend

Backend ASP.NET Core con arquitectura por proyectos `Api`, `Application`, `Domain` e `Infrastructure`, EF Core y MySQL.

## 📚 Documentacion importante

- Documentacion completa del proyecto: [docs/PROYECTO_COMPLETO.md](docs/PROYECTO_COMPLETO.md)
- Credenciales de prueba: [README-CREDENCIALES.md](README-CREDENCIALES.md)
- Repo frontend: [wen-27/.netFrontend.git](https://github.com/wen-27/.netFrontend.git)
- Swagger local: `http://localhost:5213/swagger`
- Frontend local: `http://localhost:5173`

## ⚠️ Importante antes de ejecutar

- MySQL debe estar encendido.
- Configura `ConnectionStrings:MySql` y `Jwt:Key` con `dotnet user-secrets` o variables de entorno.
- No guardar password real de MySQL ni `Jwt:Key` real en `appsettings.json`.
- En macOS se recomienda usar `server=127.0.0.1` en vez de `server=localhost`.
- Al correr en `Development`, se ejecutan seeders automaticamente.
- Si recibes `401`, falta token o expiro.
- Si recibes `403`, el rol del usuario no tiene permiso.
- Si recibes `429`, se activo el rate limit.

## 👥 Roles disponibles

- `Admin`
- `Receptionist`
- `Mechanic`
- `Client`
- `WorkshopChief`
- `WarehouseChief`
- `InventoryManager`

En la interfaz pueden mostrarse en español, pero el backend usa estos nombres exactos para JWT y autorización.

## 🔑 Usuarios seed de desarrollo

Estos usuarios son solo para ambiente de desarrollo. La contraseña seed comun es `DevPass123!` y queda hasheada en la base de datos.

- `admin@autotaller.com`
- `recepcionista@autotaller.com`
- `mecanico@autotaller.com`
- `diagnostico@autotaller.com`
- `mantenimiento@autotaller.com`
- `electricista@autotaller.com`
- `frenos@autotaller.com`
- `carlos.ramirez@test.com`
- `laura.gomez@test.com`
- `jefe.mecanicos@autotaller.com`
- `jefebodega@autotaller.com`
- `jefealmacen@autotaller.com`

## 🧭 Flujos implementados

### 🛠️ Solicitudes técnicas adicionales

El mecánico asignado crea una solicitud adicional para una orden. La solicitud inicia en `PendingWorkshopChiefApproval`. El Jefe de Taller puede aprobarla y enviarla al cliente como `PendingClientApproval`, o rechazarla como `RejectedByWorkshopChief`. El cliente dueño de la orden puede aprobar o rechazar. Al aprobar, el backend valida stock, añade el servicio o repuesto a la orden y pasa la solicitud a `AddedToOrder`.

### ✅ Aprobación del cliente

El cliente solo puede consultar sus propias órdenes, aprobaciones y mensajes. Las solicitudes rechazadas por el Jefe de Taller no se exponen al cliente.

### 📦 Bodega y almacén

El Jefe de Bodega crea `StockSubmission`, calcula precio de venta y envía a revisión. El Jefe de Almacén aprueba o rechaza. Al aprobar, se crea o actualiza el repuesto oficial (`Part`) y se registra historial de inventario.

### 🧾 Inventario oficial

El inventario oficial solo cambia cuando `InventoryManager` aprueba una solicitud de stock. El flujo registra la decisión y el movimiento en `InventoryHistory`.

### 💳 Pagos por recepción

El cliente registra pagos de facturas propias. El pago queda en `PendingReceptionVerification`. `Receptionist` o `Admin` aprueba o rechaza. Al aprobar se actualiza factura, orden y fecha de entrega.

## 🧮 Fórmulas

Precio de producto:

```txt
Precio venta = precio proveedor + (precio proveedor * porcentaje ganancia / 100)
```

Precio de servicio:

```txt
Subtotal repuestos = suma(precio venta repuesto * cantidad requerida)
Valor mano de obra = subtotal repuestos * porcentaje mano de obra / 100
Precio final = subtotal repuestos + valor mano de obra
```

## 🌐 Endpoints nuevos

### 🔧 Mecánico

- `GET /api/mechanic/orders`
- `GET /api/mechanic/orders/{orderId}`
- `GET /api/mechanic/requests`
- `POST /api/mechanic/orders/{orderId}/additional-requests`
- `POST /api/mechanic/orders/{orderId}/work`

### Jefe de Taller

- `GET /api/workshop-chief/requests`
- `GET /api/workshop-chief/requests/{requestId}`
- `POST /api/workshop-chief/requests/{requestId}/approve`
- `POST /api/workshop-chief/requests/{requestId}/reject`
- `GET /api/workshop-services`
- `GET /api/workshop-services/{id}`
- `POST /api/workshop-services`
- `PUT /api/workshop-services/{id}`
- `PATCH /api/workshop-services/{id}/activate`
- `PATCH /api/workshop-services/{id}/deactivate`

### Cliente

- `GET /api/client/orders`
- `GET /api/client/orders/{orderId}`
- `GET /api/client/approvals`
- `POST /api/client/approvals/{requestId}/approve`
- `POST /api/client/approvals/{requestId}/reject`
- `GET /api/client/messages`
- `POST /api/client/payments`

### Recepción

- `GET /api/reception/payments/pending-verification`
- `GET /api/reception/payments/{paymentId}`
- `POST /api/reception/payments/{paymentId}/approve`
- `POST /api/reception/payments/{paymentId}/reject`
- `POST /api/reception/orders/{orderId}/confirm-delivery-date`

### Bodega

- `GET /api/warehouse/products`
- `POST /api/warehouse/products`
- `PUT /api/warehouse/products/{id}`
- `GET /api/warehouse/stock-submissions`
- `GET /api/warehouse/stock-submissions/{id}`
- `POST /api/warehouse/stock-submissions`
- `POST /api/warehouse/stock-submissions/{id}/send-to-review`

### Almacén

- `GET /api/inventory/review-requests`
- `GET /api/inventory/review-requests/{id}`
- `POST /api/inventory/review-requests/{id}/approve`
- `POST /api/inventory/review-requests/{id}/reject`
- `GET /api/inventory/products`
- `GET /api/inventory/history`

## Migraciones y seeders

Los seeders son idempotentes y se ejecutan en desarrollo desde `Program.cs` con `SeedDevelopmentDataAsync`.

Los comandos de build, migrations, database update y run deben ser ejecutados manualmente por el desarrollador, no por el agente.

Desde la raíz del repositorio:

```bash
dotnet ef migrations add AddOperationalWorkflowAndSeeders --project Infrastructure --startup-project Api
dotnet ef database update --project Infrastructure --startup-project Api
dotnet build
dotnet run --project Api
```

### Resetear base local y resembrar datos

Usa estos comandos solo en desarrollo. `database drop` elimina la base de datos local configurada para el proyecto, incluyendo datos ingresados manualmente.

Desde la raíz del repositorio (`/Users/wen/.NetProyecto-1`):

```bash
dotnet ef database drop --project Infrastructure --startup-project Api
dotnet ef database update --project Infrastructure --startup-project Api
dotnet run --project Api
```

Si ya estas dentro de la carpeta `Api`, usa estas rutas:

```bash
dotnet ef database drop --project ../Infrastructure/Infrastructure.csproj --startup-project ./Api.csproj
dotnet ef database update --project ../Infrastructure/Infrastructure.csproj --startup-project ./Api.csproj
dotnet run --project ./Api.csproj
```

Al ejecutar `dotnet run --project Api` en ambiente `Development`, el backend corre `DevelopmentDataSeeder` automaticamente y vuelve a crear los datos iniciales.

Si `dotnet ef` no esta disponible:

```bash
dotnet tool install --global dotnet-ef
```

Para actualizar la herramienta:

```bash
dotnet tool update --global dotnet-ef
```
