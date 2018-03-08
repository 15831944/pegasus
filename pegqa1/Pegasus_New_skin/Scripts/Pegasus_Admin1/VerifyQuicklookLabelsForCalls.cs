using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyQuicklookLabelsForCalls : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyQuicklookLabelsForCalls()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_CallsHelper = new OfficeActivities_CallsHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var name = "Doc" + RandomNumber(1, 9999);
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            var ValidFile = GetPathToFile() + "index.jpg";
            var InvalidFile = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyQuicklookLabelsForCalls", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyQuicklookLabelsForCalls", " Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Redirect at calls page.");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", " verify title");
                VerifyTitle("Calls");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on create button.");
                officeActivities_CallsHelper.ClickElement("Create");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on save button.");
                officeActivities_CallsHelper.ClickElement("Save");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "verify validation for call type");
                officeActivities_CallsHelper.VerifyText("CallTypeError", "This field is required.");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "verify validation for call name.");
                officeActivities_CallsHelper.VerifyText("CallNameError", "This field is required.");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "verify validation for call to.");
                officeActivities_CallsHelper.VerifyText("CallToNameError", "This field is required.");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "verify validation for from number.");
                officeActivities_CallsHelper.VerifyText("FromNumError", "This field is required.");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "verify validation for to number.");
                officeActivities_CallsHelper.VerifyText("ToNumError", "This field is required.");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Select call type");
                officeActivities_CallsHelper.SelectByText("CallType", "Personal");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Select call related to.");
                officeActivities_CallsHelper.Select("Relatedto", "20");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on find list icon.");
                officeActivities_CallsHelper.ClickElement("Findlist");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on searched result.");
                officeActivities_CallsHelper.ClickElement("Client1");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Enter call from name.");
                officeActivities_CallsHelper.TypeText("CallFrom", "Howard Tang");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Select call category.");
                officeActivities_CallsHelper.SelectByText("Category", "other");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", " Enter call to name");
                officeActivities_CallsHelper.TypeText("CallToName", "Randy Jackson");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", " Enter call from number.");
                officeActivities_CallsHelper.TypeText("FromNumber", "1221221122");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", " Enter call to number.");
                officeActivities_CallsHelper.TypeText("CallTONumber", "1221221122");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on start button.");
                officeActivities_CallsHelper.ClickElement("Start");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Wait for some time.");
                officeActivities_CallsHelper.WaitForWorkAround(10000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on stop button.");
                officeActivities_CallsHelper.ClickElement("Stop");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on save button.");
                officeActivities_CallsHelper.ClickElement("Save");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Wait for success text.");
                officeActivities_CallsHelper.WaitForText("Call logged successfully.", 10);
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Redirect at calls page.");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", " verify title");
                VerifyTitle("Calls");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on any call.");
                officeActivities_CallsHelper.ClickElement("Call1");
                officeActivities_CallsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Verify label for call owner.");
                officeActivities_CallsHelper.VerifyText("VerifyOwner", "Howard Tang");
                //officeActivities_CallsHelper.WaitForWorkAround(3000);
                //officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Verify label for call category.");
                officeActivities_CallsHelper.VerifyText("VerifyCategory", "other");
                //officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on edit button.");
                officeActivities_CallsHelper.ClickElement("EditLink");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Enter call from name.");
                officeActivities_CallsHelper.TypeText("CallFrom", "Howard Tang");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", " Enter call to name");
                officeActivities_CallsHelper.TypeText("CallToName", "Randy Jackson");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", " Enter call from number.");
                officeActivities_CallsHelper.TypeText("FromNumber", "1221221122");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", " Enter call to number.");
                officeActivities_CallsHelper.TypeText("CallTONumber", "1221221122");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on start button.");
                officeActivities_CallsHelper.ClickElement("Start");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Wait for some time.");
                officeActivities_CallsHelper.WaitForWorkAround(6000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on stop button.");
                officeActivities_CallsHelper.ClickElement("Stop");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on save button.");
                officeActivities_CallsHelper.ClickElement("Save");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Redirect at calls page.");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", " verify title");
                VerifyTitle("Calls");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on call to be deleted.");
                officeActivities_CallsHelper.ClickElement("Call1");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Double click on call owner label");
                officeActivities_CallsHelper.DblClick("VerifyOwner");
                officeActivities_CallsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Select updated owner for call.");
                officeActivities_CallsHelper.SelectByText("UpdateEmployee", "Howard Tang");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on save button.");
                officeActivities_CallsHelper.ClickElement("SaveText");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Verify label for call owner.");
                officeActivities_CallsHelper.VerifyText("VerifyOwner", "Howard Tang");
                //officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Redirect at calls page.");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", " verify title");
                VerifyTitle("Calls");
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on call to be deleted.");
                officeActivities_CallsHelper.ClickElement("Call1");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on delete button.");
                officeActivities_CallsHelper.ClickElement("Delete");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Click on OK to accept alert message.");
                officeActivities_CallsHelper.AcceptAlert();
                //officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuicklookLabelsForCalls", "Wait for text call deleted.");
                officeActivities_CallsHelper.WaitForText("Call successfully deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyQuicklookLabelsForCalls");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyQuicklookLabelsForCalls");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyQuicklookLabelsForCalls", "Bug", "Medium", "Tasks page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyQuicklookLabelsForCalls");
                        TakeScreenshot("VerifyQuicklookLabelsForCalls");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Contact.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyQuicklookLabelsForCalls");
                        string id = loginHelper.getIssueID("VerifyQuicklookLabelsForCalls");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Contact.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyQuicklookLabelsForCalls"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyQuicklookLabelsForCalls");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyQuicklookLabelsForCalls");
                executionLog.WriteInExcel("VerifyQuicklookLabelsForCalls", Status, JIRA, "Activities Management");
            }
        }
    }
}