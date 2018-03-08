using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyPartnerRevenueSharesAlphanumericAssociationCode : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyPartnerRevenueSharesAlphanumericAssociationCode()
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
            var agents_PartnerAssociationHelper = new Agents_PartnerAssociationHelper(GetWebDriver());

            // Variable
            var code = "GDF-" + RandomNumber(111,99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyPartnerRevenueSharesAlphanumericAssociationCode", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyPartnerRevenueSharesAlphanumericAssociationCode", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyPartnerRevenueSharesAlphanumericAssociationCode", "Redirect at Partner Association page.");
                VisitOffice("partners/associations");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerRevenueSharesAlphanumericAssociationCode", "Redirect at Codes and Revenue Share page");
                agents_PartnerAssociationHelper.ClickElement("RevenueSharesBtn");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerRevenueSharesAlphanumericAssociationCode", "Click on Add a New Revenue Share button");
                agents_PartnerAssociationHelper.ClickElement("NewRevenueShareBtn");
                agents_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyPartnerRevenueSharesAlphanumericAssociationCode", "Enter Agent Code");
                agents_PartnerAssociationHelper.TypeText("AgentCode", code);

                executionLog.Log("VerifyPartnerRevenueSharesAlphanumericAssociationCode", "Enter Revenue Share");
                agents_PartnerAssociationHelper.TypeText("RevenueShare", "15");

                executionLog.Log("VerifyPartnerRevenueSharesAlphanumericAssociationCode", "Click on Save button");
                agents_PartnerAssociationHelper.ClickElement("Save");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerRevenueSharesAlphanumericAssociationCode", "Search for created Code");
                agents_PartnerAssociationHelper.TypeText("SearchAssCode", code);
                agents_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyPartnerRevenueSharesAlphanumericAssociationCode", "Click on Edit button");
                agents_PartnerAssociationHelper.ClickElement("EditBtn1");
                agents_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyPartnerRevenueSharesAlphanumericAssociationCode", "Change Revenue Share Code");
                agents_PartnerAssociationHelper.TypeText("RevenueShareEdit1", "16");
                //agents_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyPartnerRevenueSharesAlphanumericAssociationCode", "Click on Save button");
                agents_PartnerAssociationHelper.ClickElement("SaveBtn1");
                agents_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyPartnerRevenueSharesAlphanumericAssociationCode", "Click on Edit button");
                agents_PartnerAssociationHelper.ClickElement("EditBtn1");
                agents_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyPartnerRevenueSharesAlphanumericAssociationCode", "Click on Edit button");
                agents_PartnerAssociationHelper.VerifyTextBoxValue("RevenueShareEdit1", "16");
                agents_PartnerAssociationHelper.WaitForWorkAround(2000);
                Console.WriteLine("No error occured");


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyPartnerRevenueSharesAlphanumericAssociationCode");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Partner Revenue Shares Alphanumeric Association Code");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Partner Revenue Shares Alphanumeric Association Code", "Bug", "Medium", "Partner Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Partner Revenue Shares Alphanumeric Association Code");
                        TakeScreenshot("VerifyPartnerRevenueSharesAlphanumericAssociationCode");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPartnerRevenueSharesAlphanumericAssociationCode.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyPartnerRevenueSharesAlphanumericAssociationCode");
                        string id = loginHelper.getIssueID("Verify Partner Revenue Shares Alphanumeric Association Code");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPartnerRevenueSharesAlphanumericAssociationCode.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Partner Revenue Shares Alphanumeric Association Code"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Partner Revenue Shares Alphanumeric Association Code");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyPartnerRevenueSharesAlphanumericAssociationCode");
                executionLog.WriteInExcel("Verify Partner Revenue Shares Alphanumeric Association Code", Status, JIRA, "Association Codes and Revenue Share Management");
            }
        }
    }
}