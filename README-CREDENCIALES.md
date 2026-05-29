# AutoTallerManager - Credenciales de prueba

Credenciales para probar los paneles creados y los paneles principales del sistema en ambiente de desarrollo.

> Nota: estas cuentas se crean desde `DevelopmentDataSeeder`. Las cuentas configuradas en `Api/appsettings.Development.json` usan `Password123*`. Las cuentas que no estan en ese archivo usan la contraseña por defecto del seeder: `Admin123*`.

## Paneles principales

| Panel | Rol | Email | Password | Ruta sugerida |
| --- | --- | --- | --- | --- |
| Administrador | `Admin` | `admin@mail.com` | `Password123*` | `/dashboard/admin` |
| Recepcionista | `Receptionist` | `receptionist@mail.com` | `Password123*` | `/dashboard/reception` |
| Cliente | `Client` | `client@mail.com` | `Password123*` | `/dashboard/client` |
| Jefe de taller | `WorkshopChief` | `jefetaller@autotaller.com` | `Admin123*` | `/dashboard/workshop-chief` |

## Paneles de mecanicos

| Panel | Rol | Especialidad | Email | Password | Ruta sugerida |
| --- | --- | --- | --- | --- | --- |
| Mecanico general | `Mechanic` | General / diagnostico | `mechanic@mail.com` | `Password123*` | `/dashboard/mechanic` |
| Mecanico de diagnostico | `Mechanic` | Diagnostico | `diagnostico@autotaller.com` | `Admin123*` | `/dashboard/mechanic` |
| Mecanico de electricidad | `Mechanic` | Electricista | `electricista@autotaller.com` | `Admin123*` | `/mechanic/electricity` |
| Mecanico de mantenimiento | `Mechanic` | Mantenimiento | `mantenimiento@autotaller.com` | `Admin123*` | `/mechanic/maintenance` |
| Mecanico de frenos | `Mechanic` | Frenos | `frenos@autotaller.com` | `Admin123*` | `/mechanic/brakes` |

## Paneles de stock e inventario

| Panel | Rol | Email | Password | Ruta sugerida |
| --- | --- | --- | --- | --- |
| Jefe de stock / bodega | `WarehouseChief` | `jefebodega@autotaller.com` | `Admin123*` | `/dashboard/warehouse-chief` o `/stock-manager` |
| Jefe de inventario / almacen | `InventoryManager` | `jefealmacen@autotaller.com` | `Admin123*` | `/dashboard/inventory-manager` o `/inventory-manager` |

## Comandos para probar

Backend:

```bash
dotnet run --project Api/Api.csproj
```

Frontend:

```bash
cd /Users/wen/.netFrontend-1
pnpm run dev
```

## Importante

Si cambias credenciales en `Api/appsettings.Development.json`, esas credenciales reemplazan las del seeder para la cuenta correspondiente.

Si una cuenta no deja entrar, reinicia el backend en ambiente Development para que el seeder actualice la contraseña del usuario existente.
