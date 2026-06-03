# AutoTallerManager Backend

Backend ASP.NET Core para administrar la operacion de un taller automotriz. Usa arquitectura por capas, Entity Framework Core, MySQL, JWT, MediatR y FluentValidation.

> Este README es el documento unico del proyecto. Las credenciales incluidas son solo datos seed de desarrollo. No guardar contrasenas reales de MySQL, claves JWT reales, tokens ni dumps de base de datos con datos sensibles.

## Descripcion

`AutoTallerManager` permite gestionar:

- Usuarios, roles y autenticacion.
- Clientes, mecanicos, recepcionistas, jefes de taller, bodega e inventario.
- Vehiculos y propietarios.
- Ordenes de servicio, diagnosticos y trabajo realizado.
- Servicios del taller, repuestos, stock e inventario.
- Solicitudes tecnicas adicionales durante una orden.
- Facturas, pagos y verificacion por recepcion.
- Auditoria de acciones y catalogos base.

## Estructura

```txt
.NetProyecto-1/
|-- Api/
|-- Application/
|-- Domain/
|-- Infrastructure/
|-- AutoTallerManager.slnx
|-- db.sql
`-- README.md
```

- `Api`: entrada HTTP, controladores REST, DTOs, Swagger, JWT, CORS, rate limit, auditoria y manejo global de errores.
- `Application`: casos de uso, handlers, validadores, abstracciones, resultados, paginacion y reglas de aplicacion.
- `Domain`: entidades, value objects, enums, constantes y base de entidades.
- `Infrastructure`: `DbContext`, configuraciones EF Core, repositorios, servicios, seeders, unit of work y migraciones.

## Requisitos

- .NET SDK compatible con `net10.0`.
- MySQL local.
- `dotnet-ef` para migraciones.
- Node.js y `pnpm` si se levanta el frontend.

Verificar MySQL:

```bash
/usr/local/mysql/bin/mysqladmin --protocol=tcp --host=127.0.0.1 --port=3306 --user=root --password=TU_PASSWORD ping
```

Instalar o actualizar `dotnet-ef`:

```bash
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
```

## Configuracion segura

`Api/appsettings.json` debe conservar solo valores no secretos, como `Jwt:Issuer`, `Jwt:Audience` y `Jwt:ExpiresMinutes`. La cadena de conexion y `Jwt:Key` deben configurarse con `dotnet user-secrets` o variables de entorno.

Opcion recomendada en desarrollo:

```bash
cd /Users/wen/.NetProyecto-1/Api
dotnet user-secrets set "ConnectionStrings:MySql" "server=127.0.0.1;port=3306;database=AutoTallerManager;user=TU_USUARIO;password=TU_PASSWORD;Connection Timeout=10;Default Command Timeout=30;"
dotnet user-secrets set "Jwt:Key" "TU_CLAVE_LOCAL_DE_MINIMO_32_CARACTERES"
```

Alternativa con variables de entorno:

```bash
export ConnectionStrings__MySql="server=127.0.0.1;port=3306;database=AutoTallerManager;user=TU_USUARIO;password=TU_PASSWORD;"
export Jwt__Key="TU_CLAVE_LOCAL_DE_MINIMO_32_CARACTERES"
```

En macOS se recomienda usar `127.0.0.1` en vez de `localhost` para MySQL.

## Ejecucion

Desde la raiz del backend:

```bash
cd /Users/wen/.NetProyecto-1
dotnet build
dotnet run --project Api/Api.csproj
```

Tambien se puede ejecutar desde `Api`:

```bash
cd /Users/wen/.NetProyecto-1/Api
dotnet run
```

URLs esperadas:

```txt
Backend HTTP: http://localhost:5213
Swagger:      http://localhost:5213/swagger
HTTPS:        https://localhost:7137
```

## Frontend relacionado

Repositorio:

```txt
https://github.com/wen-27/.netFrontend.git
```

Ruta local usada en desarrollo:

```txt
/Users/wen/.netFrontend-1
```

Comandos:

```bash
cd /Users/wen/.netFrontend-1
pnpm run dev
```

URL local:

```txt
http://localhost:5173
```

El backend permite CORS desde `http://localhost:5173` en desarrollo. El frontend puede apuntar a:

```env
VITE_API_URL=https://localhost:7137
# alternativa:
VITE_API_URL=http://localhost:5213
```

## Base de datos y migraciones

Actualizar base:

```bash
dotnet ef database update --project Infrastructure/Infrastructure.csproj --startup-project Api/Api.csproj
```

