namespace ToDoListMicroService.DataBaseConfig
{
    public class UserDataBaseSettings : IUserDataBaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
    