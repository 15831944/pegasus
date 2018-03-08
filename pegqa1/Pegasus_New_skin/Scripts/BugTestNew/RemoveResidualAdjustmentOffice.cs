using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class RemoveResidualAdjustmentOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("DeleteRA")]
        public void removeResidualAdjustmentOffice()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var ResidualIncome_MasterDataHelper = new ResidualIncome_MasterDataHelper(GetWebDriver());

            // Random Variables
            String JIRA = "";
            String Status = "Pass";

      //      try
        //    {

                executionLog.Log("RemoveResidualAdjustmentOffice", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RemoveResidualAdjustmentOffice", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("RemoveResidualAdjustmentOffice", "Go to residual adjustments tools page.");
                VisitOffice("rir/adjustments_tool");

            executionLog.Log("RemoveResidualAdjustmentOffice", "Delete residual tools.");
            ResidualIncome_MasterDataHelper.WaitForWorkAround(6000);
            ResidualIncome_MasterDataHelper.CleanAdjustment();

            }
            } }
    /*        catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RemoveResidualAdjustmentOffice");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Remove Residual Adjustment Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Remove Residual Adjustment Office", "Bug", "Medium", "Residual Adjustment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Remove Residual Adjustment Office");
                        TakeScreenshot("RemoveResidualAdjustmentOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RemoveResidualAdjustmentOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RemoveResidualAdjustmentOffice");
                        string id = loginHelper.getIssueID("Remove Residual Adjustment Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RemoveResidualAdjustmentOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Remove Residual Adjustment Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Remove Residual Adjustment Office");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RemoveResidualAdjustmentOffice");
                executionLog.WriteInExcel("Remove Residual Adjustment Office", Status, JIRA, "Residual Income");
            }
        }
    }
}*/