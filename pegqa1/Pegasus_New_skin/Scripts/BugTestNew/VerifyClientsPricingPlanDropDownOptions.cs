using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyClientsPricingPlanDropDownOptions : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyClientsPricingPlanDropDownOptions()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyClientsPricingPlanDropDownOptions", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyClientsPricingPlanDropDownOptions", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyClientsPricingPlanDropDownOptions", "Redirect at All merchants page");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsPricingPlanDropDownOptions", "Open any merchant");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyClientsPricingPlanDropDownOptions", "Go to Rates and Fees tab");
                office_ClientsHelper.ClickElement("RatesAndFee");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsPricingPlanDropDownOptions", "Verify Gross and Net option present");
                office_ClientsHelper.VerifyText("PricingPlan", "Gross");
                office_ClientsHelper.VerifyText("PricingPlan", "Net");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyClientsPricingPlanDropDownOptions");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Clients Pricing Plan Drop Down Options");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Clients Pricing Plan Drop Down Options", "Bug", "Medium", "Office Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Clients Pricing Plan Drop Down Options");
                        TakeScreenshot("VerifyClientsPricingPlanDropDownOptions");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyClientsPricingPlanDropDownOptions.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyClientsPricingPlanDropDownOptions");
                        string id = loginHelper.getIssueID("Verify Clients Pricing Plan Drop Down Options");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyClientsPricingPlanDropDownOptions.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Clients Pricing Plan Drop Down Options"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Clients Pricing Plan Drop Down Options");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyClientsPricingPlanDropDownOptions");
                executionLog.WriteInExcel("Verify Clients Pricing Plan Drop Down Options", Status, JIRA, "Office Merchants");
            }
        }
    }
}
