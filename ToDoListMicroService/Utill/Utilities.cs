using ToDoListMicroService.Models;

namespace ToDoListMicroService.Util
{
    public static class Utilities
    {
        public static bool isValidStatus(string status)
        {
            return status == StatusConstant.ToDo || status == StatusConstant.InProgress || status ==  StatusConstant.Completed
                || status == StatusConstant.Fixed || status == StatusConstant.Pending;
        }
    }
}