Crear migracion:

```bash
dotnet ef migrations add NombreDeLaMigracion --project Infrastructure/Infrastructure.csproj --startup-project Api/Api.csproj
```

Reiniciar base local en desarrollo:

```bash
dotnet ef database drop --project Infrastructure/Infrastructure.csproj --startup-project Api/Api.csproj
dotnet ef database update --project Infrastructure/Infrastructure.csproj --startup-project Api/Api.csproj
dotnet run --project Api/Api.csproj
```

`database drop` elimina la base local completa. Usarlo solo en desarrollo.

## Seeders

Los seeders se ejecutan automaticamente en ambiente `Development` desde `Program.cs`.

Seeder principal:

```txt
Infrastructure/Seeders/DevelopmentDataSeeder.cs
```

Responsabilidades:

- Crear roles principales.
- Crear usuarios de prueba y reparar passwords seed.
- Crear clientes, mecanicos y personal operativo.
- Crear vehiculos, ordenes, diagnosticos, facturas y pagos de prueba.
- Crear catalogos base y datos de inventario.
- Crear datos necesarios para pruebas smoke de endpoints.

La contrasena principal de desarrollo es:

```txt
DevPass123!
```

En bases locales antiguas tambien puede aceptarse:

```txt
Password123*
```

## Roles

| Rol backend | Descripcion |
| --- | --- |
| `Admin` | Administrador general del sistema. |
| `Receptionist` | Gestiona recepcion, clientes, vehiculos, pagos y entregas. |
| `Mechanic` | Atiende ordenes asignadas, diagnosticos y solicitudes. |
| `Client` | Consulta sus ordenes, aprobaciones, mensajes y pagos. |
| `WorkshopChief` | Aprueba diagnosticos y solicitudes tecnicas. |
| `WarehouseChief` | Gestiona bodega, productos y solicitudes de stock. |
| `InventoryManager` | Aprueba inventario, stock oficial e historial. |

El backend usa estos nombres exactos para JWT y autorizacion.

## Credenciales de prueba

Estas cuentas son solo para ambiente `Development`.

### Paneles principales

| Panel | Rol | Email | Password | Ruta sugerida |
| --- | --- | --- | --- | --- |
| Administrador | `Admin` | `admin@autotaller.com` | `DevPass123!` | `/dashboard/admin` |
| Recepcionista | `Receptionist` | `recepcionista@autotaller.com` | `DevPass123!` | `/dashboard/reception` |
| Jefe de mecanicos | `WorkshopChief` | `jefe.mecanicos@autotaller.com` | `DevPass123!` | `/dashboard/workshop-chief` |
| Cliente Carlos | `Client` | `carlos.ramirez@test.com` | `DevPass123!` | `/dashboard/client` |
| Cliente Laura | `Client` | `laura.gomez@test.com` | `DevPass123!` | `/dashboard/client` |

### Mecanicos

| Panel | Rol | Especialidad | Email | Password | Ruta sugerida |
| --- | --- | --- | --- | --- | --- |
| Mecanico general | `Mechanic` | General / diagnostico | `mecanico@autotaller.com` | `DevPass123!` | `/dashboard/mechanic` |
| Mecanico de diagnostico | `Mechanic` | Diagnostico | `diagnostico@autotaller.com` | `DevPass123!` | `/dashboard/mechanic` |
| Mecanico de mantenimiento | `Mechanic` | Mantenimiento | `mantenimiento@autotaller.com` | `DevPass123!` | `/mechanic/maintenance` |
| Mecanico electricista | `Mechanic` | Electricista | `electricista@autotaller.com` | `DevPass123!` | `/mechanic/electricity` |
| Mecanico de frenos | `Mechanic` | Frenos | `frenos@autotaller.com` | `DevPass123!` | `/mechanic/brakes` |

### Stock e inventario

| Panel | Rol | Email | Password | Ruta sugerida |
| --- | --- | --- | --- | --- |
| Jefe de stock / bodega | `WarehouseChief` | `jefebodega@autotaller.com` | `DevPass123!` | `/dashboard/warehouse-chief` o `/stock-manager` |
| Jefe de inventario / almacen | `InventoryManager` | `jefealmacen@autotaller.com` | `DevPass123!` | `/dashboard/inventory-manager` o `/inventory-manager` |

### Credenciales legacy

Estas cuentas pueden existir en bases locales antiguas:

