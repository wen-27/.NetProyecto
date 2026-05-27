# AutoTallerManager API para Frontend

## Base URL

En desarrollo el backend esta configurado asi:

```env
VITE_API_URL=https://localhost:7137
# alternativa si no usas HTTPS:
# VITE_API_URL=http://localhost:5213
```

El frontend en Vite es:

```txt
http://localhost:5173
```

El backend ya permite CORS desde `http://localhost:5173`.

## Headers

Endpoints publicos:

```http
Content-Type: application/json
```

Endpoints protegidos:

```http
Content-Type: application/json
Authorization: Bearer {accessToken}
```

La mayoria de listados usan:

```txt
?pageNumber=1&pageSize=10&search=texto
```

Los listados devuelven `X-Total-Count` en headers.

## Auth

## Usuarios demo

Cuando el backend corre en `Development`, se crean o reparan estos usuarios:

```txt
Admin:        admin@mail.com        / Password123*
Mechanic:     mechanic@mail.com     / Password123*
Receptionist: receptionist@mail.com / Password123*
Client:       client@mail.com       / Password123*
```

Tambien se siembran repuestos, marcas de repuestos, proveedor y una compra inicial para que el modulo de inventario tenga datos.

### Login

```http
POST /api/auth/login
```

Body:

```json
{
  "email": "admin@mail.com",
  "password": "Password123*"
}
```

Response:

```json
{
  "userId": 1,
  "personId": 1,
  "email": "admin@mail.com",
  "role": "Admin",
  "accessToken": "jwt",
  "expiresAt": "2026-05-27T23:00:00"
}
```

### Registro de cliente

```http
POST /api/auth/register-client
```

Body:

```json
{
  "documentTypeId": 1,
  "documentNumber": "1099999999",
  "firstName": "Cliente",
  "middleName": null,
  "lastName": "Prueba",
  "secondLastName": null,
  "birthDate": "1995-01-01",
  "genderId": 1,
  "addressId": null,
  "email": "cliente@mail.com",
  "password": "Password123*",
  "phoneCountryId": 1,
  "phoneNumber": "3001234567"
}
```

## Roles

- `Admin`: todo, configuracion, usuarios, roles, inventario, borrados.
- `Receptionist`: personas, contactos, vehiculos, propietarios, ordenes y asignacion de mecanicos.
- `Mechanic`: registrar trabajo, cambiar estado, repuestos de orden y facturas.
- `Client`: login/registro. No entra a endpoints internos.

## Endpoints principales del frontend

### Personas

```http
GET /api/persons?pageNumber=1&pageSize=10&search=wendy
GET /api/persons/{id}
POST /api/persons                 # Receptionist/Admin
PUT /api/persons/{id}             # Receptionist/Admin
DELETE /api/persons/{id}          # Admin
```

Create body:

```json
{
  "firstNames": "Wendy",
  "lastNames": "Perez"
}
```

Update body:

```json
{
  "documentTypeId": 1,
  "documentNumber": "1099999999",
  "firstName": "Wendy",
  "middleName": null,
  "lastName": "Perez",
  "secondLastName": null,
  "birthDate": "1995-01-01",
  "genderId": 1,
  "addressId": null
}
```

### Emails y telefonos de persona

```http
GET /api/personemails?pageNumber=1&pageSize=10&search=mail
GET /api/personemails/{id}
POST /api/personemails            # Receptionist/Admin
PUT /api/personemails/{id}        # Receptionist/Admin

GET /api/personphones?pageNumber=1&pageSize=10&search=300
GET /api/personphones/{id}
POST /api/personphones            # Receptionist/Admin
PUT /api/personphones/{id}        # Receptionist/Admin
```

Bodies:

```json
{
  "personId": 1,
  "emailDomainId": 1,
  "emailUser": "wendy",
  "isPrimary": true
}
```

```json
{
  "personId": 1,
  "countryId": 1,
  "phoneNumber": "3001234567",
  "isPrimary": true
}
```

## Vehiculos

