using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifySpecificRelationOfEmail : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifySpecificRelationOfEmail()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_EmailsHelper = new OfficeActivities_EmailsHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifySpecificRelationOfEmail", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifySpecificRelationOfEmail", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifySpecificRelationOfEmail", "Redirect To Compose Email page");
                VisitOffice("mails/compose");
                officeActivities_EmailsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySpecificRelationOfEmail", "Expand Options section");
                officeActivities_EmailsHelper.ClickElement("OptionsSection");
                officeActivities_EmailsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifySpecificRelationOfEmail", "Select Related To section");
                officeActivities_EmailsHelper.SelectByText("RelatedTo", "Client");
                officeActivities_EmailsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifySpecificRelationOfEmail", "Click on Select Related button");
                officeActivities_EmailsHelper.ClickElement("SelectRelatedBtn");
                officeActivities_EmailsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifySpecificRelationOfEmail", "Select first entry");
                officeActivities_EmailsHelper.ClickElement("Select1popup");
                officeActivities_EmailsHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifySpecificRelationOfEmail");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Specific Relation Of Email");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Specific Relation Of Email", "Bug", "Medium", "Office Emails page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Specific Relation Of Email");
                        TakeScreenshot("VerifySpecificRelationOfEmail");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifySpecificRelationOfEmail.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifySpecificRelationOfEmail");
                        string id = loginHelper.getIssueID("Verify Specific Relation Of Email");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifySpecificRelationOfEmail.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Specific Relation Of Email"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Specific Relation Of Email");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifySpecificRelationOfEmail");
                executionLog.WriteInExcel("Verify Specific Relation Of Email", Status, JIRA, "Office Activities Email");
            }
            
        }
    }
}
