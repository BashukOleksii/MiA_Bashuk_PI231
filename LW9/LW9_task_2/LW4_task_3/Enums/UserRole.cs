namespace LW4_task_3.Enums
{
    [Flags]
    public enum UserRole
    {
        None = 0,
        User = 1 << 0,
        Manager = 1 << 1,
        Admin = 1 << 2,
    }
}
