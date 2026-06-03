# Pruebas en Swagger

Base: `http://localhost:5213/swagger`

## 0. Preparacion

1. Ejecuta `POST /api/Auth/login`.

```json
{
  "email": "admin@autotaller.com",
  "password": "DevPass123!"
}
```

2. Copia `token`, pulsa `Authorize` y pega `Bearer TU_TOKEN`.
3. Para endpoints por rol, usa estas cuentas:

| Rol | Email | Password |
| --- | --- | --- |
| Admin | `admin@autotaller.com` | `DevPass123!` |
| Receptionist | `recepcionista@autotaller.com` | `DevPass123!` |
| WorkshopChief | `jefe.mecanicos@autotaller.com` | `DevPass123!` |
| Mechanic | `mecanico@autotaller.com` | `DevPass123!` |
| WarehouseChief | `jefebodega@autotaller.com` | `DevPass123!` |
| InventoryManager | `jefealmacen@autotaller.com` | `DevPass123!` |
| Client | `carlos.ramirez@test.com` | `DevPass123!` |

> Regla para probar: primero ejecuta el `GET`, toma un `id` real de la respuesta y luego prueba `GET /{id}`, `PUT`, `PATCH` o `DELETE`.

## 1. Auth

- `POST /api/Auth/login`: debe devolver token.
- `POST /api/Auth/register-client`:

```json
{
  "documentTypeId": 1,
  "documentNumber": "TEST-1001",
  "firstName": "Cliente",
  "lastName": "Swagger",
  "email": "cliente.swagger1001@test.com",
  "password": "DevPass123!",
  "phoneCountryId": 1,
  "phoneNumber": "3001234567"
}
```

## 2. Catalogos base, solo lectura primero

Prueba en este orden: `GET`, luego `GET /{id}` con un id devuelto.

- `/api/Countries`
- `/api/Departments`
- `/api/Cities`
- `/api/Genders`
- `/api/DocumentTypes`
- `/api/StreetTypes`
- `/api/Neighborhoods`
- `/api/EmailDomains`
- `/api/Roles`
- `/api/PersonRoles`
- `/api/UserRoles`
- `/api/VehicleTypes`
- `/api/VehicleBrands`
- `/api/VehicleModels`
- `/api/PartCategories`
- `/api/PartBrands`
- `/api/Parts`
- `/api/Suppliers`
- `/api/PaymentMethods`
- `/api/PaymentStatuses`
- `/api/CardTypes`
- `/api/InvoiceStatuses`
- `/api/OrderStatuses`
- `/api/ServiceTypes`
- `/api/MechanicSpecialties`
- `/api/MechanicSpecialtyAssignments`
- `/api/AuditActionTypes`
- `/api/Audits`

## 3. Catalogos editables como Admin

Usa nombres unicos para evitar conflictos.

- `POST /api/EmailDomains`

```json
{ "domain": "swagger-test.com" }
```

- `PUT /api/EmailDomains/{id}`

```json
{ "domain": "swagger-test-updated.com" }
```

- `POST /api/Roles`

```json
{ "roleName": "SwaggerRole" }
```

- `PUT /api/Roles/{id}`

```json
{ "roleName": "SwaggerRoleUpdated" }
```

- `POST /api/VehicleBrands`

```json
{ "brandName": "Swagger Motors" }
```

- `PUT /api/VehicleBrands/{id}`

```json
{ "brandName": "Swagger Motors Updated" }
```

- `POST /api/VehicleModels`

```json
{ "brandId": 1, "modelName": "Swagger Model" }
```

- `PUT /api/VehicleModels/{id}`

```json
{ "brandId": 1, "modelName": "Swagger Model Updated" }
```

- `POST /api/PartCategories`

```json
{ "name": "Categoria Swagger" }
```

- `PUT /api/PartCategories/{id}`

```json
{ "name": "Categoria Swagger Updated" }
```

