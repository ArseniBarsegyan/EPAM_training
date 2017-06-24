namespace ManagerSystem.BLL.Interfaces
{
    public interface IServiceCreator
    {
        IUserService CreateUserService(string connectionString);
        IOrderService CreateOrderService(string connectionString);
        IManagerService CreateManagerService(string connectionString);
        IProductService CreateProductService(string connectionString);
    }
}