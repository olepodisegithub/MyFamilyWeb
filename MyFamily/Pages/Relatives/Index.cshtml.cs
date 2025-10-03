using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace MyFamily.Pages.Relatives
{
    public class IndexModel : PageModel
    {
        public List<RelativeInfo> listRelatives = new List<RelativeInfo>();
        public void OnGet()
        {
            try
            {
                String connectionstring = "Data Source=DESKTOP-2N8AUEF;Initial Catalog=Family;Persist Security Info=True;User ID=sa;Password=thabelo12317210@;Encrypt=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    String sql = "SELECT * FROM FamilyRelations";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RelativeInfo relInfo = new RelativeInfo();
                                relInfo.id = reader.GetInt32(0);
                                relInfo.MaidenSurname =  reader.GetString(1);
                                relInfo.FirstName =  reader.GetString(2);
                                relInfo.OtherNames =  reader.GetString(3);
                                relInfo.Gender =  reader.GetString(4);

                                listRelatives.Add(relInfo);
                            }
                        }     
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            } 
        }
    }

    public class RelativeInfo
    {
        public int id;
        public String MaidenSurname;
        public String FirstName;
        public String OtherNames;
        public String Gender;
    }
}