- `POST /api/Parts`

```json
{
  "partCategoryId": 1,
  "partBrandId": 1,
  "code": "SWG-PART-001",
  "description": "Repuesto Swagger",
  "stock": 10,
  "minimumStock": 2,
  "unitPrice": 45000,
  "isActive": true
}
```

- `PUT /api/Parts/{id}`

```json
{
  "partCategoryId": 1,
  "partBrandId": 1,
  "code": "SWG-PART-001-UPD",
  "description": "Repuesto Swagger Updated",
  "stock": 12,
  "minimumStock": 3,
  "unitPrice": 50000,
  "isActive": true
}
```

- `POST /api/ServiceTypes`

```json
{ "name": "Servicio Swagger", "estimatedDays": 2 }
```

- `PUT /api/ServiceTypes/{id}`

```json
{ "name": "Servicio Swagger Updated", "estimatedDays": 3 }
```

## 4. Personas, contactos, usuarios y vehiculos

- `GET /api/Persons`
- `GET /api/Persons/{id}`
- `POST /api/Persons`

```json
{
  "documentTypeId": 1,
  "documentNumber": "SWG-2001",
  "firstName": "Persona",
  "middleName": null,
  "lastName": "Swagger",
  "secondLastName": null,
  "birthDate": "1995-01-15",
  "genderId": 1,
  "addressId": null
}
```

- `PUT /api/Persons/{id}`: usa el mismo body cambiando nombre/apellido.
- `POST /api/PersonEmails`

```json
{ "personId": 1, "emailDomainId": 1, "emailUser": "swagger.user", "isPrimary": true }
```

- `PUT /api/PersonEmails/{id}`

```json
{ "emailDomainId": 1, "emailUser": "swagger.user.updated", "isPrimary": true }
```

- `POST /api/PersonPhones`

```json
{ "personId": 1, "countryId": 1, "phoneNumber": "3001234567", "isPrimary": true }
```

- `PUT /api/PersonPhones/{id}`

```json
{ "countryId": 1, "phoneNumber": "3007654321", "isPrimary": true }
```

- `POST /api/Users`

```json
{ "personId": 1, "passwordHash": "DevPass123!" }
```

- `PATCH /api/Users/{id}/status`

```json
{ "status": true }
```

- `POST /api/Vehicles`

```json
{
  "modelId": 1,
  "vehicleTypeId": 1,
  "plate": "SWG001",
  "vin": "SWAGGERVIN000001",
  "year": 2020,
  "color": "Rojo",
  "mileage": 35000,
  "isActive": true
}
```

- `PUT /api/Vehicles/{id}`: usa el mismo body cambiando `color` o `mileage`.
- `DELETE /api/Vehicles/{id}` y `DELETE /api/Persons/{id}` solo si son registros de prueba.

## 5. Recepcion

Login recomendado: `recepcionista@autotaller.com`.

- `GET /api/reception/dashboard`
- `GET /api/reception/customers`
- `GET /api/reception/customers/{id}`
- `POST /api/reception/customers`

```json
{
  "documentTypeId": 1,
  "documentNumber": "REC-1001",
  "firstName": "Cliente",
  "middleName": null,
  "lastName": "Recepcion",
  "secondLastName": null,
  "email": "cliente.recepcion1001@test.com",
  "phoneCountryId": 1,
  "phone": "3015551234"
}
```

- `GET /api/reception/customers/{id}/vehicles`
- `GET /api/reception/vehicles`
- `GET /api/reception/vehicles/{id}`
- `POST /api/reception/vehicles`

```json
{
  "ownerPersonId": 1,
  "modelId": 1,
  "vehicleTypeId": 1,
  "plate": "REC001",
  "vin": "RECEPTIONVIN0001",
  "year": 2019,
  "color": "Azul",
  "mileage": 42000,
  "startDate": "2026-06-04T00:00:00Z"
}
```