```http
GET /api/vehicles?pageNumber=1&pageSize=10
GET /api/vehicles?pageNumber=1&pageSize=10&vin=ABC123&clientPersonId=1
GET /api/vehicles/{id}
POST /api/vehicles                # Receptionist/Admin
PUT /api/vehicles/{id}            # Receptionist/Admin
DELETE /api/vehicles/{id}         # Admin
```

Body:

```json
{
  "modelId": 1,
  "vehicleTypeId": 1,
  "vin": "1HGCM82633A004352",
  "year": 2026,
  "color": "Negro",
  "mileage": 10000,
  "isActive": true
}
```

### Historial de propietarios

```http
GET /api/vehicleownerhistory?pageNumber=1&pageSize=10
GET /api/vehicleownerhistory/{id}
POST /api/vehicleownerhistory             # Receptionist/Admin
PATCH /api/vehicleownerhistory/{vehicleId}/end # Receptionist/Admin
```

Bodies:

```json
{
  "vehicleId": 1,
  "personId": 1,
  "startDate": "2026-05-27"
}
```

```json
{
  "endDate": "2026-05-27T00:00:00"
}
```

## Ordenes de servicio

```http
GET /api/serviceorders?pageNumber=1&pageSize=10
GET /api/serviceorders?pageNumber=1&pageSize=10&clientPersonId=1&vin=ABC123&fromDate=2026-01-01&toDate=2026-12-31&statusId=1&mechanicPersonId=2
GET /api/serviceorders/{id}
POST /api/serviceorders                  # Receptionist/Admin
PATCH /api/serviceorders/{id}/work       # Mechanic/Admin
PATCH /api/serviceorders/{id}/status     # Mechanic/Admin
```

Create body:

```json
{
  "vehicleId": 1,
  "orderStatusId": 1,
  "estimatedDeliveryDate": "2026-05-30T10:00:00",
  "generalDescription": "Revision general"
}
```

Registrar trabajo:

```json
{
  "workPerformed": "Cambio de aceite y diagnostico completado."
}
```

Cambiar estado:

```json
{
  "orderStatusId": 2,
  "userId": 1,
  "observation": "Orden en proceso"
}
```

### Lineas de servicio

```http
GET /api/orderservices?pageNumber=1&pageSize=10
GET /api/orderservices/{id}
POST /api/orderservices          # Admin
PUT /api/orderservices/{id}      # Admin
DELETE /api/orderservices/{id}   # Admin
```

Body:

```json
{
  "serviceOrderId": 1,
  "serviceTypeId": 1,
  "description": "Diagnostico",
  "laborCost": 80000
}
```

### Asignar mecanico

```http
GET /api/mechanicassignments?pageNumber=1&pageSize=10
GET /api/mechanicassignments/{id}
POST /api/mechanicassignments     # Receptionist/Admin
```

Body:

```json
{
  "orderServiceId": 1,
  "mechanicPersonId": 2,
  "specialtyId": 1
}
```

## Repuestos e inventario

```http
GET /api/parts?pageNumber=1&pageSize=10&search=filtro
GET /api/parts/{id}
POST /api/parts                  # Admin
PUT /api/parts/{id}              # Admin
```

Body:

```json
{
  "partCategoryId": 1,
  "partBrandId": null,
  "code": "OIL-001",
  "description": "Aceite 10W30",
  "stock": 20,
  "minimumStock": 5,
  "unitPrice": 45000,
  "isActive": true
}
```

### Repuestos usados en una orden

```http
GET /api/orderserviceparts?pageNumber=1&pageSize=10
GET /api/orderserviceparts/{id}
POST /api/orderserviceparts       # Mechanic/Admin
PUT /api/orderserviceparts/{id}   # Mechanic/Admin
DELETE /api/orderserviceparts/{id} # Mechanic/Admin
```

Create body:

```json
{
  "orderServiceId": 1,
  "partId": 1,
  "quantity": 1,
  "appliedUnitPrice": 45000
}
```

Update body:

```json
{
  "quantity": 2,
  "appliedUnitPrice": 45000,
  "customerApproved": true,
  "approvalDate": "2026-05-27T10:00:00"
}
```

## Facturacion