| Rol | Email | Password |
| --- | --- | --- |
| Admin legacy | `admin@mail.com` | `Password123*` |
| Mechanic legacy | `mechanic@mail.com` | `Password123*` |
| Receptionist legacy | `receptionist@mail.com` | `Password123*` |
| Client legacy | `client@mail.com` | `Password123*` |

## Login y Swagger

Endpoint:

```txt
POST /api/auth/login
```

Body recomendado:

```json
{
  "email": "admin@autotaller.com",
  "password": "DevPass123!"
}
```

Para probar endpoints protegidos:

1. Abrir `http://localhost:5213/swagger`.
2. Ejecutar login.
3. Copiar el `accessToken`.
4. Pulsar `Authorize`.
5. Pegar el token como Bearer.

Si recibes `401`, falta token o expiro. Si recibes `403`, el rol no tiene permiso. Si recibes `429`, se activo el rate limit.

## Seguridad

El backend usa:

- JWT Bearer Authentication.
- Politicas de autorizacion por rol.
- Validacion de `Jwt:Key` con minimo 32 caracteres.
- `ExceptionHandlingMiddleware` para errores consistentes.
- `AuditMiddleware` para registrar acciones.
- Rate limiting.
- CORS restringido al frontend local en desarrollo.

Archivos importantes:

```txt
Api/Extensions/AuthServiceExtensions.cs
Api/Security/JwtTokenService.cs
Api/Middleware/ExceptionHandlingMiddleware.cs
Api/Middleware/AuditMiddleware.cs
Api/Extensions/CorsServiceExtensions.cs
Api/Extensions/RateLimitExtension.cs
```

## Rate limit

Politicas actuales:

| Politica | Endpoint/controlador | Limite |
| --- | --- | --- |
| `parts` | `/api/parts` | 15 peticiones por minuto |
| `service-orders` | `/api/serviceorders` | 15 peticiones por minuto |
| Global por IP | Todos los endpoints | 15 peticiones por minuto |

Respuesta esperada al superar el limite:

```txt
429 Too Many Requests
```

La vista frontend `/rate-limit-test` sirve para validar visualmente el bloqueo con usuario `Admin`.

## Flujos implementados

### Solicitudes tecnicas adicionales

El mecanico asignado crea una solicitud adicional para una orden. La solicitud inicia en `PendingWorkshopChiefApproval`. El Jefe de Taller puede aprobarla y enviarla al cliente como `PendingClientApproval`, o rechazarla como `RejectedByWorkshopChief`. El cliente dueno de la orden puede aprobar o rechazar. Al aprobar, el backend valida stock, anade el servicio o repuesto a la orden y pasa la solicitud a `AddedToOrder`.

### Aprobacion del cliente

El cliente solo puede consultar sus propias ordenes, aprobaciones y mensajes. Las solicitudes rechazadas por el Jefe de Taller no se exponen al cliente.

### Bodega y almacen

El Jefe de Bodega crea `StockSubmission`, calcula precio de venta y envia a revision. El Jefe de Almacen aprueba o rechaza. Al aprobar, se crea o actualiza el repuesto oficial (`Part`) y se registra historial de inventario.

### Inventario oficial

El inventario oficial solo cambia cuando `InventoryManager` aprueba una solicitud de stock. El flujo registra la decision y el movimiento en `InventoryHistory`.

### Pagos por recepcion

El cliente registra pagos de facturas propias. El pago queda en `PendingReceptionVerification`. `Receptionist` o `Admin` aprueba o rechaza. Al aprobar se actualiza factura, orden y fecha de entrega.

## Formulas de negocio

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

## Endpoints principales

Los endpoints protegidos requieren:

```http
Content-Type: application/json
Authorization: Bearer {accessToken}
```

La mayoria de listados usan:

```txt
?pageNumber=1&pageSize=10&search=texto
```

### Auth

```txt
POST /api/auth/login
POST /api/auth/register-client
```

### Admin

```txt
GET  /api/admin/dashboard
POST /api/admin/users
```

### Mecanico

```txt
GET  /api/mechanic/orders
GET  /api/mechanic/orders/{orderId}
GET  /api/mechanic/requests
POST /api/mechanic/orders/{orderId}/additional-requests
POST /api/mechanic/orders/{orderId}/work
```

### Jefe de taller

```txt
GET  /api/workshop-chief/requests
GET  /api/workshop-chief/requests/{requestId}
POST /api/workshop-chief/requests/{requestId}/approve
POST /api/workshop-chief/requests/{requestId}/reject
GET  /api/workshop-chief/diagnostics
POST /api/workshop-chief/diagnostics/{diagnosticId}/approve
POST /api/workshop-chief/diagnostics/{diagnosticId}/reject
```

