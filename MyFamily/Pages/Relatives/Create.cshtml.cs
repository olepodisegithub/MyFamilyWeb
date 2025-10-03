using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Reflection;

namespace MyFamily.Pages.Relatives
{
    public class CreateModel : PageModel
    {
        public RelativeInfo relInfo = new RelativeInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
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
                //save the new relative into the database

                try
                {
                    String connectionString = "Data Source=DESKTOP-2N8AUEF;Initial Catalog=Family;Persist Security Info=True;User ID=sa;Password=thabelo12317210@;Encrypt=True;Trust Server Certificate=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "INSERT INTO FamilyRelations" +
                                     "(MaidenSurname,FirstName,OtherNames,Gender,DOB,POB,Status,DOD,POD,MotherID,FatherID,ChildrenCount,SiblingsCount,MarriageDate,MarriagePlace,HusbandWifeID,BirthOrder,BirthDateKnown,DeathDateKnown,MarriageDateKnown,CreationDate,CapturedInLDSFamilySearch,Nationality,ContactPerson,CellNumber,StoryOrComment,Reviewed,MarriageStatus,Visited,CapturedInGoogleMaps,LastDateOfChildrenUpdate,EntryNo)" + 
                                     " VALUES " +
                                     "(@MaidenSurname,@FirstName,@OtherNames,@Gender,@DOB,@POB,@Status,@DOD,@POD,@MotherID,@FatherID,@ChildrenCount,@SiblingsCount,@MarriageDate,@MarriagePlace,@HusbandWifeID,@BirthOrder,@BirthDateKnown,@DeathDateKnown,@MarriageDateKnown,@CreationDate,@CapturedInLDSFamilySearch,@Nationality,@ContactPerson,@CellNumber,@StoryOrComment,@Reviewed,@MarriageStatus,@Visited,@CapturedInGoogleMaps,@LastDateOfChildrenUpdate,@EntryNo)";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@MaidenSurname", relInfo.MaidenSurname);
                            command.Parameters.AddWithValue("@FirstName", relInfo.FirstName);
                            command.Parameters.AddWithValue("@OtherNames", relInfo.OtherNames);
                            command.Parameters.AddWithValue("@Gender", relInfo.Gender);
                            command.Parameters.AddWithValue("@DOB",DateTime.Now);
                            command.Parameters.AddWithValue("@POB","");
                            command.Parameters.AddWithValue("@Status","Test");
                            command.Parameters.AddWithValue("@DOD", DateTime.Now);
                            command.Parameters.AddWithValue("@POD","");
                            command.Parameters.AddWithValue("@MotherID",0);
                            command.Parameters.AddWithValue("@FatherID",0);
                            command.Parameters.AddWithValue("@ChildrenCount",0);
                            command.Parameters.AddWithValue("@SiblingsCount",0);
                            command.Parameters.AddWithValue("@MarriageDate", DateTime.Now);
                            command.Parameters.AddWithValue("@MarriagePlace","");
                            command.Parameters.AddWithValue("@HusbandWifeID",0);
                            command.Parameters.AddWithValue("@BirthOrder",0);
                            command.Parameters.AddWithValue("@BirthDateKnown","");
                            command.Parameters.AddWithValue("@DeathDateKnown","");
                            command.Parameters.AddWithValue("@MarriageDateKnown","");
                            command.Parameters.AddWithValue("@CreationDate", DateTime.Now);
                            command.Parameters.AddWithValue("@CapturedInLDSFamilySearch","");
                            command.Parameters.AddWithValue("@Nationality","");
                            command.Parameters.AddWithValue("@ContactPerson","");
                            command.Parameters.AddWithValue("@CellNumber","");
                            command.Parameters.AddWithValue("@StoryOrComment","");
                            command.Parameters.AddWithValue("@Reviewed","");
                            command.Parameters.AddWithValue("@MarriageStatus","");
                            command.Parameters.AddWithValue("@Visited","");
                            command.Parameters.AddWithValue("@CapturedInGoogleMaps","");
                            command.Parameters.AddWithValue("@LastDateOfChildrenUpdate","");
                            command.Parameters.AddWithValue("@EntryNo",0);

                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return;
                }

                relInfo.MaidenSurname = ""; relInfo.FirstName = ""; relInfo.OtherNames = ""; relInfo.Gender = "";
                successMessage = "New Relative Added Correctly";

                Response.Redirect("/Relatives/Index");
            }
        }
    }
}
