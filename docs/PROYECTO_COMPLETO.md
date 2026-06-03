# 🚗 AutoTallerManager - Documentacion completa del proyecto

Este documento resume el proyecto `AutoTallerManager`, su arquitectura, configuracion local, credenciales de prueba, modulos principales, comandos de ejecucion y puntos importantes para desarrollo.

> Importante: este archivo incluye credenciales de usuarios seed de desarrollo. No incluye secretos reales como contraseña local de MySQL ni clave JWT privada. Esos valores deben mantenerse en `dotnet user-secrets`, variables de entorno o un secret manager.

## 1. 📌 Descripcion general

`AutoTallerManager` es un sistema para administrar la operacion de un taller automotriz. El backend esta construido con ASP.NET Core, Entity Framework Core, MySQL, JWT, MediatR, FluentValidation y una separacion por capas.

El sistema permite gestionar:

- Usuarios, roles y autenticacion.
- Clientes, mecanicos, recepcionistas, jefes de taller, bodega e inventario.
- Vehiculos y propietarios.
- Ordenes de servicio.
- Diagnosticos mecanicos.
- Servicios del taller.
- Repuestos, stock e inventario.
- Solicitudes adicionales durante una orden.
- Facturas y pagos.
- Auditoria de acciones.
- Catalogos base del sistema.

## 2. 🧱 Estructura del backend

```txt
.NetProyecto-1/
├── Api/
├── Application/
├── Domain/
├── Infrastructure/
├── docs/
├── README.md
└── README-CREDENCIALES.md
```

### Api

Capa de entrada HTTP. Contiene:

- `Program.cs`: arranque de la API, registro de servicios y middlewares.
- `Controllers/`: endpoints REST.
- `DTOs/`: contratos de entrada y salida para la API.
- `Extensions/`: configuraciones de Swagger, JWT, CORS, Mapster y rate limit.
- `Middleware/`: manejo global de errores y auditoria.
- `Security/`: generacion de tokens JWT.

### Application

Capa de casos de uso y reglas de aplicacion. Contiene:

- `UseCase/`: comandos, consultas, handlers y validadores.
- `Abstractions/`: contratos de repositorios y servicios.
- `DTOs/`: modelos internos para flujos operativos.
- `Common/`: resultados, excepciones, paginacion y comportamientos.

### Domain

Capa de dominio. Contiene:

- `Entities/`: entidades principales del sistema.
- `ValueObjects/`: validaciones y valores encapsulados.
- `Enums/`: estados y categorias permitidas.
- `Constants/`: nombres constantes compartidos.
- `Common/BaseEntity.cs`: propiedades comunes de entidades.

### Infrastructure

Capa tecnica y persistencia. Contiene:

- `Context/AppDbContext.cs`: DbContext principal.
- `Configurations/`: mapeos de EF Core.
- `Repositories/`: implementaciones de repositorios.
- `Services/`: servicios operativos con persistencia.
- `Seeders/`: datos iniciales de desarrollo.
- `UnitOfWork/`: confirmacion centralizada de cambios.
- `Migrations/`: migraciones EF Core generadas.

## 3. 🖥️ Frontend relacionado

El frontend usado durante las pruebas esta en:

```txt
/Users/wen/.netFrontend-1
```

Repositorio del frontend:

```txt
https://github.com/wen-27/.netFrontend.git
```

Link directo:

