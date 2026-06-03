# AutoTallerManager - Credenciales de prueba

Credenciales para probar los paneles del sistema en ambiente `Development`.

> Estas cuentas se crean y reparan automaticamente desde `DevelopmentDataSeeder` al levantar el backend. La contraseña principal de prueba es `DevPass123!`.
>
> Para bases locales que quedaron sembradas con datos anteriores, el login en `Development` tambien acepta `Password123*` y actualiza el hash del usuario automaticamente.

## Paneles principales

| Panel | Rol | Email | Password | Ruta sugerida |
| --- | --- | --- | --- | --- |
| Administrador | `Admin` | `admin@autotaller.com` | `DevPass123!` | `/dashboard/admin` |
| Recepcionista | `Receptionist` | `recepcionista@autotaller.com` | `DevPass123!` | `/dashboard/reception` |
| Jefe de mecanicos | `WorkshopChief` | `jefe.mecanicos@autotaller.com` | `DevPass123!` | `/dashboard/workshop-chief` |
| Cliente Carlos | `Client` | `carlos.ramirez@test.com` | `DevPass123!` | `/dashboard/client` |
| Cliente Laura | `Client` | `laura.gomez@test.com` | `DevPass123!` | `/dashboard/client` |

## Paneles de mecanicos

| Panel | Rol | Especialidad | Email | Password | Ruta sugerida |
| --- | --- | --- | --- | --- | --- |
| Mecanico general | `Mechanic` | General / diagnostico | `mecanico@autotaller.com` | `DevPass123!` | `/dashboard/mechanic` |
| Mecanico de diagnostico | `Mechanic` | Diagnostico | `diagnostico@autotaller.com` | `DevPass123!` | `/dashboard/mechanic` |
| Mecanico de mantenimiento | `Mechanic` | Mantenimiento | `mantenimiento@autotaller.com` | `DevPass123!` | `/mechanic/maintenance` |
| Mecanico electricista | `Mechanic` | Electricista | `electricista@autotaller.com` | `DevPass123!` | `/mechanic/electricity` |
| Mecanico de frenos | `Mechanic` | Frenos | `frenos@autotaller.com` | `DevPass123!` | `/mechanic/brakes` |

## Paneles de stock e inventario

| Panel | Rol | Email | Password | Ruta sugerida |
| --- | --- | --- | --- | --- |
| Jefe de stock / bodega | `WarehouseChief` | `jefebodega@autotaller.com` | `DevPass123!` | `/dashboard/warehouse-chief` o `/stock-manager` |
| Jefe de inventario / almacen | `InventoryManager` | `jefealmacen@autotaller.com` | `DevPass123!` | `/dashboard/inventory-manager` o `/inventory-manager` |


## Probar login en Swagger

Endpoint:

```txt
POST /api/auth/login
```

Body recomendado:

```json
{
  "email": "carlos.ramirez@test.com",
  "password": "DevPass123!"
}
```

## Comandos para levantar

Configura primero los secretos locales del backend. Estos valores no deben guardarse en `appsettings.json`.

Opcion recomendada en desarrollo:

```bash
cd /Users/wen/.NetProyecto-1/Api
dotnet user-secrets set "ConnectionStrings:MySql" "server=localhost;port=3306;database=AutoTallerManager;user=TU_USUARIO;password=TU_PASSWORD;"
dotnet user-secrets set "Jwt:Key" "TU_CLAVE_LOCAL_DE_MINIMO_32_CARACTERES"
```

Alternativa con variables de entorno:

```bash
export ConnectionStrings__MySql="server=localhost;port=3306;database=AutoTallerManager;user=TU_USUARIO;password=TU_PASSWORD;"
export Jwt__Key="TU_CLAVE_LOCAL_DE_MINIMO_32_CARACTERES"
```

Backend:

```bash
cd /Users/wen/.NetProyecto-1
dotnet run --project Api/Api.csproj
```

Frontend:

```bash
cd /Users/wen/.netFrontend-1
pnpm run dev
```

## Reiniciar base de datos local y volver a sembrar

Usa esto solo en ambiente de desarrollo. `database drop` elimina la base de datos local configurada por `ConnectionStrings:MySql`, por lo que perderas los datos guardados manualmente.

Ejecuta desde la raiz del repositorio (`/Users/wen/.NetProyecto-1`):

Si usas `dotnet ef`, exporta la cadena de conexion para que el factory de EF pueda leerla:

```bash
export ConnectionStrings__MySql="server=localhost;port=3306;database=AutoTallerManager;user=TU_USUARIO;password=TU_PASSWORD;"
```

```bash
dotnet ef database drop --project Infrastructure/Infrastructure.csproj --startup-project Api/Api.csproj
dotnet ef database update --project Infrastructure/Infrastructure.csproj --startup-project Api/Api.csproj
dotnet run --project Api/Api.csproj
```

Al iniciar el backend en `Development`, se ejecuta automaticamente `DevelopmentDataSeeder` y quedan creados los usuarios, clientes, vehiculos, ordenes, diagnosticos, facturas y pagos de prueba.

Si `dotnet ef` no existe en tu maquina:

```bash
dotnet tool install --global dotnet-ef
```

Si ya lo tienes instalado y falla por version:

```bash
dotnet tool update --global dotnet-ef
```

## Importante

Si cambias credenciales en el seeder, reinicia el backend en ambiente `Development` para que se reparen los hashes de los usuarios existentes.

Si una cuenta no deja entrar desde la interfaz pero si entra desde Swagger, borra el token/sesion del navegador y vuelve a iniciar sesion.
 necesito que tu me realices una prueba de endpoint por endpoint para saber que todos los endpoint funcionan bien y me haces ub kistadi de cuabtos nataron 