- `GET /api/reception/vehicles/{id}/owner-history`
- `POST /api/reception/vehicles/{id}/transfer-owner`

```json
{ "newOwnerPersonId": 2, "transferDate": "2026-06-04T00:00:00Z" }
```

- `GET /api/reception/payments`
- `GET /api/reception/invoices`
- `GET /api/reception/payments/pending-verification`
- `GET /api/reception/payments/{paymentId}`
- `POST /api/reception/payments/{paymentId}/approve`

```json
{ "deliveryDate": "2026-06-10T15:00:00Z", "comment": "Pago verificado en Swagger" }
```

- `POST /api/reception/payments/{paymentId}/reject`

```json
{ "deliveryDate": null, "comment": "Referencia no valida" }
```

- `POST /api/reception/orders/{orderId}/confirm-delivery-date`

```json
{ "deliveryDate": "2026-06-10T15:00:00Z" }
```

## 6. Ordenes de servicio

- `GET /api/ServiceOrders`
- `GET /api/ServiceOrders/{id}`
- `GET /api/ServiceOrders/{id}/services`
- `POST /api/ServiceOrders/empty`

```json
{ "clientPersonId": 1, "vehicleId": 1 }
```

- `POST /api/ServiceOrders/diagnostic`

```json
{
  "clientPersonId": 1,
  "vehicleId": 1,
  "entryDate": "2026-06-04T08:00:00Z",
  "mileage": 43000,
  "problemDescription": "Ruido al frenar",
  "observations": "Prueba desde Swagger",
  "estimatedDeliveryDate": "2026-06-08T17:00:00Z",
  "checklist": {
    "lights": true,
    "tires": true,
    "mirrors": true,
    "documents": true,
    "tools": false,
    "scratchesOrDents": false,
    "fuelLevel": "Medio",
    "objectsInsideVehicle": "Ninguno",
    "notes": "Sin novedades"
  },
  "serviceAssignment": {
    "serviceTypeId": 1,
    "workshopServiceId": null,
    "specialtyId": 1,
    "mechanicPersonId": 1,
    "observation": "Diagnostico inicial",
    "laborCost": 80000
  },
  "serviceAssignments": null
}
```

- `POST /api/ServiceOrders`

```json
{
  "vehicleId": 1,
  "orderStatusId": 1,
  "estimatedDeliveryDate": "2026-06-10T17:00:00Z",
  "generalDescription": "Orden creada desde Swagger"
}
```

- `PATCH /api/ServiceOrders/{id}/work`

```json
{ "workPerformed": "Se realizo diagnostico y prueba de ruta." }
```

- `PATCH /api/ServiceOrders/{id}/status`

```json
{ "orderStatusId": 2, "userId": 1, "observation": "Cambio desde Swagger" }
```

## 7. Mecanico y jefe de taller

Login mecanico: `mecanico@autotaller.com`.

- `GET /api/mechanic/orders`
- `GET /api/mechanic/orders/{orderId}`
- `GET /api/mechanic/requests`
- `GET /api/mechanic/diagnostics`
- `POST /api/mechanic/orders/{orderId}/diagnostics`

```json
{ "findings": "Pastillas desgastadas", "recommendedWork": "Cambio de pastillas delanteras" }
```

- `POST /api/mechanic/orders/{orderId}/additional-requests`

```json
{
  "requestType": 1,
  "workshopServiceId": null,
  "partId": 1,
  "quantity": 2,
  "technicalComment": "Se requieren repuestos adicionales"
}
```

- `POST /api/mechanic/orders/{orderId}/work`

```json
{ "workPerformed": "Trabajo registrado desde Swagger" }
```

- `POST /api/mechanic/orders/{orderId}/complete`

```json
{ "workPerformed": "Orden completada desde Swagger" }
```

- `PATCH /api/mechanic/order-services/{orderServiceId}/status`

```json
{ "status": "Completed" }
```