[wen-27/.netFrontend.git](https://github.com/wen-27/.netFrontend.git)

Comando principal:

```bash
cd /Users/wen/.netFrontend-1
pnpm run dev
```

URL local esperada:

```txt
http://localhost:5173
```

El backend permite CORS desde esa URL en `Api/appsettings.Development.json`.

## 4. 🧰 Requisitos locales

Para ejecutar el proyecto se necesita:

- .NET SDK compatible con `net10.0`.
- MySQL local.
- `dotnet-ef` para migraciones.
- Node.js y `pnpm` para el frontend.

Verificar MySQL:

```bash
/usr/local/mysql/bin/mysqladmin --protocol=tcp --host=127.0.0.1 --port=3306 --user=root --password=TU_PASSWORD ping
```

Instalar o actualizar `dotnet-ef`:

```bash
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
```

## 5. 🔐 Configuracion segura

`Api/appsettings.json` no debe contener secretos reales. La cadena de conexion y la clave JWT deben configurarse localmente con `user-secrets` o variables de entorno.

### User-secrets recomendado

```bash
cd /Users/wen/.NetProyecto-1/Api

dotnet user-secrets set "ConnectionStrings:MySql" "server=127.0.0.1;port=3306;database=AutoTallerManager;user=TU_USUARIO;password=TU_PASSWORD;Connection Timeout=10;Default Command Timeout=30;"

dotnet user-secrets set "Jwt:Key" "TU_CLAVE_LOCAL_DE_MINIMO_32_CARACTERES"
```

### Variables de entorno alternativas

```bash
export ConnectionStrings__MySql="server=127.0.0.1;port=3306;database=AutoTallerManager;user=TU_USUARIO;password=TU_PASSWORD;"
export Jwt__Key="TU_CLAVE_LOCAL_DE_MINIMO_32_CARACTERES"
```

### JWT configurado en appsettings

`Api/appsettings.json` conserva valores no secretos:

- `Jwt:Issuer`
- `Jwt:Audience`
- `Jwt:ExpiresMinutes`

La clave real `Jwt:Key` debe venir de secretos locales o entorno.

## 6. ▶️ Comandos de ejecucion

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

## 7. 🗄️ Migraciones y base de datos

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

> `database drop` elimina datos locales. Usarlo solo en desarrollo.

## 8. 🌱 Seeders

Los seeders se ejecutan automaticamente en ambiente `Development` desde `Program.cs`.

Seeder principal:

```txt
Infrastructure/Seeders/DevelopmentDataSeeder.cs
```

Responsabilidades:

- Crear roles principales.
- Crear usuarios de prueba.
- Reparar passwords seed en desarrollo.
- Crear clientes, mecanicos y personal operativo.
- Crear vehiculos y ordenes de prueba.
- Crear catalogos base.
- Crear escenarios de inventario, stock, facturacion y pagos.
- Crear datos necesarios para pruebas smoke de endpoints.

La contraseña principal de los usuarios seed es:

```txt
DevPass123!
```

En desarrollo tambien se acepta la contraseña legacy:

```txt
Password123*
```

## 9. 👥 Roles del sistema

| Rol backend | Descripcion |
| --- | --- |
| `Admin` | Administrador general del sistema. |
| `Receptionist` | Gestiona recepcion, clientes, vehiculos, pagos y entregas. |
| `Mechanic` | Atiende ordenes asignadas, diagnosticos y solicitudes. |
| `Client` | Consulta sus ordenes, aprobaciones, mensajes y pagos. |
| `WorkshopChief` | Aprueba diagnosticos y solicitudes tecnicas. |
| `WarehouseChief` | Gestiona bodega, productos y solicitudes de stock. |
| `InventoryManager` | Aprueba inventario, stock oficial e historial. |

## 10. 🔑 Credenciales de prueba

Estas cuentas son para ambiente `Development`.

### Paneles principales

| Panel | Rol | Email | Password | Ruta sugerida |
| --- | --- | --- | --- | --- |
| Administrador | `Admin` | `admin@autotaller.com` | `DevPass123!` | `/dashboard/admin` |
| Recepcionista | `Receptionist` | `recepcionista@autotaller.com` | `DevPass123!` | `/dashboard/reception` |
| Jefe de mecanicos | `WorkshopChief` | `jefe.mecanicos@autotaller.com` | `DevPass123!` | `/dashboard/workshop-chief` |
| Cliente Carlos | `Client` | `carlos.ramirez@test.com` | `DevPass123!` | `/dashboard/client` |
| Cliente Laura | `Client` | `laura.gomez@test.com` | `DevPass123!` | `/dashboard/client` |

### Paneles de mecanicos

| Panel | Rol | Especialidad | Email | Password | Ruta sugerida |
| --- | --- | --- | --- | --- | --- |
| Mecanico general | `Mechanic` | General / diagnostico | `mecanico@autotaller.com` | `DevPass123!` | `/dashboard/mechanic` |
| Mecanico de diagnostico | `Mechanic` | Diagnostico | `diagnostico@autotaller.com` | `DevPass123!` | `/dashboard/mechanic` |
| Mecanico de mantenimiento | `Mechanic` | Mantenimiento | `mantenimiento@autotaller.com` | `DevPass123!` | `/mechanic/maintenance` |
| Mecanico electricista | `Mechanic` | Electricista | `electricista@autotaller.com` | `DevPass123!` | `/mechanic/electricity` |
| Mecanico de frenos | `Mechanic` | Frenos | `frenos@autotaller.com` | `DevPass123!` | `/mechanic/brakes` |

### Paneles de stock e inventario

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

## 11. 🧪 Login en Swagger

Endpoint:

```txt
POST /api/auth/login
```

Body:

```json
{
  "email": "admin@autotaller.com",
  "password": "DevPass123!"
}
```

Swagger devuelve un token JWT. Para probar endpoints protegidos:

1. Abrir `http://localhost:5213/swagger`.
2. Ejecutar login.
3. Copiar el token.
4. Pulsar `Authorize`.
5. Pegar el token como Bearer.

## 12. 🛡️ Seguridad

El backend usa:

- JWT Bearer Authentication.
- Politicas de autorizacion por rol.
- Validacion de clave JWT minima de 32 caracteres.
- `ExceptionHandlingMiddleware` para errores consistentes.
- `AuditMiddleware` para registrar acciones.
- Rate limiting.
- CORS restringido a frontend local en desarrollo.

Archivos importantes:

```txt
Api/Extensions/AuthServiceExtensions.cs
Api/Security/JwtTokenService.cs
Api/Middleware/ExceptionHandlingMiddleware.cs
Api/Middleware/AuditMiddleware.cs
Api/Extensions/CorsServiceExtensions.cs
Api/Extensions/RateLimitExtension.cs
```

## 13. 🚦 Rate limit

Archivo:

```txt
Api/Extensions/RateLimitExtension.cs
```

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

Para cambiar los limites, editar:

```csharp
limiter.PermitLimit = 15;
limiter.Window = TimeSpan.FromMinutes(1);
```

## 14. 🧭 Modulos funcionales

### Administracion

- Dashboard administrativo.
- Usuarios.
- Roles.
- Auditoria.
- Catalogos.

### Recepcion

- Registro de clientes.
- Registro de vehiculos.
- Verificacion de pagos.
- Confirmacion de entrega.

### Mecanicos

- Ordenes asignadas.
- Diagnosticos.
- Solicitudes adicionales.
- Registro de trabajo realizado.

### Jefe de taller

- Revision de solicitudes de mecanicos.
- Aprobacion o rechazo de diagnosticos.
- Gestion de servicios del taller.
- Seguimiento de ordenes.

### Cliente

- Consulta de ordenes propias.
- Aprobacion de solicitudes.
- Registro de pagos.
- Historial y mensajes.

### Bodega

- Productos operativos.
- Solicitudes de stock.
- Envio a revision de inventario.

### Inventario

- Revision de solicitudes de stock.
- Aprobacion o rechazo.
- Inventario oficial.
- Historial de movimientos.

### Facturacion y pagos

- Generacion de facturas.
- Registro de pagos.
- Estados de factura y pago.
- Verificacion por recepcion.

## 15. 🌐 Endpoints principales por flujo

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

## 16. 📚 Catalogos y CRUD generales

El proyecto tiene controladores para catalogos y entidades base:

- Addresses
- Audits
- AuditActionTypes
- CardTypes
- Cities
- Countries
- Departments
- DocumentTypes
- EmailDomains
- Genders
- InvoiceDetails
- Invoices
- InvoiceStatuses
- MechanicAssignments
- MechanicSpecialties
- MechanicSpecialtyAssignments
- Neighborhoods
- OrderServices
- OrderServiceParts
- OrderStatuses
- OrderStatusHistory
- Parts
- PartBrands
- PartCategories
- PartPurchases
- PartPurchaseDetails
- PaymentCards
- PaymentMethods
- Payments
- PaymentStatuses
- PersonEmails
- PersonPhones
- PersonRoles
- Persons
- Roles
- ServiceOrders
- ServiceTypes
- StreetTypes
- Suppliers
- Users
- UserRoles
- VehicleBrands
- VehicleEntryInventory
- VehicleModels
- VehicleOwnerHistory
- Vehicles
- VehicleTypes

Muchos de estos controladores usan patrones comunes con MediatR y repositorios.

## 17. 🧮 Formulas de negocio

### Precio de producto

```txt
Precio venta = precio proveedor + (precio proveedor * porcentaje ganancia / 100)
```

### Precio de servicio

```txt
Subtotal repuestos = suma(precio venta repuesto * cantidad requerida)
Valor mano de obra = subtotal repuestos * porcentaje mano de obra / 100
Precio final = subtotal repuestos + valor mano de obra
```

## 18. ✅ Pruebas y verificacion

### Build backend

```bash
dotnet build
```

Resultado esperado:

```txt
0 Advertencia(s)
0 Errores
```

### Build frontend

```bash
cd /Users/wen/.netFrontend-1
pnpm run build
```

### Swagger

```txt
http://localhost:5213/swagger
```

### Prueba de rate limit desde frontend

Vista agregada:

```txt
/rate-limit-test
```

Disponible para usuario `Admin`.

## 19. 🚨 Archivos sensibles y buenas practicas

No guardar en Git:

- Password real de MySQL.
- `Jwt:Key` real.
- Tokens JWT generados.
- Dumps de base de datos con datos reales.
- Logs con informacion sensible.

Usar:

- `dotnet user-secrets` en local.
- Variables de entorno en despliegue.
- Secret manager en produccion.

## 20. ⚡ Comandos utiles rapidos

```bash
# Backend
cd /Users/wen/.NetProyecto-1
dotnet build
dotnet run --project Api/Api.csproj

# Frontend
cd /Users/wen/.netFrontend-1
pnpm run dev

# Swagger
open http://localhost:5213/swagger
```

## 21. 🧠 Importante a tener en cuenta

### 🔐 Seguridad y secretos

- No volver a poner credenciales reales en `Api/appsettings.json`.
- `ConnectionStrings:MySql` debe vivir en `dotnet user-secrets`, variable de entorno o secret manager.
- `Jwt:Key` debe tener minimo 32 caracteres y tampoco debe guardarse en Git.
- Las passwords `DevPass123!` y `Password123*` son solo para datos seed de desarrollo.
- Si se cambia la clave JWT, los tokens emitidos antes dejan de servir.

### 🗄️ Base de datos

- MySQL debe estar encendido antes de ejecutar el backend.
- En macOS es mas estable usar `127.0.0.1` en vez de `localhost` en la cadena de conexion.
- `dotnet ef database drop` borra la base local completa.
- Si cambian entidades o configuraciones EF Core, probablemente se necesita nueva migracion.
- Los seeders deben seguir siendo idempotentes para poder ejecutar `dotnet run` varias veces sin duplicar datos.

### 🌱 Seeders y credenciales

- Los usuarios seed se crean automaticamente en ambiente `Development`.
- Si una cuenta ya existe, el seeder puede reparar datos necesarios sin duplicarla.
- El login en desarrollo acepta `Password123*` para cuentas legacy y puede reparar hashes antiguos.
- Si una cuenta no entra desde el frontend pero si desde Swagger, limpiar token/sesion del navegador.

### ▶️ Arranque correcto

- Primero debe estar activo MySQL.
- Luego levantar backend en `http://localhost:5213`.
- Despues levantar frontend en `http://localhost:5173`.
- Swagger vive en `http://localhost:5213/swagger`.
- Si el backend parece quedarse esperando al arrancar, revisar primero conexion a MySQL y user-secrets.

### 🧪 Swagger y JWT

- Para probar endpoints protegidos hay que hacer login y usar `Authorize`.
- El token debe ir como Bearer token.
- Si un endpoint devuelve `401`, falta token o el token expiro.
- Si devuelve `403`, el usuario esta autenticado pero no tiene el rol correcto.
- Si devuelve `429`, se activo el rate limit.

### 🚦 Rate limit

- El rate limit actual esta en `Api/Extensions/RateLimitExtension.cs`.
- Si se prueba muchas veces desde Swagger o frontend, se puede bloquear temporalmente la IP local.
- Para repetir una prueba limpia hay que esperar a que termine la ventana del limitador.
- La vista frontend `/rate-limit-test` sirve para validar visualmente el bloqueo.

### 🧩 Frontend

- El frontend relacionado esta en `/Users/wen/.netFrontend-1`.
- Repo frontend: [wen-27/.netFrontend.git](https://github.com/wen-27/.netFrontend.git).
- El frontend usa proxy hacia `/api` apuntando al backend local.
- Si el frontend no carga datos, revisar que el backend este vivo y que el token no este vencido.
- Si CORS falla, revisar `Api/appsettings.Development.json`.

### 🧾 Documentacion

- `README.md` resume el backend.
- `README-CREDENCIALES.md` lista usuarios de prueba.
- `docs/PROYECTO_COMPLETO.md` es el documento integral del proyecto.
- `docs/frontend-api-endpoints.md` contiene informacion para integracion frontend/backend.

### ✅ Validacion antes de entregar

Ejecutar siempre:

```bash
dotnet build
```

Y si se toco frontend:

```bash
cd /Users/wen/.netFrontend-1
pnpm run build
```

Resultado esperado backend:

```txt
0 Advertencia(s)
0 Errores
```

## 22. 📍 Estado actual conocido

- Backend compila correctamente.
- Se eliminaron secretos reales de `appsettings.json`.
- JWT exige clave minima de 32 caracteres.
- MySQL se recomienda por `127.0.0.1` para evitar problemas con `localhost`.
- Seeders crean datos de desarrollo.
- Rate limit esta activo.
- Frontend tiene vista `/rate-limit-test`.
- El backend tiene comentarios internos en clases, records, interfaces y enums.
- Frontend relacionado: [wen-27/.netFrontend.git](https://github.com/wen-27/.netFrontend.git).
