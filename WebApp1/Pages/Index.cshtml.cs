using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
 using Microsoft.Data.SqlClient;

namespace WebApp1.Pages;

public class IndexModel : PageModel
{
   public List<Course> courses = new List<Course>();
   private IConfiguration _configuration;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger,IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public void OnGet()
    {

        string connectionString = _configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
        var sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();

        var sqlCommand = new SqlCommand("SELECT CourseID,CourseName,Rating FROM Course;",sqlConnection);

        using(SqlDataReader dr = sqlCommand.ExecuteReader())
        {
            while (dr.Read())
            {
                courses.Add(new Course(){CourseID=int.Parse(dr["CourseID"].ToString()),CourseName=dr["CourseName"].ToString(),Rating=decimal.Parse(dr["Rating"].ToString())});
            }
        }

    }
}
