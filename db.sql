CREATE DATABASE IF NOT EXISTS AutoWorkshopManager;
USE AutoWorkshopManager;

CREATE TABLE DocumentTypes (
    DocumentTypeId INT AUTO_INCREMENT PRIMARY KEY,
    Code VARCHAR(10) NOT NULL UNIQUE,
    Name VARCHAR(80) NOT NULL UNIQUE
);

CREATE TABLE Persons (
    PersonId INT AUTO_INCREMENT PRIMARY KEY,
    FirstNames VARCHAR(100) NOT NULL,
    LastNames VARCHAR(100) NOT NULL,
    RegistrationDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Departments (
    DepartmentId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Cities (
    CityId INT AUTO_INCREMENT PRIMARY KEY,
    DepartmentId INT NOT NULL,
    Name VARCHAR(100) NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(DepartmentId),
    UNIQUE (DepartmentId, Name)
);

CREATE TABLE PersonAddresses (
    PersonAddressId INT AUTO_INCREMENT PRIMARY KEY,
    PersonId INT NOT NULL,
    CityId INT NOT NULL,
    Address VARCHAR(150) NOT NULL,
    IsPrimary TINYINT(1) NOT NULL DEFAULT 0,
    FOREIGN KEY (PersonId) REFERENCES Persons(PersonId),
    FOREIGN KEY (CityId) REFERENCES Cities(CityId)
);

CREATE TABLE PersonDocuments (
    PersonDocumentId INT AUTO_INCREMENT PRIMARY KEY,
    PersonId INT NOT NULL,
    DocumentTypeId INT NOT NULL,
    DocumentNumber VARCHAR(50) NOT NULL,
    IsPrimary TINYINT(1) NOT NULL DEFAULT 0,
    FOREIGN KEY (PersonId) REFERENCES Persons(PersonId),
    FOREIGN KEY (DocumentTypeId) REFERENCES DocumentTypes(DocumentTypeId),
    UNIQUE (DocumentTypeId, DocumentNumber)
);

CREATE TABLE EmailDomains (
    EmailDomainId INT AUTO_INCREMENT PRIMARY KEY,
    Domain VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE PersonEmails (
    PersonEmailId INT AUTO_INCREMENT PRIMARY KEY,
    PersonId INT NOT NULL,
    EmailDomainId INT NOT NULL,
    EmailUser VARCHAR(100) NOT NULL,
    IsPrimary TINYINT(1) NOT NULL DEFAULT 0,
    FOREIGN KEY (PersonId) REFERENCES Persons(PersonId),
    FOREIGN KEY (EmailDomainId) REFERENCES EmailDomains(EmailDomainId),
    UNIQUE (EmailUser, EmailDomainId)
);

CREATE TABLE PhoneCodes (
    PhoneCodeId INT AUTO_INCREMENT PRIMARY KEY,
    Code VARCHAR(10) NOT NULL UNIQUE,
    Country VARCHAR(80) NOT NULL
);

CREATE TABLE PersonPhones (
    PersonPhoneId INT AUTO_INCREMENT PRIMARY KEY,
    PersonId INT NOT NULL,
    PhoneCodeId INT NOT NULL,
    PhoneNumber VARCHAR(30) NOT NULL,
    IsPrimary TINYINT(1) NOT NULL DEFAULT 0,
    FOREIGN KEY (PersonId) REFERENCES Persons(PersonId),
    FOREIGN KEY (PhoneCodeId) REFERENCES PhoneCodes(PhoneCodeId),
    UNIQUE (PhoneCodeId, PhoneNumber)
);

CREATE TABLE Customers (
    CustomerId INT AUTO_INCREMENT PRIMARY KEY,
    PersonId INT NOT NULL UNIQUE,
    Status TINYINT(1) NOT NULL DEFAULT 1,
    FOREIGN KEY (PersonId) REFERENCES Persons(PersonId)
);

CREATE TABLE Users (
    UserId INT AUTO_INCREMENT PRIMARY KEY,
    PersonId INT NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Status TINYINT(1) NOT NULL DEFAULT 1,
    FOREIGN KEY (PersonId) REFERENCES Persons(PersonId)
);

CREATE TABLE Roles (
    RoleId INT AUTO_INCREMENT PRIMARY KEY,
    RoleName VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE UserRoles (
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

CREATE TABLE VehicleTypes (
    VehicleTypeId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(80) NOT NULL UNIQUE
);

CREATE TABLE VehicleBrands (
    BrandId INT AUTO_INCREMENT PRIMARY KEY,
    BrandName VARCHAR(80) NOT NULL UNIQUE
);

CREATE TABLE VehicleModels (
    ModelId INT AUTO_INCREMENT PRIMARY KEY,
    BrandId INT NOT NULL,
    ModelName VARCHAR(80) NOT NULL,
    FOREIGN KEY (BrandId) REFERENCES VehicleBrands(BrandId),
    UNIQUE (BrandId, ModelName)
);

CREATE TABLE Vehicles (
    VehicleId INT AUTO_INCREMENT PRIMARY KEY,
    ModelId INT NOT NULL,
    VehicleTypeId INT NOT NULL,
    VIN VARCHAR(17) NOT NULL UNIQUE,
    Year YEAR NOT NULL,
    Mileage INT NOT NULL DEFAULT 0,
    FOREIGN KEY (ModelId) REFERENCES VehicleModels(ModelId),
    FOREIGN KEY (VehicleTypeId) REFERENCES VehicleTypes(VehicleTypeId)
);

CREATE TABLE VehicleOwnerHistory (
    VehicleOwnerHistoryId INT AUTO_INCREMENT PRIMARY KEY,
    VehicleId INT NOT NULL,
    CustomerId INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NULL,
    FOREIGN KEY (VehicleId) REFERENCES Vehicles(VehicleId),
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

CREATE TABLE ServiceTypes (
    ServiceTypeId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(80) NOT NULL UNIQUE
);

CREATE TABLE OrderStatuses (
    OrderStatusId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE ServiceOrders (
    ServiceOrderId INT AUTO_INCREMENT PRIMARY KEY,
    VehicleId INT NOT NULL,
    OrderStatusId INT NOT NULL,
    EntryDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    EstimatedDeliveryDate DATETIME NULL,
    WorkPerformed TEXT NULL,
    FOREIGN KEY (VehicleId) REFERENCES Vehicles(VehicleId),
    FOREIGN KEY (OrderStatusId) REFERENCES OrderStatuses(OrderStatusId)
);

CREATE TABLE ServiceOrderServices (
    ServiceOrderServiceId INT AUTO_INCREMENT PRIMARY KEY,
    ServiceOrderId INT NOT NULL,
    ServiceTypeId INT NOT NULL,
    MechanicId INT NOT NULL,
    Description TEXT NULL,
    LaborCost DECIMAL(10,2) NOT NULL DEFAULT 0,
    FOREIGN KEY (ServiceOrderId) REFERENCES ServiceOrders(ServiceOrderId),
    FOREIGN KEY (ServiceTypeId) REFERENCES ServiceTypes(ServiceTypeId),
    FOREIGN KEY (MechanicId) REFERENCES Users(UserId),
    UNIQUE (ServiceOrderId, ServiceTypeId)
);

CREATE TABLE OrderStatusHistory (
    OrderStatusHistoryId INT AUTO_INCREMENT PRIMARY KEY,
    ServiceOrderId INT NOT NULL,
    PreviousOrderStatusId INT NULL,
    NewOrderStatusId INT NOT NULL,
    UserId INT NOT NULL,
    ChangeDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Observation TEXT NULL,
    FOREIGN KEY (ServiceOrderId) REFERENCES ServiceOrders(ServiceOrderId),
    FOREIGN KEY (PreviousOrderStatusId) REFERENCES OrderStatuses(OrderStatusId),
    FOREIGN KEY (NewOrderStatusId) REFERENCES OrderStatuses(OrderStatusId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE PartCategories (
    PartCategoryId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(80) NOT NULL UNIQUE
);

CREATE TABLE PartBrands (
    PartBrandId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(80) NOT NULL UNIQUE
);

CREATE TABLE Suppliers (
    SupplierId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(120) NOT NULL,
    TaxId VARCHAR(30) NULL UNIQUE,
    Phone VARCHAR(30) NULL,
    Email VARCHAR(120) NULL,
    Status TINYINT(1) NOT NULL DEFAULT 1
);

CREATE TABLE Parts (
    PartId INT AUTO_INCREMENT PRIMARY KEY,
    PartCategoryId INT NOT NULL,
    PartBrandId INT NULL,
    Code VARCHAR(50) NOT NULL UNIQUE,
    Description VARCHAR(255) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    MinimumStock INT NOT NULL DEFAULT 0,
    UnitPrice DECIMAL(10,2) NOT NULL,
    IsActive TINYINT(1) NOT NULL DEFAULT 1,
    FOREIGN KEY (PartCategoryId) REFERENCES PartCategories(PartCategoryId),
    FOREIGN KEY (PartBrandId) REFERENCES PartBrands(PartBrandId)
);

CREATE TABLE ServiceOrderParts (
    ServiceOrderPartId INT AUTO_INCREMENT PRIMARY KEY,
    ServiceOrderId INT NOT NULL,
    PartId INT NOT NULL,
    Quantity INT NOT NULL,
    AppliedUnitPrice DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (ServiceOrderId) REFERENCES ServiceOrders(ServiceOrderId),
    FOREIGN KEY (PartId) REFERENCES Parts(PartId),
    UNIQUE (ServiceOrderId, PartId)
);

CREATE TABLE PartPurchases (
    PartPurchaseId INT AUTO_INCREMENT PRIMARY KEY,
    SupplierId INT NOT NULL,
    PurchaseDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Total DECIMAL(10,2) NOT NULL DEFAULT 0,
    FOREIGN KEY (SupplierId) REFERENCES Suppliers(SupplierId)
);

CREATE TABLE PartPurchaseDetails (
    PartPurchaseDetailId INT AUTO_INCREMENT PRIMARY KEY,
    PartPurchaseId INT NOT NULL,
    PartId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (PartPurchaseId) REFERENCES PartPurchases(PartPurchaseId),
    FOREIGN KEY (PartId) REFERENCES Parts(PartId),
    UNIQUE (PartPurchaseId, PartId)
);

CREATE TABLE InvoiceStatuses (
    InvoiceStatusId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Invoices (
    InvoiceId INT AUTO_INCREMENT PRIMARY KEY,
    ServiceOrderId INT NOT NULL UNIQUE,
    InvoiceStatusId INT NOT NULL,
    InvoiceDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    LaborCost DECIMAL(10,2) NOT NULL DEFAULT 0,
    Total DECIMAL(10,2) NOT NULL DEFAULT 0,
    FOREIGN KEY (ServiceOrderId) REFERENCES ServiceOrders(ServiceOrderId),
    FOREIGN KEY (InvoiceStatusId) REFERENCES InvoiceStatuses(InvoiceStatusId)
);

CREATE TABLE InvoiceDetails (
    InvoiceDetailId INT AUTO_INCREMENT PRIMARY KEY,
    InvoiceId INT NOT NULL,
    Concept VARCHAR(150) NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    UnitPrice DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (InvoiceId) REFERENCES Invoices(InvoiceId)
);

CREATE TABLE PaymentMethods (
    PaymentMethodId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE PaymentStatuses (
    PaymentStatusId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Payments (
    PaymentId INT AUTO_INCREMENT PRIMARY KEY,
    InvoiceId INT NOT NULL,
    PaymentMethodId INT NOT NULL,
    PaymentStatusId INT NOT NULL,
    PaymentDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Amount DECIMAL(10,2) NOT NULL,
    Reference VARCHAR(100) NULL,
    FOREIGN KEY (InvoiceId) REFERENCES Invoices(InvoiceId),
    FOREIGN KEY (PaymentMethodId) REFERENCES PaymentMethods(PaymentMethodId),
    FOREIGN KEY (PaymentStatusId) REFERENCES PaymentStatuses(PaymentStatusId)
);

CREATE TABLE CardTypes (
    CardTypeId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE PaymentCards (
    PaymentCardId INT AUTO_INCREMENT PRIMARY KEY,
    PaymentId INT NOT NULL UNIQUE,
    CardTypeId INT NOT NULL,
    LastFourDigits VARCHAR(4) NOT NULL,
    CardHolder VARCHAR(100) NOT NULL,
    AuthorizationCode VARCHAR(100) NULL,
    FOREIGN KEY (PaymentId) REFERENCES Payments(PaymentId),
    FOREIGN KEY (CardTypeId) REFERENCES CardTypes(CardTypeId)
);

CREATE TABLE AuditActionTypes (
    AuditActionTypeId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Audits (
    AuditId INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    AuditActionTypeId INT NOT NULL,
    AffectedEntity VARCHAR(100) NOT NULL,
    AffectedRecordId INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Description TEXT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (AuditActionTypeId) REFERENCES AuditActionTypes(AuditActionTypeId)
);
