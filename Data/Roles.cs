namespace ECommerceWebApp.Data
{
    public static class Roles
    {
        public const string SuperAdmin = "SuperAdmin";

        public const string SalesManager = "SalesManager";

        public const string WarehouseManager = "WarehouseManager";

        public const string Customer = "Customer";

        public const string AdminRoles = SuperAdmin + ", " + SalesManager + ", " + WarehouseManager;
    }
}
