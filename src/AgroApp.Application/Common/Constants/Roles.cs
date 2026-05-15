namespace AgroApp.Application.Common.Constants;

public static class Roles
{
    public const string Admin = "Admin";
    public const string Manager = "Manager";
    public const string Farmer = "Farmer";
    public const string Viewer = "Viewer";

    public const string AdminOrManager = $"{Admin},{Manager}";
    public const string AdminManagerOrFarmer = $"{Admin},{Manager},{Farmer}";
    public const string All = $"{Admin},{Manager},{Farmer},{Viewer}";
}