### Servicios del taller

```txt
GET   /api/workshop-services
GET   /api/workshop-services/{id}
POST  /api/workshop-services
PUT   /api/workshop-services/{id}
PATCH /api/workshop-services/{id}/activate
PATCH /api/workshop-services/{id}/deactivate
```

### Cliente

```txt
GET  /api/client/orders
GET  /api/client/orders/{orderId}
GET  /api/client/approvals
POST /api/client/approvals/{requestId}/approve
POST /api/client/approvals/{requestId}/reject
GET  /api/client/messages
POST /api/client/payments
```

### Recepcion

```txt
GET  /api/reception/payments/pending-verification
GET  /api/reception/payments/{paymentId}
POST /api/reception/payments/{paymentId}/approve
POST /api/reception/payments/{paymentId}/reject
POST /api/reception/orders/{orderId}/confirm-delivery-date
```

### Bodega

```txt
GET  /api/warehouse/products
POST /api/warehouse/products
PUT  /api/warehouse/products/{id}
GET  /api/warehouse/stock-submissions
GET  /api/warehouse/stock-submissions/{id}
POST /api/warehouse/stock-submissions
POST /api/warehouse/stock-submissions/{id}/send-to-review
```

### Inventario

```txt
GET  /api/inventory/review-requests
GET  /api/inventory/review-requests/{id}
POST /api/inventory/review-requests/{id}/approve
POST /api/inventory/review-requests/{id}/reject
GET  /api/inventory/products
GET  /api/inventory/history
```

### Stock

```txt
GET /api/stock/dashboard
GET /api/stock/parts
GET /api/stock/parts/{id}/movements
```

### Personas y contactos

```txt
GET    /api/persons?pageNumber=1&pageSize=10&search=wendy
GET    /api/persons/{id}
POST   /api/persons
PUT    /api/persons/{id}
DELETE /api/persons/{id}

GET  /api/personemails?pageNumber=1&pageSize=10&search=mail
GET  /api/personemails/{id}
POST /api/personemails
PUT  /api/personemails/{id}

GET  /api/personphones?pageNumber=1&pageSize=10&search=300
GET  /api/personphones/{id}
POST /api/personphones
PUT  /api/personphones/{id}
```

### Vehiculos y propietarios

```txt
GET    /api/vehicles?pageNumber=1&pageSize=10
GET    /api/vehicles?pageNumber=1&pageSize=10&vin=ABC123&clientPersonId=1
GET    /api/vehicles/{id}
POST   /api/vehicles
PUT    /api/vehicles/{id}
DELETE /api/vehicles/{id}

GET   /api/vehicleownerhistory?pageNumber=1&pageSize=10
GET   /api/vehicleownerhistory/{id}
POST  /api/vehicleownerhistory
PATCH /api/vehicleownerhistory/{vehicleId}/end
```

### Ordenes, servicios y asignaciones

```txt
GET   /api/serviceorders?pageNumber=1&pageSize=10
GET   /api/serviceorders?pageNumber=1&pageSize=10&clientPersonId=1&vin=ABC123&fromDate=2026-01-01&toDate=2026-12-31&statusId=1&mechanicPersonId=2
GET   /api/serviceorders/{id}
POST  /api/serviceorders
PATCH /api/serviceorders/{id}/work
PATCH /api/serviceorders/{id}/status

GET    /api/orderservices?pageNumber=1&pageSize=10
GET    /api/orderservices/{id}
POST   /api/orderservices
PUT    /api/orderservices/{id}
DELETE /api/orderservices/{id}

GET  /api/mechanicassignments?pageNumber=1&pageSize=10
GET  /api/mechanicassignments/{id}
POST /api/mechanicassignments
```

### Repuestos e inventario

```txt
GET  /api/parts?pageNumber=1&pageSize=10&search=filtro
GET  /api/parts/{id}
POST /api/parts
PUT  /api/parts/{id}

GET    /api/orderserviceparts?pageNumber=1&pageSize=10
GET    /api/orderserviceparts/{id}
POST   /api/orderserviceparts
PUT    /api/orderserviceparts/{id}
DELETE /api/orderserviceparts/{id}
```

### Facturacion y pagos