```http
GET /api/invoices?pageNumber=1&pageSize=10&search=INV
GET /api/invoices/{id}
POST /api/invoices               # Mechanic/Admin

GET /api/invoicedetails?pageNumber=1&pageSize=10
GET /api/invoicedetails/{id}
```

Generar factura:

```json
{
  "serviceOrderId": 1,
  "invoiceStatusId": 1
}
```

## Pagos

```http
GET /api/payments?pageNumber=1&pageSize=10
GET /api/payments/{id}
POST /api/payments               # Admin
PUT /api/payments/{id}           # Admin
DELETE /api/payments/{id}        # Admin

GET /api/paymentcards?pageNumber=1&pageSize=10
GET /api/paymentcards/{id}
POST /api/paymentcards           # Admin
PUT /api/paymentcards/{id}       # Admin
DELETE /api/paymentcards/{id}    # Admin
```

Bodies:

```json
{
  "invoiceId": 1,
  "paymentMethodId": 1,
  "paymentStatusId": 1,
  "amount": 120000,
  "reference": "PAY-001"
}
```

```json
{
  "paymentId": 1,
  "cardTypeId": 1,
  "lastFourDigits": "1234",
  "cardHolder": "Wendy Perez",
  "authorizationCode": "AUTH-001"
}
```

## Usuarios, roles y seguridad

```http
GET /api/users?pageNumber=1&pageSize=10       # Admin
GET /api/users/{id}                           # Admin
POST /api/users                               # Admin
PATCH /api/users/{id}/status                  # Admin

GET /api/roles?pageNumber=1&pageSize=10       # Admin
GET /api/roles/{id}                           # Admin
POST /api/roles                               # Admin
PUT /api/roles/{id}                           # Admin

GET /api/userroles?pageNumber=1&pageSize=10   # Admin
GET /api/userroles/{userId}/{roleId}          # Admin
POST /api/userroles                           # Admin
DELETE /api/userroles?userId=1&roleId=1       # Admin
```

Bodies:

```json
{
  "personId": 1,
  "passwordHash": "hash"
}
```

```json
{
  "status": true
}
```

```json
{
  "roleName": "Admin"
}
```

```json
{
  "userId": 1,
  "roleId": 1
}
```

## Auditoria

```http
GET /api/audits?pageNumber=1&pageSize=10      # Admin
GET /api/audits/{id}                          # Admin
POST /api/audits                              # Admin

GET /api/auditactiontypes?pageNumber=1&pageSize=10 # Admin
GET /api/auditactiontypes/{id}                     # Admin
POST /api/auditactiontypes                         # Admin
PUT /api/auditactiontypes/{id}                     # Admin
```

Body auditoria manual:

```json
{
  "userId": 1,
  "auditActionTypeId": 1,
  "affectedEntity": "Vehicles",
  "affectedRecordId": 1,
  "description": "Prueba manual"
}
```

## Catalogos de solo consulta

Estos endpoints son para selects/autocompletados del frontend:

```http
GET /api/cardtypes?pageNumber=1&pageSize=50
GET /api/cities?pageNumber=1&pageSize=50
GET /api/departments?pageNumber=1&pageSize=50
GET /api/invoicestatuses?pageNumber=1&pageSize=50
GET /api/orderstatuses?pageNumber=1&pageSize=50
GET /api/partbrands?pageNumber=1&pageSize=50
GET /api/paymentmethods?pageNumber=1&pageSize=50
GET /api/paymentstatuses?pageNumber=1&pageSize=50
GET /api/vehicletypes?pageNumber=1&pageSize=50
GET /api/serviceTypes?pageNumber=1&pageSize=50
GET /api/mechanicspecialties?pageNumber=1&pageSize=50
GET /api/documenttypes?pageNumber=1&pageSize=50
GET /api/genders?pageNumber=1&pageSize=50
GET /api/countries?pageNumber=1&pageSize=50
```

Cada uno tambien soporta:

```http
GET /api/{resource}/{id}
```

## Catalogos CRUD generico Admin

Estos soportan:

```http
GET /api/{resource}?pageNumber=1&pageSize=10&search=x
GET /api/{resource}/{id}
POST /api/{resource}
PUT /api/{resource}/{id}
DELETE /api/{resource}/{id}
```

Resources:

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
