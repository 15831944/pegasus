using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyEditPartnerAssociationConnectUsername : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyEditPartnerAssociationConnectUsername()
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
            var agents_PartnerAssociationHelper = new Agents_PartnerAssociationHelper(GetWebDriver());

            // Variable
            var assname = "TestAssociate" + GetRandomNumber();
            var name = "TestAgent" + RandomNumber(111, 999999);
            var user = "agentuser" + RandomNumber(111,9999999);
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Go to Partner Agent page.");
                VisitOffice("partners/association/create");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Verify page title.");
                VerifyTitle("Create a Partner Association");

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Enter  Association name");
                agents_PartnerAssociationHelper.TypeText("Name", assname);

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Enter FirstNAME");
                agents_PartnerAssociationHelper.TypeText("FirstNAME", name);

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Enter LastName");
                agents_PartnerAssociationHelper.TypeText("LastName", "Tester");

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Enter Date Of Birth");
                agents_PartnerAssociationHelper.TypeText("Birthday", "08/08/1992");

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Select eAddressType");
                agents_PartnerAssociationHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Select eAddressLebel");
                agents_PartnerAssociationHelper.Select("eAddressLebel", "Work");

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Enter eAddressType");
                var Email = "P.Ass" + GetRandomNumber() + "@yopmail.com";
                agents_PartnerAssociationHelper.TypeText("eAddress", Email);

                //executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Select User Account Check Box");
                //agents_PartnerAssociationHelper.ClickElement("UserAccChkBox");

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Enter Username");
                agents_PartnerAssociationHelper.TypeText("UserName", user);

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Select PartnerUser Avatar Check Box");
                agents_PartnerAssociationHelper.ClickElement("ClickOnAvatar");

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Click Save Agent btn");
                agents_PartnerAssociationHelper.ClickElement("AssSave");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Verify success message. ");
                agents_PartnerAssociationHelper.WaitForText("Partner Association Created Successfully.", 05);

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Go to All Partner Association");
                VisitOffice("partners/associations");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Enter association name to be searched");
                agents_PartnerAssociationHelper.TypeText("SearchAssociation", assname);
                agents_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Edit association");
                agents_PartnerAssociationHelper.ClickElement("EditAssociation1");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEditPartnerAssociationConnectUsername", "Verify username box is not present anymore");
                Assert.IsFalse(agents_PartnerAssociationHelper.IsElementPresent("//*[@id='UserUserName']"));

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyEditPartnerAssociationConnectUsername");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Edit Partner Association Connect Username");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Edit Partner Association Connect Username", "Bug", "Medium", "Partner Agents", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Edit Partner Association Connect Username");
                        TakeScreenshot("VerifyEditPartnerAssociationConnectUsername");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEditPartnerAssociationConnectUsername.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyEditPartnerAssociationConnectUsername");
                        string id = loginHelper.getIssueID("Verify Edit Partner Association Connect Username");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEditPartnerAssociationConnectUsername.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Edit Partner Association Connect Username"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Edit Partner Association Connect Username");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyEditPartnerAssociationConnectUsername");
                executionLog.WriteInExcel("Verify Edit Partner Association Connect Username", Status, JIRA, "Agents Portal");
            }
        }
    }
}