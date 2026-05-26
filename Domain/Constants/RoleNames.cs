namespace Domain.Constants;

public static class RoleNames
{
    public const string Admin = "Admin";
    public const string Mecanico = "Mecánico";
    public const string Recepcionista = "Recepcionista";

    public const string AdminOrRecepcionista = Admin + "," + Recepcionista;
    public const string AdminOrMecanico = Admin + "," + Mecanico;
    public const string AnyInternalRole = Admin + "," + Mecanico + "," + Recepcionista;
}