```txt
GET  /api/invoices?pageNumber=1&pageSize=10&search=INV
GET  /api/invoices/{id}
POST /api/invoices

GET /api/invoicedetails?pageNumber=1&pageSize=10
GET /api/invoicedetails/{id}

GET    /api/payments?pageNumber=1&pageSize=10
GET    /api/payments/{id}
POST   /api/payments
PUT    /api/payments/{id}
DELETE /api/payments/{id}

GET    /api/paymentcards?pageNumber=1&pageSize=10
GET    /api/paymentcards/{id}
POST   /api/paymentcards
PUT    /api/paymentcards/{id}
DELETE /api/paymentcards/{id}
```

### Usuarios, roles y auditoria

```txt
GET   /api/users?pageNumber=1&pageSize=10
GET   /api/users/{id}
POST  /api/users
PATCH /api/users/{id}/status

GET  /api/roles?pageNumber=1&pageSize=10
GET  /api/roles/{id}
POST /api/roles
PUT  /api/roles/{id}

GET    /api/userroles?pageNumber=1&pageSize=10
GET    /api/userroles/{userId}/{roleId}
POST   /api/userroles
DELETE /api/userroles?userId=1&roleId=1

GET  /api/audits?pageNumber=1&pageSize=10
GET  /api/audits/{id}
POST /api/audits
```

### Catalogos para selects

```txt
GET /api/cardtypes?pageNumber=1&pageSize=50
GET /api/cities?pageNumber=1&pageSize=50
GET /api/departments?pageNumber=1&pageSize=50
GET /api/documenttypes?pageNumber=1&pageSize=50
GET /api/genders?pageNumber=1&pageSize=50
GET /api/countries?pageNumber=1&pageSize=50
GET /api/invoicestatuses?pageNumber=1&pageSize=50
GET /api/mechanicspecialties?pageNumber=1&pageSize=50
GET /api/orderstatuses?pageNumber=1&pageSize=50
GET /api/partbrands?pageNumber=1&pageSize=50
GET /api/paymentmethods?pageNumber=1&pageSize=50
GET /api/paymentstatuses?pageNumber=1&pageSize=50
GET /api/servicetypes?pageNumber=1&pageSize=50
GET /api/vehicletypes?pageNumber=1&pageSize=50
```

Cada uno soporta tambien:

```txt
GET /api/{resource}/{id}
```

### CRUD generico Admin

Estos recursos siguen el patron `GET`, `GET /{id}`, `POST`, `PUT /{id}` y `DELETE /{id}` cuando aplica:

```txt
addresses
countries
genders
mechanicspecialties
mechanicspecialtyassignments
neighborhoods
orderservices
personroles
streettypes
suppliers
vehicleentryinventory
```

## Pruebas smoke de endpoints

Para validar endpoints manualmente se puede usar Swagger o un cliente HTTP. La estrategia de pruebas smoke queda resumida asi:

1. Levantar MySQL.
2. Ejecutar migraciones.
3. Levantar backend en `Development`.
4. Hacer login con cada rol seed necesario.
5. Probar listados principales con token `Admin`.
6. Probar detalles por `id` con datos seed existentes.
7. Probar endpoints protegidos con roles correctos.
8. Confirmar que un cliente no pueda entrar a endpoints internos y reciba `403`.
9. Probar filtros avanzados en vehiculos y ordenes.
10. Probar flujos operativos por rol: mecanico, jefe de taller, cliente, recepcion, bodega e inventario.

Conteo minimo sugerido para smoke test:

- Auth: 2 endpoints.
- Listados CRUD/catalogos: 45+ endpoints.
- Detalles por id: 35+ endpoints.
- Flujos operativos: 30+ endpoints.
- Seguridad negativa: al menos 1 prueba `403`.

## Validacion

Backend:

```bash
dotnet build
```

Resultado esperado:

```txt
0 Advertencia(s)
0 Errores
```

Frontend, si se modifico:

```bash
cd /Users/wen/.netFrontend-1
pnpm run build
```

## Notas importantes

- MySQL debe estar encendido antes de levantar el backend.
- Al correr en `Development`, `DevelopmentDataSeeder` vuelve a crear o reparar datos iniciales.
- Los seeders deben ser idempotentes para evitar duplicados.
- Si se cambia `Jwt:Key`, los tokens emitidos antes dejan de servir.
- Si una cuenta entra por Swagger pero no por frontend, borrar token o sesion del navegador.
- Si CORS falla, revisar `Api/appsettings.Development.json`.
- Si el backend parece quedarse esperando al arrancar, revisar primero conexion a MySQL y secretos locales.