Login jefe de taller: `jefe.mecanicos@autotaller.com`.

- `GET /api/workshop-chief/requests`
- `GET /api/workshop-chief/requests/{requestId}`
- `POST /api/workshop-chief/requests/{requestId}/approve`
- `POST /api/workshop-chief/requests/{requestId}/reject`
- `GET /api/workshop-chief/diagnostics`
- `GET /api/workshop-chief/diagnostics/{diagnosticId}`
- `POST /api/workshop-chief/diagnostics/{diagnosticId}/approve`
- `POST /api/workshop-chief/diagnostics/{diagnosticId}/reject`

Body para aprobar/rechazar:

```json
{ "comment": "Revisado desde Swagger" }
```

## 8. Cliente

Login cliente: `carlos.ramirez@test.com`.

- `GET /api/client/orders`
- `GET /api/client/orders/{orderId}`
- `POST /api/client/orders/{orderId}/approve`
- `POST /api/client/orders/{orderId}/reject`
- `GET /api/client/approvals`
- `POST /api/client/approvals/{requestId}/approve`
- `POST /api/client/approvals/{requestId}/reject`
- `GET /api/client/messages`
- `GET /api/client/payments`
- `GET /api/client/invoices`

Body para aprobar/rechazar:

```json
{ "comment": "Respuesta del cliente desde Swagger" }
```

- `POST /api/client/payments`

```json
{
  "invoiceId": 1,
  "paymentMethodId": 1,
  "amount": 150000,
  "reference": "SWG-PAY-001",
  "cardType": "Visa",
  "cardLastFourDigits": "1234",
  "cardHolderName": "Carlos Ramirez",
  "cardBrand": "Visa"
}
```

## 9. Bodega, inventario y stock

Login bodega: `jefebodega@autotaller.com`.

- `GET /api/warehouse/products`
- `POST /api/warehouse/products`
- `GET /api/warehouse/stock-submissions`
- `GET /api/warehouse/stock-submissions/{id}`
- `POST /api/warehouse/stock-submissions`

```json
{
  "productName": "Filtro Swagger",
  "referenceCode": "SWG-STOCK-001",
  "supplierId": 1,
  "supplierPrice": 25000,
  "profitPercentage": 30,
  "quantity": 20,
  "minimumStock": 5,
  "partCategoryId": 1,
  "partBrandId": 1,
  "categoryName": null,
  "brandName": null,
  "description": "Producto de prueba Swagger",
  "warehouseComment": "Alta desde Swagger"
}
```

- `PUT /api/warehouse/products/{id}`: usa el mismo body cambiando precio o cantidad.
- `POST /api/warehouse/stock-submissions/{id}/send-to-review`

Login inventario: `jefealmacen@autotaller.com`.

- `GET /api/inventory/dashboard`
- `GET /api/inventory/review-requests`
- `GET /api/inventory/review-requests/{id}`
- `POST /api/inventory/review-requests/{id}/approve`
- `POST /api/inventory/review-requests/{id}/reject`

```json
{ "comment": "Revision desde Swagger" }
```

- `GET /api/inventory/products`
- `POST /api/inventory/products`

```json
{
  "partCategoryId": 1,
  "partBrandId": 1,
  "code": "INV-SWG-001",
  "description": "Producto inventario Swagger",
  "minimumStock": 4,
  "unitPrice": 32000,
  "isActive": true
}
```

- `PUT /api/inventory/products/{id}`: usa el mismo body actualizado.
- `PATCH /api/inventory/products/{id}/activate`
- `PATCH /api/inventory/products/{id}/deactivate`
- `GET /api/inventory/categories`
- `POST /api/inventory/categories`
- `PUT /api/inventory/categories/{id}`
- `GET /api/inventory/brands`
- `POST /api/inventory/brands`
- `PUT /api/inventory/brands/{id}`

Body categorias/marcas:

```json
{ "name": "Catalogo Swagger" }
```

