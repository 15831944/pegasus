using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CreateEditAvators : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void createEditAvators()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpSystem_AvtarsHelper = new CorpSystem_AvtarsHelper(GetWebDriver());

            // Variable random
            var AvaName = "AvaTest" + RandomNumber(44, 777);
            var name = "Test" + RandomNumber(99, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateEditAvators", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("CreateEditAvators", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateEditAvators", "Redirect at avatars Create page.");
                VisitCorp("avatars/create");
                corpSystem_AvtarsHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateEditAvators", "Enter Avatar Name");
                corpSystem_AvtarsHelper.TypeText("AvatarName", AvaName);

                executionLog.Log("CreateEditAvators", "Select User Type");
                corpSystem_AvtarsHelper.SelectByText("SelectUserType", "Employee");
                corpSystem_AvtarsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEditAvators", "Click onSave");
                corpSystem_AvtarsHelper.ClickElement("Save");
                corpSystem_AvtarsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateEditAvators", "Wait for confirmation");
                corpSystem_AvtarsHelper.WaitForText("Avatar has been created.", 30);

                executionLog.Log("CreateEditAvators", "Search Avatars");
                corpSystem_AvtarsHelper.TypeText("SearchAvatars", AvaName);
                corpSystem_AvtarsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEditAvators", "Click Edit Avator");
                corpSystem_AvtarsHelper.ClickElement("ClickEditAvator");
                corpSystem_AvtarsHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateEditAvators", "Click on Save");
                corpSystem_AvtarsHelper.ClickJS("Save");
                corpSystem_AvtarsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEditAvators", "Search Avatars");
                corpSystem_AvtarsHelper.TypeText("SearchAvatars", AvaName);
                corpSystem_AvtarsHelper.WaitForWorkAround(6000);

                executionLog.Log("CreateEditAvators", "Click on delete icon");
                corpSystem_AvtarsHelper.ClickElement("DeleteIcon");
                corpSystem_AvtarsHelper.AcceptAlert();
                corpSystem_AvtarsHelper.WaitForWorkAround(3000);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateEditAvators");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Edit Avators");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Edit Avators", "Bug", "Medium", "Corp Avatar page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Edit Avators");
                        TakeScreenshot("CreateEditAvators");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateEditAvators.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateEditAvators");
                        string id = loginHelper.getIssueID("Create Edit Avators");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateEditAvators.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Edit Avators"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Edit Avators");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateEditAvators");
                executionLog.WriteInExcel("Create Edit Avators", Status, JIRA, "Corp System");
            }
        }
    }
}
