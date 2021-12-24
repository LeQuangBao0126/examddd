namespace Examination.Infrastructure.SeedWork;


//app-setting
public class ExamSettings
{
    public string IdentityUrl { get; set; }
    public DatabaseSettings DatabaseSettings { get; set; }
}

public class DatabaseSettings
{
    public string ConnectionStrings { get; set; }
    public string DatabaseName { get; set; }
}