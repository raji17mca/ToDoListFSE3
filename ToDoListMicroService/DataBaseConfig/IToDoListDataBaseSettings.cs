namespace ToDoListMicroService.DataBaseConfig
{
    public interface IToDoListDataBaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
