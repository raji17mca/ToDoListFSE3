namespace ToDoListMicroService.DataBaseConfig
{
    public interface IUserDataBaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
