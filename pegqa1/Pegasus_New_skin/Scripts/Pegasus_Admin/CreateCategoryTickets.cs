using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CreateCategoryTickets : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createCategoryTickets()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            String JIRA = "";
            String Status = "Pass";

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var tickets_MasterDataHelper = new Tickets_MasterDataHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();

            try
            {
                executionLog.Log("CreateCategoryTickets", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateCategoryTickets", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateCategoryTickets", "Redirect To URL");
                VisitOffice("tickets/masterdata/category");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateCategoryTickets", "Verify title");
                VerifyTitle("Master Data");

                executionLog.Log("CreateCategoryTickets", " Click On Create");
                tickets_MasterDataHelper.ClickElement("Create");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateCategoryTickets", "Verify title");
                VerifyTitle("Create");

                executionLog.Log("CreateCategoryTickets", "Enter Name");
                tickets_MasterDataHelper.TypeText("Name", name);

                executionLog.Log("CreateCategoryTickets", "Click on Save  ");
                tickets_MasterDataHelper.ClickElement("Save");
                tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateCategoryTickets", "Wait for text");
                tickets_MasterDataHelper.WaitForText("Masterdata created successfully", 10);

                executionLog.Log("CreateCategoryTickets", "Click on delete item");
                tickets_MasterDataHelper.ClickElement("DeleteItem");
                tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateCategoryTickets", "Delete item.");
                tickets_MasterDataHelper.DeleteCategory(name);

                executionLog.Log("CreateCategoryTickets", "Select replace with ");
                tickets_MasterDataHelper.SelectByText("ReplaceWith", "Other");
                //tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateCategoryTickets", "Confirm delete by clicking save. ");
                tickets_MasterDataHelper.ClickElement("SaveDelete");
                tickets_MasterDataHelper.AcceptAlert();
                tickets_MasterDataHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateCategoryTickets");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Category Tickets");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Category Tickets", "Bug", "Medium", "Ticket category page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Category Tickets");
                        TakeScreenshot("CreateCategoryTickets");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateCategoryTickets.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateCategoryTickets");
                        string id = loginHelper.getIssueID("Create Category Tickets");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateCategoryTickets.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Category Tickets"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Category Tickets");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateCategoryTickets");
                executionLog.WriteInExcel("Create Category Tickets", Status, JIRA, "Ticketing System");
            }
        }
    }
}