- `GET /api/inventory/suppliers`
- `POST /api/inventory/suppliers`
- `PUT /api/inventory/suppliers/{id}`

```json
{
  "name": "Proveedor Swagger",
  "taxId": "NIT-SWG",
  "phone": "3001112233",
  "email": "proveedor.swagger@test.com",
  "status": true
}
```

- `GET /api/inventory/history`

Login Admin para stock:

- `GET /api/stock/dashboard`
- `GET /api/stock/parts`
- `GET /api/stock/low-stock`
- `GET /api/stock/out-of-stock`
- `GET /api/stock/movements`
- `GET /api/stock/parts/{id}/movements`
- `POST /api/stock/movements/in`
- `POST /api/stock/movements/out`

```json
{ "partId": 1, "quantity": 5, "comment": "Movimiento desde Swagger" }
```

## 10. Admin

Login Admin.

- `GET /api/admin/dashboard`
- `GET /api/admin/clients`
- `GET /api/admin/clients/{id}`
- `GET /api/admin/clients/{id}/vehicles`
- `GET /api/admin/users`
- `POST /api/admin/users`

```json
{
  "documentTypeId": 1,
  "documentNumber": "ADM-1001",
  "firstName": "Usuario",
  "lastName": "AdminSwagger",
  "email": "admin.swagger1001@test.com",
  "phone": "3002223344",
  "phoneCountryId": 1,
  "password": "DevPass123!",
  "roleId": 2,
  "mechanicSpecialtyId": null,
  "isActive": true
}
```

- `PATCH /api/admin/users/{id}/status`

```json
{ "isActive": true }
```

- `PUT /api/admin/users/{id}/roles`

```json
{ "roleNames": ["Receptionist"] }
```

## 11. Facturacion y pagos generales

- `GET /api/Invoices`
- `GET /api/Invoices/{id}`
- `POST /api/Invoices`

```json
{
  "invoiceNumber": "INV-SWG-001",
  "serviceOrderId": 1,
  "invoiceStatusId": 1,
  "subtotal": 126050,
  "tax": 23950,
  "total": 150000,
  "observations": "Factura creada desde Swagger"
}
```

- `GET /api/InvoiceDetails`
- `GET /api/InvoiceDetails/{id}`
- `GET /api/Payments`
- `GET /api/Payments/{id}`
- `GET /api/PaymentCards`
- `GET /api/PaymentCards/{id}`

## 12. Historiales y detalles

Prueba `GET`, `GET /{id}`, y luego `POST/PUT/DELETE` solo con ids reales:

- `/api/VehicleOwnerHistory`
- `/api/VehicleEntryInventory`
- `/api/MechanicAssignments`
- `/api/OrderServices`
- `/api/OrderServiceParts`
- `/api/OrderStatusHistory`
- `/api/PartPurchases`
- `/api/PartPurchaseDetails`

Ejemplo `POST /api/MechanicAssignments`:

```json
{ "orderServiceId": 1, "mechanicPersonId": 1, "specialtyId": 1 }
```

Ejemplo `POST /api/OrderServices`:

```json
{ "serviceOrderId": 1, "serviceTypeId": 1, "description": "Prueba Swagger", "laborCost": 80000 }
```

Ejemplo `POST /api/OrderServiceParts`:

```json
{ "orderServiceId": 1, "partId": 1, "quantity": 2, "appliedUnitPrice": 45000 }
```

## 13. Resultado esperado

- `200 OK`: consulta o accion correcta.
- `201 Created`: creacion correcta.
- `204 No Content`: actualizacion o accion sin cuerpo.
- `400 Bad Request`: datos invalidos o ids que no existen.
- `401 Unauthorized`: falta token.
- `403 Forbidden`: rol incorrecto.
- `404 Not Found`: id inexistente.
- `409 Conflict`: dato duplicado.
- `429 Too Many Requests`: rate limit.
