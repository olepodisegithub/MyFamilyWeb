using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace MyFamily.Pages.Relatives
{
    public class EditModel : PageModel
    {
        public RelativeInfo relInfo = new RelativeInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=DESKTOP-2N8AUEF;Initial Catalog=Family;Persist Security Info=True;User ID=sa;Password=thabelo12317210@;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM FamilyRelations WHERE AutoID = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id",id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                relInfo.id = reader.GetInt32(0);
                                relInfo.MaidenSurname = reader.GetString(1);
                                relInfo.FirstName = reader.GetString(2);
                                relInfo.OtherNames = reader.GetString(3);
                                relInfo.Gender = reader.GetString(4);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost() 
        {
            relInfo.id = int.Parse(Request.Form["id"]);
            relInfo.MaidenSurname = Request.Form["maidensurname"];
            relInfo.FirstName = Request.Form["firstname"];
            relInfo.OtherNames = Request.Form["othernames"];
            relInfo.Gender = Request.Form["gender"];

            if (relInfo.MaidenSurname.Length == 0 || relInfo.FirstName.Length == 0 ||
                relInfo.OtherNames.Length == 0 || relInfo.Gender.Length == 0)
            {
                errorMessage = "All the fields are required";
            }
            else
            {
                try
                {
                    String connectionString = "Data Source=DESKTOP-2N8AUEF;Initial Catalog=Family;Persist Security Info=True;User ID=sa;Password=thabelo12317210@;Encrypt=True;Trust Server Certificate=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "update FamilyRelations set MaidenSurname = MaidenSurname,FirstName = @FirstName,OtherNames = @OtherNames,Gender = @Gender where AutoID = @AutoID";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@MaidenSurname", relInfo.MaidenSurname);
                            command.Parameters.AddWithValue("@FirstName", relInfo.FirstName);
                            command.Parameters.AddWithValue("@OtherNames", relInfo.OtherNames);
                            command.Parameters.AddWithValue("@Gender", relInfo.Gender);
                            command.Parameters.AddWithValue("@AutoID", relInfo.id);
                            
                            command.ExecuteNonQuery();

                         }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return;
                }
                Response.Redirect("/Relatives/Index");
            }
        }
    }
}
