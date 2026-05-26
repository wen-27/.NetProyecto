CREATE DATABASE IF NOT EXISTS AutoTallerManager;
USE AutoTallerManager;

CREATE TABLE TiposDocumento (
    IdTipoDocumento INT AUTO_INCREMENT PRIMARY KEY,
    Codigo VARCHAR(10) NOT NULL UNIQUE,
    Nombre VARCHAR(80) NOT NULL UNIQUE
);

CREATE TABLE Personas (
    IdPersona INT AUTO_INCREMENT PRIMARY KEY,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE DocumentosPersona (
    IdDocumentoPersona INT AUTO_INCREMENT PRIMARY KEY,
    IdPersona INT NOT NULL,
    IdTipoDocumento INT NOT NULL,
    NumeroDocumento VARCHAR(50) NOT NULL,
    EsPrincipal TINYINT(1) NOT NULL DEFAULT 0,
    FOREIGN KEY (IdPersona) REFERENCES Personas(IdPersona),
    FOREIGN KEY (IdTipoDocumento) REFERENCES TiposDocumento(IdTipoDocumento),
    UNIQUE (IdTipoDocumento, NumeroDocumento)
);

CREATE TABLE DominiosCorreo (
    IdDominioCorreo INT AUTO_INCREMENT PRIMARY KEY,
    Dominio VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE CorreosPersona (
    IdCorreoPersona INT AUTO_INCREMENT PRIMARY KEY,
    IdPersona INT NOT NULL,
    IdDominioCorreo INT NOT NULL,
    UsuarioCorreo VARCHAR(100) NOT NULL,
    EsPrincipal TINYINT(1) NOT NULL DEFAULT 0,
    FOREIGN KEY (IdPersona) REFERENCES Personas(IdPersona),
    FOREIGN KEY (IdDominioCorreo) REFERENCES DominiosCorreo(IdDominioCorreo),
    UNIQUE (UsuarioCorreo, IdDominioCorreo)
);

CREATE TABLE CodigosTelefono (
    IdCodigoTelefono INT AUTO_INCREMENT PRIMARY KEY,
    Codigo VARCHAR(10) NOT NULL UNIQUE,
    Pais VARCHAR(80) NOT NULL
);

CREATE TABLE TelefonosPersona (
    IdTelefonoPersona INT AUTO_INCREMENT PRIMARY KEY,
    IdPersona INT NOT NULL,
    IdCodigoTelefono INT NOT NULL,
    NumeroTelefono VARCHAR(30) NOT NULL,
    EsPrincipal TINYINT(1) NOT NULL DEFAULT 0,
    FOREIGN KEY (IdPersona) REFERENCES Personas(IdPersona),
    FOREIGN KEY (IdCodigoTelefono) REFERENCES CodigosTelefono(IdCodigoTelefono),
    UNIQUE (IdCodigoTelefono, NumeroTelefono)
);

CREATE TABLE Clientes (
    IdCliente INT AUTO_INCREMENT PRIMARY KEY,
    IdPersona INT NOT NULL UNIQUE,
    Estado TINYINT(1) NOT NULL DEFAULT 1,
    FOREIGN KEY (IdPersona) REFERENCES Personas(IdPersona)
);

CREATE TABLE Usuarios (
    IdUsuario INT AUTO_INCREMENT PRIMARY KEY,
    IdPersona INT NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Estado TINYINT(1) NOT NULL DEFAULT 1,
    FOREIGN KEY (IdPersona) REFERENCES Personas(IdPersona)
);

CREATE TABLE Roles (
    IdRol INT AUTO_INCREMENT PRIMARY KEY,
    NombreRol VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE UsuarioRoles (
    IdUsuario INT NOT NULL,
    IdRol INT NOT NULL,
    PRIMARY KEY (IdUsuario, IdRol),
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (IdRol) REFERENCES Roles(IdRol)
);

CREATE TABLE MarcasVehiculo (
    IdMarca INT AUTO_INCREMENT PRIMARY KEY,
    NombreMarca VARCHAR(80) NOT NULL UNIQUE
);

CREATE TABLE ModelosVehiculo (
    IdModelo INT AUTO_INCREMENT PRIMARY KEY,
    IdMarca INT NOT NULL,
    NombreModelo VARCHAR(80) NOT NULL,
    FOREIGN KEY (IdMarca) REFERENCES MarcasVehiculo(IdMarca),
    UNIQUE (IdMarca, NombreModelo)
);

CREATE TABLE Vehiculos (
    IdVehiculo INT AUTO_INCREMENT PRIMARY KEY,
    IdModelo INT NOT NULL,
    VIN VARCHAR(17) NOT NULL UNIQUE,
    Anio YEAR NOT NULL,
    Kilometraje INT NOT NULL DEFAULT 0,
    FOREIGN KEY (IdModelo) REFERENCES ModelosVehiculo(IdModelo)
);

CREATE TABLE HistorialPropietariosVehiculo (
    IdHistorialPropietario INT AUTO_INCREMENT PRIMARY KEY,
    IdVehiculo INT NOT NULL,
    IdCliente INT NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaFin DATE NULL,
    FOREIGN KEY (IdVehiculo) REFERENCES Vehiculos(IdVehiculo),
    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente)
);

CREATE TABLE TiposServicio (
    IdTipoServicio INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(80) NOT NULL UNIQUE
);

CREATE TABLE EstadosOrden (
    IdEstadoOrden INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE OrdenesServicio (
    IdOrdenServicio INT AUTO_INCREMENT PRIMARY KEY,
    IdVehiculo INT NOT NULL,
    IdTipoServicio INT NOT NULL,
    IdMecanico INT NOT NULL,
    IdEstadoOrden INT NOT NULL,
    FechaIngreso DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FechaEstimadaEntrega DATETIME NULL,
    TrabajoRealizado TEXT NULL,
    FOREIGN KEY (IdVehiculo) REFERENCES Vehiculos(IdVehiculo),
    FOREIGN KEY (IdTipoServicio) REFERENCES TiposServicio(IdTipoServicio),
    FOREIGN KEY (IdMecanico) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (IdEstadoOrden) REFERENCES EstadosOrden(IdEstadoOrden)
);

CREATE TABLE CategoriasRepuesto (
    IdCategoriaRepuesto INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(80) NOT NULL UNIQUE
);

CREATE TABLE Repuestos (
    IdRepuesto INT AUTO_INCREMENT PRIMARY KEY,
    IdCategoriaRepuesto INT NOT NULL,
    Codigo VARCHAR(50) NOT NULL UNIQUE,
    Descripcion VARCHAR(255) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    StockMinimo INT NOT NULL DEFAULT 0,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    Activo TINYINT(1) NOT NULL DEFAULT 1,
    FOREIGN KEY (IdCategoriaRepuesto) REFERENCES CategoriasRepuesto(IdCategoriaRepuesto)
);

CREATE TABLE DetalleOrdenRepuestos (
    IdDetalleOrdenRepuesto INT AUTO_INCREMENT PRIMARY KEY,
    IdOrdenServicio INT NOT NULL,
    IdRepuesto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitarioAplicado DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (IdOrdenServicio) REFERENCES OrdenesServicio(IdOrdenServicio),
    FOREIGN KEY (IdRepuesto) REFERENCES Repuestos(IdRepuesto),
    UNIQUE (IdOrdenServicio, IdRepuesto)
);

CREATE TABLE Facturas (
    IdFactura INT AUTO_INCREMENT PRIMARY KEY,
    IdOrdenServicio INT NOT NULL UNIQUE,
    FechaFactura DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ManoObra DECIMAL(10,2) NOT NULL DEFAULT 0,
    Total DECIMAL(10,2) NOT NULL DEFAULT 0,
    FOREIGN KEY (IdOrdenServicio) REFERENCES OrdenesServicio(IdOrdenServicio)
);

CREATE TABLE DetalleFactura (
    IdDetalleFactura INT AUTO_INCREMENT PRIMARY KEY,
    IdFactura INT NOT NULL,
    Concepto VARCHAR(150) NOT NULL,
    Cantidad INT NOT NULL DEFAULT 1,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (IdFactura) REFERENCES Facturas(IdFactura)
);

CREATE TABLE TiposAccionAuditoria (
    IdTipoAccionAuditoria INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Auditorias (
    IdAuditoria INT AUTO_INCREMENT PRIMARY KEY,
    IdUsuario INT NOT NULL,
    IdTipoAccionAuditoria INT NOT NULL,
    EntidadAfectada VARCHAR(100) NOT NULL,
    IdRegistroAfectado INT NOT NULL,
    FechaHora DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Descripcion TEXT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (IdTipoAccionAuditoria) REFERENCES TiposAccionAuditoria(IdTipoAccionAuditoria)
);